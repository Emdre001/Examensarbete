import React, { useState, useEffect } from 'react';
import useCartStore from './CartStore';
import { useNavigate } from 'react-router-dom';
import './styles/checkout.css';
import ToastNotification from './ToastNotification';

const Checkout = () => {
  const { cart, clearCart } = useCartStore();
  const [form, setForm] = useState({ name: '', email: '', address: '', payment: 'card' });
  const [showToast, setShowToast] = useState(false);
  const [sizeMap, setSizeMap] = useState({});
  const navigate = useNavigate();

  const totalPrice = cart.reduce((sum, item) => sum + item.price * item.quantity, 0);

  useEffect(() => {
    const uniqueSizeIds = [...new Set(cart.map(item => item.size))];

    Promise.all(
      uniqueSizeIds.map(sizeId =>
        fetch(`http://localhost:5066/api/Size/GetSizeById/${sizeId}`)
          .then(res => res.json())
          .then(data => ({
            sizeId: data.sizeId,
            sizeValue: data.sizeValue
          }))
          .catch(err => {
            console.error(`Failed to fetch size for ${sizeId}`, err);
            return { sizeId, sizeValue: sizeId };
          })
      )
    ).then(results => {
      const map = {};
      results.forEach(({ sizeId, sizeValue }) => {
        map[sizeId] = sizeValue;
      });
      setSizeMap(map);
    });
  }, [cart]);

  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
  e.preventDefault();

    try {
      for (const item of cart) {
        const productId = item.productId || item.id;
        const sizeId = item.size;
        const quantity = item.quantity;

        // ✅ 1. Get current stock using the correct endpoint
        const sizeRes = await fetch(`http://localhost:5066/api/GetProductSizeById/${productId}/${sizeId}`);
        if (!sizeRes.ok) throw new Error(`Failed to fetch stock for product ${productId}, size ${sizeId}`);
        const stockData = await sizeRes.json();

        const currentStock = stockData.stock;
        if (currentStock == null) throw new Error('Stock value missing in response');

        const newStock = currentStock - quantity;
        if (newStock < 0) {
          alert(`Not enough stock for ${item.name} (Size: ${sizeMap[sizeId] || sizeId})`);
          return;
        }

        // ✅ 2. Update the stock using correct PUT endpoint and payload
        const updateRes = await fetch(`http://localhost:5066/api/updateProductSize/${productId}/${sizeId}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            productId,
            sizeId,
            stock: newStock
          }),
        });

        if (!updateRes.ok) throw new Error('Failed to update product size stock');
      }

      // ✅ 3. Save order to localStorage
      const orders = JSON.parse(localStorage.getItem('orders') || '[]');
      orders.push({
        id: Date.now(),
        items: cart,
        total: totalPrice,
        date: new Date().toLocaleDateString(),
        status: 'Placed',
        ...form,
      });
      localStorage.setItem('orders', JSON.stringify(orders));

      clearCart();
      setShowToast(true);
      setTimeout(() => {
        setShowToast(false);
        navigate('/orders');
      }, 1200);
    } catch (error) {
      console.error(error);
      alert(`Order failed: ${error.message}`);
    }
  };


  return (
    <div className="checkout-page">
      <ToastNotification show={showToast} message="Order placed! Log in to view your orders." />
      <h2>Checkout</h2>
      <div className="checkout-cart">
        <h3>Your Items</h3>
        {cart.length === 0 ? (
          <p>Your cart is empty.</p>
        ) : (
          <ul>
            {cart.map(item => (
              <li key={`${item.id}-${item.size}`}>
                {item.name} (Size: {sizeMap[item.size] || item.size}) x {item.quantity} - {item.price} kr each
              </li>
            ))}
          </ul>
        )}
        <p><strong>Total: {totalPrice} kr</strong></p>
      </div>
      <form className="checkout-form" onSubmit={handleSubmit}>
        <h3>Contact & Shipping</h3>
        <input name="name" placeholder="Full Name" value={form.name} onChange={handleChange} required />
        <input name="email" placeholder="Email" type="email" value={form.email} onChange={handleChange} required />
        <input name="address" placeholder="Shipping Address" value={form.address} onChange={handleChange} required />
        <h3>Payment</h3>
        <select name="payment" value={form.payment} onChange={handleChange}>
          <option value="card">Credit Card</option>
          <option value="swish">Swish</option>
          <option value="klarna">Klarna</option>
        </select>
        <button type="submit" className="place-order-btn" disabled={cart.length === 0}>
          Place Order
        </button>
      </form>
    </div>
  );
};

export default Checkout;
