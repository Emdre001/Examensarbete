import React from 'react';
import './styles/products.css';
import { Link } from 'react-router-dom';

const products = [
  {
    id: 1,
    name: "Nike Air Max DN Women",
    price: 1499,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen.png",
  },
  {
    id: 2,
    name: "Coming Soon Product",
    price: 0,
    image: "", // Add image path later
  },
];

export function Products() {
  return (
    <div className="page-wrapper">
      <h2 className="products-header">Our Collection</h2>
      <div className="product-grid">
        {products.map((product) => (
          <div className="product-card" key={product.id}>
            {product.discount && (
              <span className="discount-badge">-{product.discount}%</span>
            )}
            <Link to={`/products/${product.id}`}>
              {product.image && (
                <img src={product.image} alt={product.name} className="product-image" />
              )}
              <div className="product-details">
                <h3 className="product-title">{product.name}</h3>
                <p className="product-price">
                  {product.price > 0 ? `${product.price} kr` : 'Snart tillgänglig'}
                </p>
              </div>
            </Link>
            <button className="view-details">View Details</button>
            <button className="add-to-cart">Add to Cart</button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Products;