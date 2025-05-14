import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import './styles/ProductDetail.css';
import useCartStore from './CartStore';

const productData = {
  1: {
    name: "Nike Air Max DN Women",
    price: 1499,
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
    description: "Nike Air Max DN är utformad för komfort hela dagen och en sportig stil.",
  },
  2: {
    name: "Coming Soon Product",
    price: 0,
    images: [],
    video: "",
    sizes: [],
    description: "Produktbeskrivning kommer snart.",
  },
};

export function ProductDetail() {
    const { id } = useParams();
    const product = productData[id];
    const addToCart = useCartStore((state) => state.addToCart);
  
    const [selectedMedia, setSelectedMedia] = useState(product?.images[0]); // For main media display
    const [selectedSize, setSelectedSize] = useState(null); // For selected size
  
    if (!product) {
      return <div>Produkten hittades inte.</div>;
    }
  
    const handleAddToCart = () => {
      if (!selectedSize) {
        alert("Vänligen välj en storlek innan du lägger till i shoppingbagen.");
        return;
      }
  
      addToCart({
        id: product.id,
        name: product.name,
        price: product.price,
        image: product.images[0],
        size: selectedSize,
      });
      alert("Produkten har lagts till i shoppingbagen!");
    };
  
    return (
      <div className="product-detail-container">
        <div className="product-gallery">
          {product.video && (
            <div
              className="thumbnail"
              onMouseEnter={() => setSelectedMedia(product.video)}
            >
              <video
                className="thumbnail-video"
                autoPlay
                loop
                muted
              >
                <source src={product.video} type="video/mp4" />
                Din webbläsare stöder inte video-taggen.
              </video>
            </div>
          )}
          {product.images.map((img, index) => (
            <img
              key={index}
              src={img}
              alt={`Product ${index}`}
              className={`thumbnail ${selectedMedia === img ? "selected" : ""}`}
              onMouseEnter={() => setSelectedMedia(img)}
            />
          ))}
        </div>
  
        <div className="product-main-image">
          {selectedMedia.endsWith(".mp4") ? (
            <video controls autoPlay className="main-video">
              <source src={selectedMedia} type="video/mp4" />
              Din webbläsare stöder inte video-taggen.
            </video>
          ) : (
            <img src={selectedMedia} alt="Selected Product" className="main-image" />
          )}
        </div>
  
        <div className="product-info">
          <h1>{product.name}</h1>
          <p className="product-price">{product.price} kr</p>
          <p className="product-description">{product.description}</p>
          <h3>Välj storlek:</h3>
          <div className="size-selector">
            {product.sizes.map((size, index) => (
              <button
                key={index}
                className={`size-button ${selectedSize === size ? "selected" : ""}`}
                onClick={() => setSelectedSize(size)}
              >
                {size}
              </button>
            ))}
          </div>
          <button className="add-to-cart-button" onClick={handleAddToCart}>
            Lägg i shoppingbagen
          </button>
        </div>
      </div>
    );
  }
  
  export default ProductDetail;