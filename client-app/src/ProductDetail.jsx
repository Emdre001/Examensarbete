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

const productImages = {
  "79eee69d-a383-4303-b0be-021070a57d45": [ '/Assets/img/NewBalanceBasic.jpg', ],
  "1dbcfcb3-090f-45f0-a78d-3eb7da6c5468": [ '/Assets/img/NewBalanceBeige.jpg', ],
  "cb45cbe5-600f-41bc-84fc-81997c96e5f3": [ '/Assets/img/AIR force.jpg', ],
  "7fb89bfd-a86e-4cfa-8eae-3acbde1abbfd": [ '/Assets/img/AirMaxPlus.webp', ],
  "5d4c227b-fcca-42f3-b0b1-b1a89de47ef5": [
  '/Assets/Video/AirMaxWomenVid.mp4',
  '/Assets/img/AirMaxWomen.png',
  '/Assets/img/AirMaxWomen2.png',
  '/Assets/img/AirMaxWomen3.png',
  '/Assets/img/AirMaxWomen4.png',
  '/Assets/img/AirMaxWomen5.png',
  '/Assets/img/AirMaxWomen6.png',
  ],
  "59d157e9-7e82-4254-8163-341acef2cc51": [ '/Assets/img/UggsLow.jpg', ],
  "02ac4c55-eef5-4722-b19d-877e917cd7cb": [ '/Assets/img/uggMiniSvart.jpg', ],
  "134890f0-fa0e-4d71-826b-829cb5deab30": [ '/Assets/img/AdidasCampus.jpg', ],
  "fe07d1b5-af4f-4392-ad03-a03cd825dd22": [ 'Assets/img/BabyDunk.jpg', ],
  "eb9aea7c-a7fa-4ed5-8799-4dbe130f8d76": [ '/Assets/img/NikeGreen.jpg', ],
  "73d20b7a-1a01-42b7-baf6-58070f3e1954": [ '/Assets/img/NikeDunkBlue.jpg', ],
  "f2ebfe0c-080d-4951-b6da-55bbb2b7337c": [ '/Assets/img/NikebabyPink.jpg', ],
  "5f21de1b-b31c-4b47-9192-4cb8a9d09213": [ '/Assets/img/NikePanda.jpg', ],
  "ca3dbabf-c551-4bae-82d7-feafa6c0e3bb": [ '/Assets/img/AxelArigato.jpg', ],
  "f7e815f3-06a4-4d56-a6ad-d2c49c5849b7": [ '/Assets/img/Arigattooo.jpg', ],
  "a502ef7b-b3d0-4fb0-949a-c4aabae18060": [ '/Assets/img/Dior.jpg', ],
  //"ProductID here": 'Image Link Here',

};

export function ProductDetail() {
  const { productId } = useParams(); 
  const [product, setProduct] = useState(null);
  const [selectedSize, setSelectedSize] = useState(null);
  const [selectedColor, setSelectedColor] = useState(null);
  const [showToast, setShowToast] = useState(false);
  const [sizes, setSizes] = useState([]);


  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
  const fetchProduct = async () => {
    try {
      const response = await fetch(`http://localhost:5066/api/Product/Get/${productId}`);
      if (!response.ok) throw new Error('Product not found');
      const data = await response.json();
      setProduct(data);

      const sizeValues = data.productSizes?.$values || [];
      const fetchedSizes = await Promise.all(
      sizeValues.map(async (s) => {
        const res = await fetch(`http://localhost:5066/api/Size/GetSizeById/${s.sizeId}`);
        const sizeData = await res.json();
        return {
          sizeId: s.sizeId,
          sizeValue: sizeData.sizeValue
        };
      })
    );

    // Sort sizes numerically (or lexicographically if needed)
    fetchedSizes.sort((a, b) => {
      const aVal = parseFloat(a.sizeValue);
      const bVal = parseFloat(b.sizeValue);
      return aVal - bVal;
    });

    setSizes(fetchedSizes);


      setSizes(fetchedSizes);
    } catch (error) {
      console.error('Error fetching product or sizes:', error);
    }
  };

  fetchProduct();
}, [productId]);


  if (!product) {
    return <div>Loading product...</div>;
  }

  const extractedColors = product.colors?.$values?.map(c => c.colorName) || [];
  const extractedSizes = product.productSizes?.$values?.map(s => s.sizeId) || [];

  const handleAddToCart = () => {
    if (!selectedSize || !selectedColor) {
      alert("Please select size and color before adding to cart.");
      return;
    }

    addToCart({
      productId: product.productId,
      name: product.productName,
      price: product.productPrice,
      image: "", // no image in API response
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
      <div className="product-main-image-gallery" tabIndex="0">
      {(productImages[product.productId] || ['/placeholder-image.png']).map((media, index) => {
        const isVideo = media.endsWith('.mp4') || media.endsWith('.webm');

        return isVideo ? (
          <video
          key={index}
          autoPlay
          muted
          loop
          playsInline
          className="main-image"
        >
          <source src={media} type="video/mp4" />
          Your browser does not support the video tag.
        </video>        
        ) : (
          <img
            key={index}
            src={media}
            alt={`${product.productName} bild ${index + 1}`}
            className="main-image"
          />
        );
      })}
    </div>

        <div className="product-info">
          <h1>{product.productName}</h1>
          <p className="product-price">{product.productPrice} kr</p>
          <p className="product-description">{product.productDescription}</p>

          <h3>Select size:</h3>
            <div className="size-selector">
              {sizes.map(({ sizeId, sizeValue }) => (
                <button
                  key={sizeId}
                  className={`size-button ${selectedSize === sizeId ? "selected" : ""}`}
                  onClick={() => setSelectedSize(sizeId)}
                >
                  {sizeValue}
                </button>
              ))}
            </div>

          <h3>Select color:</h3>
          <div className="color-circles">
            {colorOptions
              .filter(color => extractedColors.includes(color.name))
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