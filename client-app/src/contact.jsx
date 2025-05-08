import React from 'react';
import './styles/contact.css'; // OBS! Se till att filnamnet matchar korrekt
import { FaEnvelope } from 'react-icons/fa';

export default function Contact() {
    return (
        <section className="Contact">
            <div className="page-wrapper">
                <h1 className="contact-title">Kontakta oss</h1>
                <p className="contact-lead">
                    Vi ser fram emot att höra från dig! Har du några frågor eller funderingar? Tveka inte att skicka oss ett mail.
                </p>

                <p className="contact-email">
                    <FaEnvelope size={20} />
                    <a href="mailto:shoes.solemate2025@gmail.com"> shoes.solemate2025@gmail.com
                    </a>
                </p>

                <p className="contact-address">
                    <strong>Adress:</strong> Malmvägen 1, 115 41, Stockholm, Sverige
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
    );
}


