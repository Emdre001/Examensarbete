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
    name: "Nike Air Force 1 '07",
    price: 1499,
    image: process.env.PUBLIC_URL + "/Assets/img/AirForce1.png",
  },
  {
    id: 3,
    name: "Nike Air Max Plus",
    price: 2399,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxPlus.png",
  },
  {
    id: 4,
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
    <div className="products-layout">
      <aside className="filter-sidebar">
        <h2 style={{ fontSize: "1.6rem", margin: "0 0 24px 0" }}>Filter</h2>
        <nav className="filter-nav">
          <ul>
            <li>Livsstil</li>
            <li>Jordan</li>
            <li>Löpning</li>
            <li>Basket</li>
            <li>Fotboll</li>
            <li>Träning och gym</li>
            <li>Skateboarding</li>
            <li>Golf</li>
            <li>Tennis</li>
            <li>Gång</li>
          </ul>
        </nav>
        <hr className="filter-divider" />
        <div className="filter-section">
          <button className="filter-toggle">Kön</button>
          {/* Example: */}
          <div className="filter-options">
            <label><input type="checkbox" /> Män</label>
            <label><input type="checkbox" /> Kvinnor</label>
          </div>
        </div>
        <div className="filter-section">
          <button className="filter-toggle">Storlek</button>
          <div className="filter-options">
            <label><input type="checkbox" /> 36</label>
            <label><input type="checkbox" /> 37</label>
            <label><input type="checkbox" /> 38</label>
            {/* ... */}
          </div>
        </div>
        <div className="filter-section">
          <button className="filter-toggle">Färg</button>
          <div className="filter-options">
            <label><input type="checkbox" /> Svart</label>
            <label><input type="checkbox" /> Vit</label>
            {/* ... */}
          </div>
        </div>
        <div className="filter-section">
          <button className="filter-toggle">Shoppa efter pris</button>
          <div className="filter-options">
            <label><input type="checkbox" /> Under 1000 kr</label>
            <label><input type="checkbox" /> 1000-2000 kr</label>
            {/* ... */}
          </div>
        </div>
      </aside>
      <main className="products-main">
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
      </main>
    </div>
  );
}

export default Products;