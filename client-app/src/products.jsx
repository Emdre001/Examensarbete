import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import ReactPlayer from "react-player";
import "./styles/products.css";
import useCartStore from "./CartStore";
import toast from "react-hot-toast";

const Products = () => {
  const [products, setProducts] = useState([]);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const navigate = useNavigate();
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    const fetchProducts = async () => {
      const data = [
        { 
          id: 1, 
          name: "Nike Air Max Dn", 
          price: 120, 
          images: [
            "/img/AirMaxWomen.png",
            "/img/AirMaxWomen1.png",
            "/img/AirMaxWomen2.png",
            "/img/AirMaxWomen3.png",
            "/img/AirMaxWomen4.png",
            "/img/AirMaxWomen5.png",
            "/img/AirMaxWomen6.png"
          ],
          video: "/img/AirMaxWomenVid.mp4",
          discount: 10 
        },
        { 
          id: 2, 
          name: "Adidas Ultraboost", 
          price: 140, 
          images: ["/img/shoe2.jpg"],
          video: null
        },
        { 
          id: 3, 
          name: "Puma RS-X", 
          price: 110, 
          images: ["/img/shoe3.jpg"],
          video: null,
          discount: 15 
        },
      ];
      setProducts(data);
    };

    fetchProducts();
  }, []);

  const handleAddToCart = (product) => {
    addToCart(product);
    toast.success(`${product.name} added to cart`);
  };

  const viewDetails = (id) => {
    navigate(`/products/${id}`);
  };

  const nextImage = (product) => {
    setCurrentImageIndex((prevIndex) => 
      (prevIndex + 1) % product.images.length
    );
  };

  const prevImage = (product) => {
    setCurrentImageIndex((prevIndex) => 
      (prevIndex - 1 + product.images.length) % product.images.length
    );
  };

  return (
    <div className="products-page">
      <h2 className="products-header">Our Collection</h2>
      <div className="products-container">
        {products.map((product) => {
          const discountedPrice = product.discount
            ? (product.price * (1 - product.discount / 100)).toFixed(2)
            : product.price;

          return (
            <div className="product-card" key={product.id}>
              {product.discount && (
                <span className="discount-badge">-{product.discount}%</span>
              )}
              
              <div className="media-container">
                {product.video ? (
                  <ReactPlayer
                    url={product.video}
                    width="100%"
                    height="200px"
                    controls={true}
                    playing={false}
                    light={product.images[0]}
                  />
                ) : (
                  <>
                    <img 
                      src={product.images[currentImageIndex]} 
                      alt={product.name} 
                      className="product-image" 
                    />
                    {product.images.length > 1 && (
                      <div className="image-nav">
                        <button onClick={() => prevImage(product)}>&lt;</button>
                        <button onClick={() => nextImage(product)}>&gt;</button>
                      </div>
                    )}
                  </>
                )}
              </div>
              
              <div className="product-details">
                <h3 className="product-title">{product.name}</h3>
                <p className="product-price">
                  ${discountedPrice}
                  {product.discount && (
                    <span className="original-price">${product.price}</span>
                  )}
                </p>
              </div>
              
              <div className="product-buttons">
                <button className="view-details" onClick={() => viewDetails(product.id)}>
                  View Details
                </button>
                <button className="add-to-cart" onClick={() => handleAddToCart(product)}>
                  Add to Cart
                </button>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
};

export default Products;