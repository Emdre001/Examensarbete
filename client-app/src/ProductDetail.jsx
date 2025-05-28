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
        <div className="product-main-image">
          <img
            src="/placeholder-image.png"
            alt="Product"
            className="main-image"
          />
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