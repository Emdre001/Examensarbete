import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './styles/login.css';

const Login = ({ onLoginSuccess }) => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    identifier: '',
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
    
    if (!formData.identifier || !formData.password) {
      setError('Please fill in all fields');
      return;
    }

    try {
      setIsLoading(true);

      const credentials = btoa(`${formData.identifier}:${formData.password}`);
      localStorage.setItem('auth', credentials);
      console.log('Logging in with credentials:', credentials);

      const response = await fetch('http://localhost:5066/api/Account/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          UsernameOrEmail: formData.identifier,
          Password: formData.password,
        })
      });

      console.log('Response status:', response.status);

      if (!response.ok) {
        const msg = await response.text();
        setError(msg || 'Invalid credentials. Please try again.');
        setIsLoading(false);
        return;
      }

      const data = await response.json();

      if (onLoginSuccess) {
        onLoginSuccess({
          name: data.name,
          role: data.role,
        });
      }

      navigate('/');
    }
    catch (err) {
      setError('Login failed. Please try again.');
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
              type="text"
              id="identifier"
              name="identifier"
              value={formData.identifier}
              onChange={handleChange}
              placeholder="Email or Username"
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