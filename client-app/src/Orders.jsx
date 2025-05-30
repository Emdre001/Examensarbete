import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Orders.css';

const Orders = () => {
  // Simulate authentication
  const isLoggedIn = true; // Replace with real auth logic, set to true for testing
  const orders = JSON.parse(localStorage.getItem('orders') || '[]');

   if (!isLoggedIn) {
    return (
      <div className="orders-page">
        <h2>My Orders</h2>
        <p>
          Please <Link to="/login">log in</Link> or <Link to="/signup">create an account</Link> to view your orders.
        </p>
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
              <div className="order-header">
                <span className="order-id">Order #{order.id}</span>
                <span className={`order-status order-status-${order.status.toLowerCase()}`}>{order.status}</span>
              </div>
              <div className="order-date">Date: {order.date}</div>
              <div className="order-total">Total: <strong>{order.total} kr</strong></div>
              <div className="order-products">
                <strong>Products:</strong>
                <ul>
                {order.items.map((item, idx) => (
                    <li key={idx} style={{ display: 'flex', alignItems: 'center', marginBottom: 8 }}>
                    <img
                        src={item.image}
                        alt={item.name}
                        style={{ width: 40, height: 40, objectFit: 'cover', borderRadius: 6, marginRight: 12 }}
                    />
                    <span>
                        {item.name} (Size: {item.size}) x {item.quantity} – {item.price} kr each
                    </span>
                    </li>
                ))}
                </ul>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default Orders;