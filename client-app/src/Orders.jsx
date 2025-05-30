import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './styles/Orders.css';

const Orders = ({user}) => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const credentials = localStorage.getItem('auth');
  const isLoggedIn = !!credentials;

  useEffect(() => {
    if (!isLoggedIn) return;
    const fetchOrders = async () => {
      try {
        const response = await fetch(`http://localhost:5066/api/Order/GetByName?userName=${encodeURIComponent(user?.userName || '')}`, {
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
  }, [isLoggedIn, navigate, credentials]);

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
              <div className="order-total">Total: <strong>
                  {(
                    (Array.isArray(order.orderProducts)
                      ? order.orderProducts.map(op => op.product)
                      : order.orderProducts?.$values
                        ? order.orderProducts.$values.map(op => op.product)
                        : []
                    ).reduce((sum, item) =>
                      sum + (item.productPrice || 0), 0)
                  )} kr
                </strong>
              </div>
              <div className="order-products">
                <strong>Products:</strong>
                  <ul>
                    {(Array.isArray(order.orderProducts)
                      ? order.orderProducts.map(op => op.product)
                      : order.orderProducts?.$values
                        ? order.orderProducts.$values.map(op => op.product)
                        : []
                    ).map((item, idx) => (
                      <li key={idx} style={{ display: 'flex', alignItems: 'center', marginBottom: 8 }}>
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
