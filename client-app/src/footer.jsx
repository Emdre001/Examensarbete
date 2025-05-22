import React from "react";
import { Link } from "react-router-dom";
import "./styles/footer.css";

export default function Footer() {
  return (
    <footer className="footer-wrapper">
      <div className="footer-content">
        <div className="footer-links">
          <Link to="/">Home</Link>
          <Link to="/about">About</Link>
          <Link to="/contact">Contact</Link>
        </div>
        <div className="footer-info">
          <span>© 2025 SoleMate</span>
          <span>Made with ❤️ by the SoleMate Team</span>
        </div>
      </div>
    </footer>
  );
}

