import React from 'react';
import './styles/contact.css'; // OBS! Se till att filnamnet matchar korrekt
import { FaEnvelope } from 'react-icons/fa';

export default function Contact() {
    return (

        <div className="home-wrapper">
      {/* 🔙 Bakgrundsbild som ligger bakom allt */}
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
        <section className="Contact">
            <div className="page-wrapper">
                <h1 className="contact-title">Contact us</h1>
                <p className="contact-lead">
                We look forward to hearing from you! Do you have any questions or concerns? Do not hesitate to send us an email.                </p>

                <p className="contact-email">
                    <FaEnvelope size={20} />
                    <a href="mailto:shoes.solemate2025@gmail.com"> shoes.solemate2025@gmail.com
                    </a>
                </p>

                <p className="contact-address">
                    <strong>Address:</strong> Malmvägen 1, 115 41, Stockholm, Sverige
                </p>

                <div className="map-container">
                    <iframe
                        title="Google Map"
                        src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2520.555663417943!2d18.081495215744997!3d59.345932981662134!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x465f9d6558f7e7e9%3A0x8b7d8d935d0a1d64!2sMalmv%C3%A4gen%201%2C%20115%2041%20Stockholm%2C%20Sweden!5e0!3m2!1sen!2sus!4v1636563965408!5m2!1sen!2sus"
                        allowFullScreen=""
                        loading="lazy"
                        referrerPolicy="no-referrer-when-downgrade"
                    ></iframe>
                </div>
            </div>
        </section>
        </div>
    );
}


