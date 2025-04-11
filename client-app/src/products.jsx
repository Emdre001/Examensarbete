import React, { useState, useEffect } from "react";
import "./styles/products.css"; // Ensure correct path

const Products = () => {
    const [products, setProducts] = useState([]);

    // Fetch products (Replace with actual API)
    useEffect(() => {
        const fetchProducts = async () => {
            // Example products (Replace with real API call)
            const data = [
                { id: 1, name: "Nike Air Max", price: 120, image: "/img/shoe1.jpg" },
                { id: 2, name: "Adidas Ultraboost", price: 140, image: "/img/shoe2.jpg" },
                { id: 3, name: "Puma RS-X", price: 110, image: "/img/shoe3.jpg" },
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
                        <img src={product.image} alt={product.name} className="product-image" />
                        <div className="product-details">
                            <h3 className="product-title">{product.name}</h3>
                            <p className="product-price">${product.price}</p>
                        </div>
                        <button className="add-to-cart">Add to Cart</button>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Products;
