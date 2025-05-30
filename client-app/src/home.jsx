import React from "react";
import { useNavigate } from "react-router-dom";
import Carousel from "react-bootstrap/Carousel";
import { FaShippingFast, FaShieldAlt, FaStar } from "react-icons/fa";
import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/home.css";

const Home = () => {
  const navigate = useNavigate();

  return (
    <div className="home-wrapper">
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

      <div className="page-wrapper">
        <section className="hero">
          <h1 className="hero-title">Step into Style with SoleMate</h1>
          <p className="hero-subtitle">
            Discover the latest sneaker drops and timeless classics.
          </p>
          <button className="hero-button" onClick={() => navigate("/products")}>
            Shop Now
          </button>

          <div className="carousel-wrapper">
            <Carousel interval={3000} pause={false} fade={false}>
              <Carousel.Item>
                <img
                  className="carousel-img"
                  src="/Assets/img/NewBalance.jpg"
                  alt="New Balance"
                />
              </Carousel.Item>
              <Carousel.Item>
                <img
                  className="carousel-img"
                  src="/Assets/img/NikeDunkBlue.jpg"
                  alt="Nike Dunk Blue"
                />
              </Carousel.Item>
              <Carousel.Item>
                <img
                  className="carousel-img"
                  src="/Assets/img/UggsLow.jpg"
                  alt="Uggs Low"
                />
              </Carousel.Item>
              <Carousel.Item>
                <img
                  className="carousel-img"
                  src="/Assets/img/BabyDunk.jpg"
                  alt="Baby Dunks"
                />
              </Carousel.Item>
              <Carousel.Item>
                <img
                  className="carousel-img"
                  src="/Assets/img/AdidasCampus.jpg"
                  alt="Adidas Campus"
                />
              </Carousel.Item>
            </Carousel>
          </div>
        </section>

        <section className="slogan-section">
  <p className="slogan-subtitle">  We believe in style that unites – for every identity, every body, every step </p>
<div className="brand-logos">
  <img src="/Assets/img/Nike.png" alt="Nike" />
  <img src="/Assets/img/Adidas.png" alt="Adidas" />
  <img src="/Assets/img/NB.png" alt="New Balance" />
  <img src="/Assets/img/Uggs.png" alt="UGG" />
  <img src="/Assets/img/Jordan.png" alt="Jordans" />
  <img src="/Assets/img/Puma.png" alt="Puma" />
  <img src="/Assets/img/Vans.png" alt="Vans" />
</div>
</section>



       <section className="collection-section">
  <div
    className="collection-card"
    onClick={() => navigate("/products?gender=men")}
  >
    <img src="/Assets/img/MAN.jpg" alt="Men" className="collection-img" />
    <div className="collection-label">MEN</div>
  </div>
  <div
    className="collection-card center-card"
    onClick={() => navigate("/products")}
  >
    <img src="/Assets/img/Together.jpg" alt="Our Collection" className="collection-img" />
    <div className="collection-label">OUR COLLECTION</div>
  </div>
  <div
    className="collection-card"
    onClick={() => navigate("/products?gender=women")}
  >
    <img src="/Assets/img/DAM.jpg" alt="Women" className="collection-img" />
    <div className="collection-label">WOMEN</div>
  </div>
</section>


        <section className="features-redesign">
          <div className="feature-box">
            <FaShippingFast size={36} color="#4f46e5" />
            <h3 className="feature-title">Free Shipping</h3>
            <p className="feature-description">
              On all orders over $100. Fast and reliable delivery.
            </p>
          </div>
          <div className="feature-box">
            <FaShieldAlt size={36} color="#4f46e5" />
            <h3 className="feature-title">100% Authentic</h3>
            <p className="feature-description">
              We only sell legit sneakers from trusted brands.
            </p>
          </div>
          <div className="feature-box">
            <FaStar size={36} color="#4f46e5" />
            <h3 className="feature-title">Exclusive Drops</h3>
            <p className="feature-description">
              Be the first to cop limited edition releases.
            </p>
          </div>
        </section>
      </div>
    </div>
  );
};

export default Home;
