import React from 'react';
import './styles/about.css';

export function About() {
  return (
    <div className="home-wrapper">
      {/* Bakgrundsbild som ligger bakom allt */}
      <div
        className="background-image"
        style={{
          backgroundImage: 'url("/Assets/img/ShoesWall.jpg")',
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundRepeat: "no-repeat",
          backgroundAttachment: "fixed",
          position: "fixed",
          top: 0,
          left: 0,
          width: "100vw",
          height: "100vh",
          opacity: 0.12,
          zIndex: -1
        }}
      ></div>
    <div className="About-page">
      <div className="about-image-section">
        <img src="/Assets/img/About.jpg" alt="About us" className="about-hero-img" />
        <div className="about-image-text">
          Together in every step – sneakers built for unity, comfort and style.
        </div>
      </div>

      <div className="page-wrapper">
        <div className="about-card">
          <p className="about-lead">
          Welcome to SoleMate, your destination to easily and safely buy sneakers online.
          </p>
          <p className="about-lead">
          Our goal is to provide a safe and smooth platform where sneaker enthusiasts can find the latest models and timeless classics.
          We strive to make it easy for you to discover, buy and enjoy high-quality shoes from famous brands.
          </p>
          <p className="about-lead">
          We are constantly updating our range with new products to ensure you always have access to the best shoes on the market.
          Whether you're looking for sporty sneakers, trendy casual shoes or comfortable running shoes, we've got you covered.
          </p>
          <p className="about-lead">
          Our platform is designed with a focus on ease of use and security, so you can trade with complete confidence.
          We are continuously working to improve our service and look forward to offering you the best shopping experience.
          </p>
          <p className="about-lead">
          Do not hesitate to contact us if you have any questions or need help. Welcome to SoleMate - where style and comfort meet!
          </p>

          <img src="/Assets/img/Logo_1.png" alt="LOGO" className="img-fluid" />
        </div>
      </div>
    </div>
    </div>
  );
}

export default About;

