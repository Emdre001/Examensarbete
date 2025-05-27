import React from 'react';
import useCartStore from './CartStore';
import './styles/cart.css';
import { FaTrash } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';


const colorNames = {
  "#ff0000": "Red",
  "#007aff": "Blue",
  "#228B22": "Olive Green",
  "#ffffff": "White",
  "#000000": "Black",
  "#888888": "Grey",
  "#8B5C2A": "Brown",
  "#ff69b4": "Pink",
  "#f5f5dc": "Beige"
};

const Cart = () => {
  const { cart, removeFromCart, clearCart, updateQuantity } = useCartStore();
  const navigate = useNavigate();

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
            {cart.map((item) => {              
              return (
                <div key={`${item.id}-${item.size}-${item.color}`} className="cart-item">
                  <img src={item.image} alt={item.name} className="cart-item-image" />
                  <div className="cart-item-details">
                    <h3 className="cart-item-name">{item.name}</h3>
                    <div className="cart-item-options">
                      {/* Size row */}
                     <div className="cart-size-row">
                      <span>Size:</span>
                      <span className="cart-size-value">{item.size}</span>
                    </div>
                      {/* Color row */}
                      <div className="cart-color-row">
                        <span>Color:</span>
                        <span
                          className="cart-color-circle"
                          style={{ background: item.color }}
                        ></span>
                        <span className="cart-color-name">{colorNames[item.color] || item.color}</span>
                      </div>
                    </div>
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
              );
            })}
          </div>
          <div className="cart-summary">
            <h3>Summary</h3>
            <div className="summary-details">
              <p>Subtotal: <span>{totalPrice.toFixed(2)} kr</span></p>
              <p>Shipping: <span>Free</span></p>
              <p className="summary-total">Total: <span>{totalPrice.toFixed(2)} kr</span></p>
            </div>
            <button
              className="checkout-button"
              onClick={() => navigate('/checkout')}
            >
              Checkout
            </button>
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