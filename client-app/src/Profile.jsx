import React, { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import './styles/Profile.css';

const Profile = () => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  useEffect(() => {
    // Replace with your real backend API call
    fetch('/api/profile', { credentials: 'include' })
      .then(res => {
        if (res.status === 401) {
          navigate('/login');
          return null;
        }
        return res.json();
      })
      .then(data => {
        if (data) setUser(data);
      });
  }, [navigate]);

  if (!user) return <div className="profile-page">Loading...</div>;

  return (
    <div className="profile-page">
      <h2>My Profile</h2>
      <div className="profile-info">
        <div><strong>Name:</strong> {user.name}</div>
        <div><strong>Email:</strong> {user.email}</div>
        {/* You can add more fields as needed */}
      </div>
      <div className="profile-actions">
        <Link to="/orders" className="profile-link">My Orders</Link>
        <Link to="/settings" className="profile-link">Settings</Link>
        <button className="profile-logout" onClick={() => {
          // Replace with your logout logic
          fetch('/api/logout', { method: 'POST', credentials: 'include' })
            .then(() => navigate('/login'));
        }}>Log out</button>
      </div>
    </div>
  );
};

export default Profile;