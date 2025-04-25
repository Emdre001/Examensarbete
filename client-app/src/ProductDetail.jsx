import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import './styles/ProductDetail.css';

const productData = {
    1: {
        name: "Nike Air Max DN Women",
        price: 1517,
        images: [
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen.png",
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen2.png",
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen3.png",
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen4.png",
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen5.png",
            process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen6.png",
        ],
        video: process.env.PUBLIC_URL + "/Assets/video/AirMaxWomenVid.mp4",
        sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41"],
        description: "Nike Air Max DN är utformad för komfort hela dagen och en sportig stil."
    },
    // Template for future products
    2: {
        name: "Coming Soon Product",
        price: 0,
        images: [],
        video: "",
        sizes: [],
        description: "Produktbeskrivning kommer snart."
    }
};

export function ProductDetail() {
    const { id } = useParams();
    const product = productData[id];

    const [selectedImage, setSelectedImage] = useState(product?.images[0]);

    if (!product) {
        return <div>Produkten hittades inte.</div>;
    }

    return (
        <div className="product-detail-container">
            <div className="product-gallery">
                {product.images.map((img, index) => (
                    <img
                        key={index}
                        src={img}
                        alt={`Product ${index}`}
                        className="thumbnail"
                        onClick={() => setSelectedImage(img)}
                    />
                ))}
                {product.video && (
                    <video controls className="product-video">
                        <source src={product.video} type="video/mp4" />
                        Din webbläsare stöder inte video-taggen.
                    </video>
                )}
            </div>

            <div className="product-main-image">
                {selectedImage ? (
                    <img src={selectedImage} alt="Selected Product" className="main-image" />
                ) : (
                    <p>Ingen bild vald.</p>
                )}
            </div>

            <div className="product-info">
                <h1>{product.name}</h1>
                <p className="product-price">{product.price} kr</p>
                <p className="product-description">{product.description}</p>
                <h3>Välj storlek:</h3>
                <div className="size-selector">
                    {product.sizes.map((size, index) => (
                        <button key={index} className="size-button">{size}</button>
                    ))}
                </div>
                <button className="add-to-cart-button">Lägg i shoppingbagen</button>
            </div>
        </div>
    );
}

export default ProductDetail;
