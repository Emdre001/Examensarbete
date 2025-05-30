import React, { use, useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './styles/Orders.css';

const Orders = ({user}) => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const credentials = localStorage.getItem('auth');
  const isLoggedIn = !!credentials;
  const userId = user.userId || user.id;

  useEffect(() => {
    if (!isLoggedIn) return;
    const fetchOrders = async () => {
      try {
        const response = await fetch(`http://localhost:5066/api/Order/GetMyOrders`, {
          headers: {
            'Authorization': `Basic ${credentials}`
          }
        });

        if (response.status === 401) {
          navigate('/login');
          return;
        }

        if (!response.ok) {
          throw new Error(await response.text());
        }
        
        const data = await response.json();
        setOrders(data.$values || data);
      } catch (err) {
        setError('Failed to load orders.');
      } finally {
        setLoading(false);
      }
    };

    fetchOrders();
  }, [isLoggedIn, userId, navigate, credentials]);

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

  if (loading) {
    return <div className="orders-page">Loading...</div>;
  }

  if (error) {
    return <div className="orders-page">{error}</div>;
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
            <div key={order.orderId || order.id} className="order-item">
              <div className="order-header">
                <span className="order-id">Order #{order.orderId || order.id}</span>
                <span className={`order-status order-status-${(order.orderStatus || order.status || '').toLowerCase()}`}>
                  {order.orderStatus || order.status}
                </span>
              </div>
              <div className="order-date">Date: {order.orderDate ? new Date(order.orderDate).toLocaleDateString() : ''}</div>
              <div className="order-total">Total: <strong>{order.orderAmount || order.total} kr</strong></div>
              <div className="order-products">
                <strong>Products:</strong>
                <ul>
                  {(order.products || order.items || []).map((item, idx) => (
                    <li key={idx} style={{ display: 'flex', alignItems: 'center', marginBottom: 8 }}>
                      <img
                        src={item.image || item.productImage || ''}
                        alt={item.name || item.productName || ''}
                        style={{ width: 40, height: 40, objectFit: 'cover', borderRadius: 6, marginRight: 12 }}
                      />
                      <span>
                        {(item.name || item.productName || '')}
                        {item.size && ` (Size: ${item.size})`}
                        {item.quantity && ` x ${item.quantity}`}
                        {item.price && ` – ${item.price} kr each`}
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