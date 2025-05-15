import React from 'react';
import './styles/products.css';
import { useNavigate } from 'react-router-dom';
import useCartStore from './CartStore';

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
    image: "",
  },
];

export function Products() {
  const navigate = useNavigate();
  const addToCart = useCartStore((state) => state.addToCart);

  const handleAddToCart = (product) => {
    addToCart({
      id: product.id,
      name: product.name,
      price: product.price,
      image: product.image,
      size: null,
    });
    alert("Produkten har lagts till i shoppingbagen!");
  };

  return (
    <div className="page-wrapper">
      <h2 className="products-header">Our Collection</h2>
      <div className="product-grid">
        {products.map((product) => (
          <div className="product-card" key={product.id}>
            {product.image && (
              <img src={product.image} alt={product.name} className="product-image" />
            )}
            <div className="product-details">
              <div className="product-title">{product.name}</div>
              <div className="product-price">
                {product.price > 0 ? `${product.price} kr` : 'Snart tillgänglig'}
              </div>
            </div>
            <button
              className="view-details"
              onClick={() => navigate(`/products/${product.id}`)}
            >
              View Details
            </button>
            <button
              className="add-to-cart"
              onClick={() => handleAddToCart(product)}
              disabled={product.price === 0}
            >
              Add to Cart
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}

export default Products;