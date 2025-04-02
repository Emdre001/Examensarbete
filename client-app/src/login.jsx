import React, { useState } from "react";
import "../styles/login.css"; // Ensure the correct path

const Login = () => {
    const [formData, setFormData] = useState({ email: "", password: "" });
    const [errorMessage, setErrorMessage] = useState("");

    // Handle input changes
    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    // Handle form submission
    const handleSubmit = (e) => {
        e.preventDefault();
        if (!formData.email || !formData.password) {
            setErrorMessage("Please fill in all fields.");
        } else {
            setErrorMessage("");
            console.log("Logging in with:", formData);
            // Perform authentication logic here
        }
    };

    return (
        <div className="login-page">
            <div className="login-container">
                <h2 className="login-header">Welcome Back!</h2>
                <p className="info-text-login">Please enter your credentials to continue.</p>

                <form className="login-form" onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label htmlFor="email">Email:</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            placeholder="Enter your email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            placeholder="Enter your password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    {errorMessage && <p className="error-message">{errorMessage}</p>}

                    <button type="submit">Login</button>
                </form>
            </div>
        </div>
    );
};

export default Login;
