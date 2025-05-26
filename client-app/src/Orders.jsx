import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Orders.css';

const Orders = () => {
  // Simulate authentication
  const isLoggedIn = false; // Replace with real auth logic
  const orders = JSON.parse(localStorage.getItem('orders') || '[]');

  if (!isLoggedIn) {
    return (
      <div className="orders-page">
        <h2>My Orders</h2>
        <p>Please <Link to="/login">log in</Link> or <Link to="/signup">create an account</Link> to view your orders.</p>
      </div>
    );
  }

  return (
    <div className="orders-page">
      <h2>My Orders</h2>
      {orders.length === 0 ? (
        <div>
          <p>You have no orders yet.</p>
          <Link to="/cart" className="checkout-link">Go to Checkout</Link>
        </div>
      ) : (
        <div className="orders-list">
          {orders.map(order => (
            <div key={order.id} className="order-item">
              <div><strong>Order #{order.id}</strong></div>
              <div>Date: {order.date}</div>
              <div>Total: {order.total} kr</div>
              <div>Status: {order.status}</div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default Orders;