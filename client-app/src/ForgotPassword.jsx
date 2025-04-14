import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './styles/login.css';

const ForgotPassword = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    // Simulate sending reset link
    setMessage(`Password reset link sent to ${email}`);
    setTimeout(() => navigate('/login'), 3000);
  };

  return (
    <div className="login-container">
      <div className="login-form">
        <img src="/LOGO.png" alt="Shoe Store Logo" className="login-logo" />
        <h2>Reset Your Password</h2>
        
        {message && <div className="success-message">{message}</div>}
        
        <form onSubmit={handleSubmit}>
          <div className="input-group">
            <input
              type="email"
              placeholder="Enter your email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          
          <button type="submit" className="login-button">
            Send Reset Link
          </button>
        </form>
        
        <div className="signup-link">
          Remember your password?{' '}
          <button 
            type="button" 
            className="text-button"
            onClick={() => navigate('/login')}
          >
            Back to Login
          </button>
        </div>
      </div>
    </div>
  );
};

export default ForgotPassword;