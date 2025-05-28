import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import './styles/ProductDetail.css';
import useCartStore from './CartStore';
import ToastNotification from './ToastNotification';

const colorOptions = [
  { name: "Red", value: "#ff0000" },
  { name: "Blue", value: "#007aff" },
  { name: "Green", value: "#228B22" },
  { name: "White", value: "#ffffff" },
  { name: "Black", value: "#000000" },
  { name: "Grey", value: "#888888" },
  { name: "Brown", value: "#8B5C2A" },
  { name: "Pink", value: "#ff69b4" },
  { name: "Beige", value: "#f5f5dc" }
];

export function ProductDetail() {
  const { productId } = useParams(); 
  const [product, setProduct] = useState(null);
  const [selectedMedia, setSelectedMedia] = useState(null);
  const [selectedSize, setSelectedSize] = useState(null);
  const [selectedColor, setSelectedColor] = useState(null);
  const [showToast, setShowToast] = useState(false);

  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        console.log("Fetching product with ID:", productId);
        const response = await fetch(`http://localhost:5066/api/Product/Get/${productId}`);
        if (!response.ok) throw new Error('Product not found');
        const data = await response.json();
        setProduct(data);
        setSelectedMedia(data.video || data.images?.[0]);
      } catch (error) {
        console.error('Error fetching product:', error);
      }
    };

    fetchProduct();
  }, [productId]);

  if (!product) {
    return <div>Loading product...</div>;
  }

  const handleAddToCart = () => {
    if (!selectedSize || !selectedColor) {
      alert("Please select size and color before adding to cart.");
      return;
    }

    addToCart({
      productId,
      name: product.name,
      price: product.price,
      image: product.images[0],
      size: selectedSize,
      color: selectedColor,
    });

    setShowToast(true);
    setTimeout(() => setShowToast(false), 1800);
  };

  return (
    <>
      <ToastNotification show={showToast} message="Product added to cart!" />
      <div className="product-detail-container">
        <div className="product-gallery">
          {product.video && (
            <div
              className={`thumbnail${selectedMedia === product.video ? " selected" : ""}`}
              onMouseEnter={() => setSelectedMedia(product.video)}
              style={{ cursor: "pointer" }}
            >
              <video className="thumbnail-video" autoPlay loop muted>
                <source src={product.video} type="video/mp4" />
              </video>
            </div>
          )}
          {product.images.map((img, index) => (
            <img
              key={index}
              src={img}
              alt={`Product ${index}`}
              className={`thumbnail${selectedMedia === img ? " selected" : ""}`}
              onMouseEnter={() => setSelectedMedia(img)}
              style={{ cursor: "pointer" }}
            />
          ))}
        </div>

        <div className="product-main-image">
          {selectedMedia && selectedMedia.endsWith(".mp4") ? (
            <video controls autoPlay className="main-video">
              <source src={selectedMedia} type="video/mp4" />
            </video>
          ) : (
            <img src={selectedMedia} alt="Selected Product" className="main-image" />
          )}
        </div>

        <div className="product-info">
          <h1>{product.name}</h1>
          <p className="product-price">{product.price} kr</p>
          <p className="product-description">{product.description}</p>

          <h3>Select size:</h3>
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

          <h3>Select color:</h3>
          <div className="color-circles">
            {colorOptions
              .filter(color => product.colors?.includes(color.value))
              .map((color) => (
                <div
                  key={color.value}
                  className="color-circle-wrapper"
                  onClick={() => setSelectedColor(color.value)}
                >
                  <span
                    className={`color-circle${selectedColor === color.value ? " selected" : ""}`}
                    style={{ backgroundColor: color.value }}
                    title={color.name}
                  >
                    {selectedColor === color.value && (
                      <span className="color-checkmark">&#10003;</span>
                    )}
                  </span>
                  <span className="color-label">{color.name}</span>
                </div>
              ))}
          </div>
          <button className="add-to-cart-button" onClick={handleAddToCart}>
            Add to cart
          </button>
        </div>
      </div>
    </>
  );
}

export default ProductDetail;
