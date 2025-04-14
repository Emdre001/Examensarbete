import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./styles/login.css";

const ForgotPassword = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [message, setMessage] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!email) {
      setMessage("Please enter your email address.");
      return;
    }

    // Simulate sending reset email
    setMessage("If this email is registered, a password reset link has been sent.");
    setEmail("");
  };

  return (
    <div className="wrapper">
      <div className="form-container">
        <header>Forgot Your Password?</header>
        <p className="subheader">Enter your email and we’ll send you a reset link</p>

        {message && <div className="success-message">{message}</div>}

        <form onSubmit={handleSubmit} noValidate>
          <div className="input-field">
            <input
              type="email"
              placeholder="Email address"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <i className="fas fa-envelope"></i>
          </div>

          <button
            type="submit"
            className="button login-button"
          >
            Send Reset Link
          </button>
        </form>

        <div className="auth-text">
          Remember your password?{" "}
          <button
            type="button"
            className="auth-link"
            onClick={() => navigate("/login")}
          >
            Back to Login
          </button>
        </div>
      </div>
    </div>
  );
};

export default ForgotPassword;
