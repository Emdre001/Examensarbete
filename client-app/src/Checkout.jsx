import React, { useState } from 'react';
import useCartStore from './CartStore';
import { useNavigate } from 'react-router-dom';
import './styles/checkout.css';
import ToastNotification from './ToastNotification';

const Checkout = () => {
  const { cart, clearCart } = useCartStore();
  const [form, setForm] = useState({ name: '', email: '', address: '', payment: 'card' });
  const [showToast, setShowToast] = useState(false);
  const navigate = useNavigate();

  const totalPrice = cart.reduce((sum, item) => sum + item.price * item.quantity, 0);

  const handleChange = e => setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async e => {
    e.preventDefault();

    const credentials = localStorage.getItem('auth');
    if (!credentials) {
      alert("You must be logged in to place an order.");
      return;
    }

    const productIds = cart.map(item => item.id);
    const orderData = {
      OrderDetails: `Order for ${cart.length} item(s) by ${form.name}`,
      OrderDate: new Date().toISOString(),
      OrderStatus: "Pending",
      OrderAmount: parseInt(cart.reduce((sum, item) => sum + item.quantity, 0)),
      ProductIds: productIds
    };

    try {
      const response = await fetch('http://localhost:5066/api/Order/Create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Authorization': `Basic ${credentials}`
        },
        body: JSON.stringify(orderData)
      });

      if (!response.ok) {
        const msg = await response.text();
        alert(`Order failed: ${msg}`);
        return;
      }

      clearCart();
      setShowToast(true);
      setTimeout(() => {
        setShowToast(false);
        navigate('/orders');
      }, 1800);
    } catch (err) {
      alert("Error placing order.");
    }
  };

  // const handlePlaceOrder = async () => {
  //   if (!selectedSize || !selectedColor) {
  //     alert("Please select size and color before placing order.");
  //     return;
  //   }

  //   const credentials = localStorage.getItem('auth');
  //   if (!credentials) {
  //     alert("You must be logged in to place an order.");
  //     return;
  //   }

  //   const orderData = {
  //     OrderDetails: "Order for Nike Air Max (Red, size 42)",
  //     OrderDate: new Date().toISOString(),
  //     OrderStatus: "Pending",
  //     OrderAmount: 1200,
  //     ProductsId: [selectedProductId]
  //   };

  //   try {
  //     const response = await fetch('http://localhost:5066/api/Order/Create', {
  //       method: 'POST',
  //       headers: {
  //         'Content-Type': 'application/json',
  //         'Authorization': `Basic ${credentials}`
  //       },
  //       body: JSON.stringify(orderData)
  //     });

  //     if (!response.ok) {
  //       const msg = await response.text();
  //       alert(`Order failed: ${msg}`);
  //       return;
  //     }

  //     alert("Order placed successfully!");
  //   } catch (error) {
  //     console.error('Error placing order:', error);
  //   }
  // }

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
                {item.name} (Size: {item.size}) x {item.quantity} - {item.price} kr each
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