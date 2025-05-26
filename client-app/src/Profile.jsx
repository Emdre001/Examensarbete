import React from 'react';
import { Link } from 'react-router-dom';
import './styles/Profile.css';

const Profile = () => {
  // Replace with real user data if you have authentication
  const user = {
    name: "John Doe",
    email: "john@example.com",
    password: "********"
  };

  return (
    <div className="profile-page">
      <h2>My Profile</h2>
      <div className="profile-info">
        <div><strong>Name:</strong> {user.name}</div>
        <div><strong>Email:</strong> {user.email}</div>
        <div><strong>Password:</strong> {user.password}</div>
      </div>
      <div className="profile-links">
        <Link to="/login">Login</Link> | <Link to="/signup">Sign Up</Link>
      </div>
    </div>
  );
};

export default Profile;