import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import './styles/Orders.css';

const Orders = () => {
  const isLoggedIn = true;
  const [orders, setOrders] = useState([]);
  const [sizeMap, setSizeMap] = useState({});
  const [imageMap, setImageMap] = useState({});

  // Load orders from localStorage once
  useEffect(() => {
    const stored = JSON.parse(localStorage.getItem('orders') || '[]');
    setOrders(stored);
  }, []);

  useEffect(() => {
    const fetchExtraData = async () => {
      const newSizeMap = { ...sizeMap };
      const newImageMap = { ...imageMap };

      for (const order of orders) {
        for (const item of order.items) {
          // Fetch size
          if (item.size && !newSizeMap[item.size]) {
            try {
              const res = await fetch(`http://localhost:5066/api/Size/GetSizeById/${item.size}`);
              if (res.ok) {
                const data = await res.json();
                newSizeMap[item.size] = data.sizeValue;
              }
            } catch (err) {
              console.error('Error fetching size:', err);
            }
          }

          // Fetch image
          const key = item.productId;
          if (key && !newImageMap[key]) {
            try {
              const res = await fetch(`http://localhost:5066/api/ProductImage/GetImagesByProductId/${key}`);
              if (res.ok) {
                const data = await res.json();
                const images = data?.$values || [];
                const sorted = images.sort((a, b) =>
                  a.imageUrl.localeCompare(b.imageUrl, undefined, { numeric: true })
                );
                const imagePath = sorted.length > 0 ? sorted[0].imageUrl : null;
                newImageMap[key] = imagePath
                  ? `http://localhost:5066${imagePath.startsWith('/') ? '' : '/'}${imagePath}`
                  : '/placeholder-image.png';
              }
            } catch (err) {
              console.error('Error fetching image:', err);
            }
          }
        }
      }

      setSizeMap(newSizeMap);
      setImageMap(newImageMap);
    };

    if (orders.length > 0) {
      fetchExtraData();
    }
  }, [orders]); // Safe: `orders` is now stable from useState

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
                  {order.items.map((item, idx) => {
                    const sizeLabel = sizeMap[item.size] || item.size;
                    const imageUrl = imageMap[item.productId] || '/placeholder-image.png';

                    return (
                      <li key={idx} style={{ display: 'flex', alignItems: 'center', marginBottom: 8 }}>
                        <img
                          src={imageUrl}
                          alt={item.name}
                          style={{ width: 40, height: 40, objectFit: 'cover', borderRadius: 6, marginRight: 12 }}
                        />
                        <span>
                          {item.name} (Size: {sizeLabel}) x {item.quantity} – {item.price} kr each
                        </span>
                      </li>
                    );
                  })}
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
