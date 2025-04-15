import React, { useState, useEffect } from "react";
import "./styles/products.css";

const Products = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const fetchProducts = async () => {
      const data = [
        { id: 1, name: "Nike Air Max", price: 120, image: "/img/shoe1.jpg", discount: 10 },
        { id: 2, name: "Adidas Ultraboost", price: 140, image: "/img/shoe2.jpg" },
        { id: 3, name: "Puma RS-X", price: 110, image: "/img/shoe3.jpg", discount: 15 },
      ];
      setProducts(data);
    };

    fetchProducts();
  }, []);

  return (
    <div className="products-page">
      <h2 className="products-header">Our Collection</h2>
      <div className="products-container">
        {products.map((product) => (
          <div className="product-card" key={product.id}>
            {product.discount && (
              <span className="discount-badge">-{product.discount}%</span>
            )}
            <img src={product.image} alt={product.name} className="product-image" />
            <div className="product-details">
              <h3 className="product-title">{product.name}</h3>
              <p className="product-price">
                ${product.price - (product.discount || 0)}
              </p>
            </div>
            <button className="view-details">View Details</button>
            <button className="add-to-cart">Add to Cart</button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Products;