import React, { useState } from 'react';
import { useParams } from 'react-router-dom';
import './styles/ProductDetail.css';
import useCartStore from './CartStore';
import ToastNotification from './ToastNotification';

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
    colors: ["#ffffff", "#ff69b4", "#888888" ], // White/Pink/Grey
    description: "Nike Air Max DN Womens shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
  },
  2: {
    name: "Nike Air Force 1 '07",
    price: 1499,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/AIR force.jpg"
    ],
    video: "",
    sizes: ["EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44"],
    colors: ["#ffffff"],
    description: "Nike Air Force 1 '07 Man shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
  },
  3: {
    name: "Nike Air Max Plus",
    price: 2399,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/AirMaxPlus.webp",
    ],
    video: "",
    sizes: ["EU 40", "EU 41", "EU 42", "EU 43", "EU 44"],
    colors: ["#000000", "#8B5C2A"],
    description: "Nike Air Max Plus Man shoe combine comfort and style with a sleek design and responsive cushioning. Perfect for everyday wear.",
  },
   4: {
    name: "Axel arigato Area Lo Sneaker",
    price: 2565,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/AxelArigato.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#ffffff", "#f5f5dc"], // White/Beige
    description: "Axel arigato Area Lo Sneaker in white/beige.",
  },
  5: {
    name: "Axel arigato Clean 90 Mocha",
    price: 3723,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/Arigattooo.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#000000", "#ffffff"], // Black/White
    description: "Axel arigato Clean 90 Mocha in black/white.",
  },
  6: {
    name: "Walk'n'Dior Platform Sneaker",
    price: 10232,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/Dior.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#ffffff", "#f5f5dc"], // White/Beige
    description: "Walk'n'Dior Platform Sneaker in white/beige.",
  },
  7: {
    name: "New Balance 530",
    price: 1270,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NewBalance.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#ffffff"], // White
    description: "New Balance 530 in white.",
  },
  8: {
    name: "New Balance 530 Beige",
    price: 1070,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NewBalanceBeige.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#f5f5dc"], // Beige
    description: "New Balance 530 Beige.",
  },
  9: {
    name: "Nike Dunk pink",
    price: 799,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NikebabyPink.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#ff69b4"], // Pink
    description: "Nike Dunk pink.",
  },
  10: {
    name: "Nike Dunks Olive green/white",
    price: 1045,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NikeGreen.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#228B22", "#ffffff"], // Olive Green/White
    description: "Nike Dunks Olive green/white.",
  },
  11: {
    name: "Nike Dunks Low Panda",
    price: 1245,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NikePanda.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#000000", "#ffffff"], // Black/White
    description: "Nike Dunks Low Panda black and white.",
  },
  12: {
    name: "Ugg mini",
    price: 2745,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/uggMiniSvart.jpeg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#000000"], // Black
    description: "Ugg mini black.",
  },
  13: {
    name: "Ugg ultra mini",
    price: 2859,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/UggsLow.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#f5f5dc"], // Beige
    description: "Ugg ultra mini beige.",
  },
  14: {
    name: "Adidas Campus",
    price: 1355,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/AdidasCampus.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#888888", "#ffffff"], // Grey/White
    description: "Adidas Campus grey/white.",
  },
  15: {
    name: "Nike Baby blue",
    price: 1170,
    images: [
      process.env.PUBLIC_URL + "/Assets/img/NikeDunkBlue.jpg"
    ],
    video: "",
    sizes: ["EU 36", "EU 37", "EU 38", "EU 39", "EU 40", "EU 41", "EU 42", "EU 43", "EU 44", "EU 45"],
    colors: ["#007aff"], // Blue
    description: "Nike Baby blue shoe combine perfect comfort with a cool aesthetic vibe and responsive cushioning. Perfect for everyday.",
  },
  
};

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
  const { id } = useParams();
  const product = productData[id];
  const addToCart = useCartStore((state) => state.addToCart);
  const [showToast, setShowToast] = useState(false);

  // Start with first image or video as selected
  const [selectedMedia, setSelectedMedia] = useState(
    product?.video ? product.video : product?.images[0]
  );
  const [selectedSize, setSelectedSize] = useState(null);
  const [selectedColor, setSelectedColor] = useState(null);

  if (!product) {
    return <div>Product not found.</div>;
  }

  const handleAddToCart = () => {
    if (!selectedSize) {
      alert("Please select a size before adding to cart.");
      return;
    }
    if (!selectedColor) {
      alert("Please select a color before adding to cart.");
      return;
    }

    addToCart({
      id,
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
        {/* Show video as first thumbnail if it exists */}
        {product.video && (
          <div
            className={`thumbnail${selectedMedia === product.video ? " selected" : ""}`}
            onMouseEnter={() => setSelectedMedia(product.video)}
            style={{ cursor: "pointer" }}
          >
            <video
              className="thumbnail-video"
              autoPlay
              loop
              muted
              style={{ width: "100%", height: "100%", objectFit: "contain" }}
            >
              <source src={product.video} type="video/mp4" />
              Your browser does not support the video tag.
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
            Your browser does not support the video tag.
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