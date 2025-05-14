import React from 'react';
import useCartStore from './CartStore';
import './styles/cart.css';
import { FaTrash } from 'react-icons/fa';

const Cart = () => {
  const { cart, removeFromCart, clearCart, updateQuantity } = useCartStore();

  const totalPrice = cart.reduce((sum, item) => {
    const price = item.discount
      ? item.price * (1 - item.discount / 100)
      : item.price;
    return sum + price * item.quantity;
  }, 0);

  return (
    <div className="cart-page">
      <h2 className="cart-title">Shoppingbag</h2>

      {cart.length === 0 ? (
        <p className="empty-cart-message">Your cart is empty.</p>
      ) : (
        <div className="cart-content">
          <div className="cart-items">
            {cart.map((item) => (
              <div key={`${item.id}-${item.size}`} className="cart-item">
                <img src={item.image} alt={item.name} className="cart-item-image" />
                <div className="cart-item-details">
                  <h3 className="cart-item-name">{item.name}</h3>
                  <p className="cart-item-size">Size: {item.size}</p>
                  <p className="cart-item-price">
                    {item.discount
                      ? `Price: ${(item.price * (1 - item.discount / 100)).toFixed(2)} kr`
                      : `Price: ${item.price} kr`}
                  </p>
                  <div className="cart-item-quantity">
                    <button
                      className="quantity-button"
                      onClick={() => updateQuantity(item.id, item.size, item.quantity - 1)}
                      disabled={item.quantity === 1}
                    >
                      -
                    </button>
                    <span className="quantity-display">{item.quantity}</span>
                    <button
                      className="quantity-button"
                      onClick={() => updateQuantity(item.id, item.size, item.quantity + 1)}
                    >
                      +
                    </button>
                  </div>
                </div>
                <button
                  className="remove-button"
                  onClick={() => removeFromCart(item.id, item.size)}
                >
                  <FaTrash />
                </button>
              </div>
            ))}
          </div>

          <div className="cart-summary">
            <h3>Summary</h3>
            <div className="summary-details">
              <p>Subtotal: <span>{totalPrice.toFixed(2)} kr</span></p>
              <p>Shipping: <span>Free</span></p>
              <p className="summary-total">Total: <span>{totalPrice.toFixed(2)} kr</span></p>
            </div>
            <button className="checkout-button">Checkout</button>
            <button className="clear-cart-button" onClick={clearCart}>
              Clear Cart
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Cart;