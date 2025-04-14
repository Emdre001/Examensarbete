import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './styles/login.css'; // Same styling as login

const Signup = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: ''
  });
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    if (!formData.name || !formData.email || !formData.password) {
      setError('Please fill in all fields');
      return;
    }

    try {
      setIsLoading(true);
      await new Promise(resolve => setTimeout(resolve, 1000));

      // Simulate success (replace with backend call)
      console.log('Sign up data:', formData);
      navigate('/login');
    } catch (err) {
      setError('Something went wrong. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="wrapper">
      <div className="form-container">
        <header>Create your SoleMate Account</header>
        <p className="subheader">Join to start your sneaker journey</p>

        <form onSubmit={handleSubmit} noValidate>
          {error && <div className="error-message">{error}</div>}

          <div className="input-field">
            <input
              type="text"
              name="name"
              placeholder="Full Name"
              value={formData.name}
              onChange={handleChange}
              required
            />
            <i className="fas fa-user"></i>
          </div>

          <div className="input-field">
            <input
              type="email"
              name="email"
              placeholder="Email address"
              value={formData.email}
              onChange={handleChange}
              required
            />
            <i className="fas fa-envelope"></i>
          </div>

          <div className="input-field">
            <input
              type={showPassword ? "text" : "password"}
              name="password"
              placeholder="Password"
              value={formData.password}
              onChange={handleChange}
              required
            />
            <i
              className={`fas ${showPassword ? 'fa-eye-slash' : 'fa-eye'} password-toggle`}
              onClick={() => setShowPassword(!showPassword)}
              role="button"
              tabIndex="0"
            ></i>
          </div>

          <button
            type="submit"
            className="button login-button"
            disabled={isLoading}
          >
            {isLoading ? (
              <>
                <span className="spinner"></span>
                Creating account...
              </>
            ) : (
              <>
                <i className="fas fa-user-plus"></i> Sign Up
              </>
            )}
          </button>
        </form>

        <div className="auth-text">
          Already have an account?{' '}
          <button
            type="button"
            className="auth-link"
            onClick={() => navigate('/login')}
          >
            Log in
          </button>
        </div>
      </div>
    </div>
  );
};

export default Signup;
