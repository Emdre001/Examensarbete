import React from 'react';
import './styles/products.css';
import { Link } from 'react-router-dom';

const products = [
    {
        id: 1,
        name: "Nike Air Max DN Women",
        price: 1517,
        image: process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen.png",
    },
    // Template for future products
    {
        id: 2,
        name: "Coming Soon Product",
        price: 0,
        image: "", // Add image path later
    },
];

export function Products() {
    return (
        <div className="products-container">
            {products.map((product) => (
                <div key={product.id} className="product-card">
                    <Link to={`/products/${product.id}`}>
                        {product.image && (
                            <img src={product.image} alt={product.name} className="product-image" />
                        )}
                        <h2 className="product-name">{product.name}</h2>
                        <p className="product-price">{product.price > 0 ? `${product.price} kr` : 'Snart tillgänglig'}</p>
                    </Link>
                </div>
            ))}
        </div>
    );
}

export default Products;
