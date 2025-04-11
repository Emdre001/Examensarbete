import React, { useState, useEffect, useRef } from "react";
import { gsap } from "gsap";
import "./styles/login.css";

const Login = ({ onLogin }) => {
  const [showPassword, setShowPassword] = useState(false);
  const [eyesCovered, setEyesCovered] = useState(false);
  const leftEyeRef = useRef(null);
  const rightEyeRef = useRef(null);
  const emailRef = useRef(null);

  // Initialize animations
  useEffect(() => {
    const emailInput = emailRef.current;
    
    const handleEmailInput = (e) => {
      const emailLength = e.target.value.length;
      const movement = Math.min(emailLength * 2, 30); // Limit movement range
      
      gsap.to([leftEyeRef.current, rightEyeRef.current], {
        x: movement - 15, // Center the movement
        duration: 0.3,
        ease: "power1.out"
      });
    };

    emailInput.addEventListener("input", handleEmailInput);
    
    return () => {
      emailInput.removeEventListener("input", handleEmailInput);
    };
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    onLogin(); // Trigger login
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <div className="svg-container">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 200 200"
            className={`face-svg ${eyesCovered ? "eyes-covered" : ""}`}
          >
            {/* Face */}
            <circle cx="100" cy="100" r="80" fill="#FFDAC1" className="face" />
            
            {/* Eyes */}
            <circle 
              cx="70" 
              cy="80" 
              r="10" 
              fill="black" 
              className="eye" 
              ref={leftEyeRef}
            />
            <circle 
              cx="130" 
              cy="80" 
              r="10" 
              fill="black" 
              className="eye" 
              ref={rightEyeRef}
            />
            
            {/* Eye covers (shown when password focused) */}
            {eyesCovered && (
              <>
                <rect x="55" y="65" width="30" height="30" fill="#FFDAC1" />
                <rect x="115" y="65" width="30" height="30" fill="#FFDAC1" />
              </>
            )}
            
            {/* Smile */}
            <path 
              d="M60,120 Q100,150 140,120" 
              fill="none" 
              stroke="black" 
              strokeWidth="3"
              className="mouth"
            />
          </svg>
        </div>

        <h2 className="login-header">Welcome Back!</h2>
        
        <form className="login-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="loginEmail">Email:</label>
            <input
              type="email"
              id="loginEmail"
              ref={emailRef}
              placeholder="Enter your email"
              required
            />
          </div>
          
          <div className="form-group">
            <label htmlFor="loginPassword">Password:</label>
            <div className="password-container">
              <input
                type={showPassword ? "text" : "password"}
                id="loginPassword"
                placeholder="Enter your password"
                onFocus={() => setEyesCovered(true)}
                onBlur={() => setEyesCovered(false)}
                required
              />
              <button
                type="button"
                className="show-password-btn"
                onClick={toggleShowPassword}
                aria-label={showPassword ? "Hide password" : "Show password"}
              >
                {showPassword ? "🙈" : "👁️"}
              </button>
            </div>
          </div>
          
          <button type="submit" className="login-btn">
            Login
          </button>
        </form>
      </div>
    </div>
  );
};

export default Login;