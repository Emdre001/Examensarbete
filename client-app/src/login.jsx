import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './styles/login.css';

const Login = ({ onLoginSuccess }) => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    rememberMe: false
  });
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'checkbox' ? checked : value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');
    
    if (!formData.email || !formData.password) {
      setError('Please fill in all fields');
      return;
    }

    try {
      setIsLoading(true);
      // Simulate API call
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // In a real app, you would verify credentials here
      console.log('Login attempt with:', formData);
      
      // Call the success handler (would set auth state in parent)
      if (onLoginSuccess) {
        onLoginSuccess({
          email: formData.email,
          name: "Test User" // In real app, this would come from your backend
        });
      }
      
      // Redirect to shop page after login
      navigate('/shop');
    } catch (err) {
      setError('Invalid credentials. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  const navigateToForgotPassword = () => {
    navigate('/forgot-password');
  };

  const navigateToSignUp = () => {
    navigate('/signup');
  };

  return (
    <div className="wrapper">
      <div className="form-container">
        <header>Welcome to SoleMate</header>
        <p className="subheader">Sign in to access your shoe collection</p>
        
        <form onSubmit={handleSubmit} noValidate>
          {error && <div className="error-message" role="alert">{error}</div>}

          <div className="input-field">
            <label htmlFor="email" className="sr-only">Email</label>
            <input
              type="email"
              id="email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              placeholder="Enter your email"
              required
              autoComplete="username"
            />
            <i className="fas fa-envelope"></i>
          </div>

          <div className="input-field">
            <label htmlFor="password" className="sr-only">Password</label>
            <input
              type={showPassword ? "text" : "password"}
              id="password"
              name="password"
              value={formData.password}
              onChange={handleChange}
              placeholder="Enter your password"
              required
              autoComplete="current-password"
            />
            <i 
              className={`fas ${showPassword ? 'fa-eye-slash' : 'fa-eye'} password-toggle`}
              onClick={() => setShowPassword(!showPassword)}
              aria-label={showPassword ? 'Hide password' : 'Show password'}
              role="button"
              tabIndex="0"
            ></i>
          </div>

          <div className="options">
            <div className="checkbox">
              <input
                type="checkbox"
                id="remember"
                name="rememberMe"
                checked={formData.rememberMe}
                onChange={handleChange}
              />
              <label htmlFor="remember">Remember me</label>
            </div>
            <button 
              type="button" 
              className="forgot-password"
              onClick={navigateToForgotPassword}
            >
              Forgot password?
            </button>
          </div>

          <button 
            type="submit" 
            className="button login-button"
            disabled={isLoading}
          >
            {isLoading ? (
              <>
                <span className="spinner"></span>
                Signing in...
              </>
            ) : (
              <>
                <i className="fas fa-shoe-prints"></i> Login Now
              </>
            )}
          </button>
        </form>

        <div className="auth-text">
          New to SoleMate??{' '}
          <button 
            type="button" 
            className="auth-link"
            onClick={navigateToSignUp}
          >
            Create an account
          </button>
        </div>
      </div>
    </div>
  );
};

export default Login;