import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/home.css";

const Home = () => {
  const navigate = useNavigate();

  return (
    <div className="home-container">
      <section className="hero">
        <h1 className="hero-title">Step into Style with SoleMate</h1>
        <p className="hero-subtitle">Discover the latest sneaker drops and timeless classics.</p>
        <button className="hero-button" onClick={() => navigate("/products")}>
          Shop Now
        </button>
      </section>

      <section className="feature-section">
        <div className="feature-card">
          <h3 className="feature-title">Free Shipping</h3>
          <p className="feature-description">On all orders over $100. Fast and reliable delivery.</p>
        </div>
        <div className="feature-card">
          <h3 className="feature-title">100% Authentic</h3>
          <p className="feature-description">We only sell legit sneakers from trusted brands.</p>
        </div>
        <div className="feature-card">
          <h3 className="feature-title">Exclusive Drops</h3>
          <p className="feature-description">Be the first to cop limited edition releases.</p>
        </div>
      </section>
    </div>
  );
};

export default Home;
