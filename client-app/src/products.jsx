import React, { useState, useEffect, useRef } from 'react';
import './styles/products.css';
import { useNavigate, useLocation } from 'react-router-dom';
import useCartStore from './CartStore';
import ToastNotification from './ToastNotification';

const brandOptions = [
  "Nike", "Adidas", "New Balance", "Axel Arigato", "Ugg", "Dior"
];
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
const sizeOptions = Array.from({ length: 11 }, (_, i) => 35 + i);
const categoryOptions = [
  "Sneaker", "Boots"
];
const priceOptions = [
  { label: "0 kr - 1000 kr", min: 0, max: 1000 },
  { label: "1000 kr - 2000 kr", min: 1000, max: 2000 },
  { label: "2000 kr - 5000 kr", min: 2000, max: 5000 },
  { label: "5000+ kr", min: 5000, max: Infinity },
];

// Product data 
const products = [
  {
    id: 1,
    name: "Nike Air Max DN Women",
    brand: "Nike",
    price: 1499,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen.png",
    gender: "Women",
    category: "Sneaker",
    colors: ["#ffffff", "#ff69b4", "#888888"], // White/Pink/Grey
    sizes: [36, 37, 38, 39, 40],
  },
  {
    id: 2,
    name: "Nike Air Force 1 '07",
    brand: "Nike",
    price: 1499,
    image: process.env.PUBLIC_URL + "/Assets/img/AIR force.jpg",
    gender: "Men",
    category: "Sneaker",
    colors: ["#ffffff", ],
    sizes: [38, 39, 40, 41, 42, 43, 44],
  },
  {
    id: 3,
    name: "Nike Air Max Plus",
    brand: "Nike",
    price: 2399,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxPlus.webp",
    gender: "Men",
    category: "Sneaker",
    colors: ["#000000", "#8B5C2A"],
    sizes: [40, 41, 42, 43, 44],
  },
  {
    id: 4,
    name: "Axel arigato Area Lo Sneaker",
    brand: "Axel Arigato",
    price: 2565,
    image: process.env.PUBLIC_URL + "/Assets/img/AxelArigato.jpg",
    gender: "Unisex",
    category: "Sneaker",
    colors: ["#ffffff", "#f5f5dc"], // White/Beige
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 5,
    name: "Axel arigato Clean 90 Mocha",
    brand: "Axel Arigato",
    price: 3723,
    image: process.env.PUBLIC_URL + "/Assets/img/Arigattooo.jpg",
    gender: "Unisex",
    category: "Sneaker",
    colors: ["#000000", "#ffffff"], // Black/White
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 6,
    name: "Walk'n'Dior Platform Sneaker",
    brand: "Dior",
    price: 10232,
    image: process.env.PUBLIC_URL + "/Assets/img/Dior.jpg",
    gender: "Women",
    category: "Sneaker",
    colors: ["#ffffff", "#f5f5dc"], // White/Beige
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 7,
    name: "New Balance 530",
    brand: "New Balance",
    price: 1270,
    image: process.env.PUBLIC_URL + "/Assets/img/NewBalance.jpg",
    gender: "Women",
    category: "Sneaker",
    colors: ["#ffffff"], // White
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 8,
    name: "New Balance 530 Beige",
    brand: "New Balance",
    price: 1070,
    image: process.env.PUBLIC_URL + "/Assets/img/NewBalanceBeige.jpg",
    gender: "Women",
    category: "Sneaker",
    colors: ["#f5f5dc"], // Beige
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 9,
    name: "Nike Dunk Low Pink",
    brand: "Nike",
    price: 799,
    image: process.env.PUBLIC_URL + "/Assets/img/NikebabyPink.jpg",
    gender: "Women",
    category: "Sneaker",
    colors: ["#ff69b4"], // Pink
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 10,
    name: "Nike Dunk Low Olive Green",
    brand: "Nike",
    price: 1045,
    image: process.env.PUBLIC_URL + "/Assets/img/NikeGreen.jpg",
    gender: "Men",
    category: "Sneaker",
    colors: ["#228B22", "#ffffff"], // Olive Green/White
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 11,
    name: "Nike Dunks Low Panda",
    brand: "Nike",
    price: 1245,
    image: process.env.PUBLIC_URL + "/Assets/img/NikePanda.jpg",
    gender: "Unisex",
    category: "Sneaker",
    colors: ["#000000", "#ffffff"], // Black/White
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 12,
    name: "Ugg Mini",
    brand: "Ugg",
    price: 2745,
    image: process.env.PUBLIC_URL + "/Assets/img/uggMiniSvart.jpeg",
    gender: "Women",
    category: "Boots",
    colors: ["#000000"], // Black
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 13,
    name: "Ugg Ultra Mini",
    brand: "Ugg",
    price: 2859,
    image: process.env.PUBLIC_URL + "/Assets/img/UggsLow.jpg",
    gender: "Women",
    category: "Boots",
    colors: ["#f5f5dc"], // Beige
    sizes: [36, 37, 38, 39, 40],
  },
  {
    id: 14,
    name: "Adidas Campus",
    brand: "Adidas",
    price: 1355,
    image: process.env.PUBLIC_URL + "/Assets/img/AdidasCampus.jpg",
    gender: "Unisex",
    category: "Sneaker",
    colors: ["#888888", "#ffffff"], // Grey/White
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
  {
    id: 15,
    name: "Nike Dunk Low Baby blue",
    brand: "Nike",
    price: 1170,
    image: process.env.PUBLIC_URL + "/Assets/img/NikeDunkBlue.jpg",
    gender: "Unisex",
    category: "Sneaker",
    colors: ["#007aff"], // Blue
    sizes: [36, 37, 38, 39, 40, 41, 42, 43, 44, 45],
  },
];

export function Products() {
  const navigate = useNavigate();
  const location = useLocation();
  const addToCart = useCartStore((state) => state.addToCart);

  // Toast state
  const [showToast, setShowToast] = useState(false);
  const toastTimeout = useRef(null);

  // Parse filters from URL
  const params = new URLSearchParams(location.search);
  const urlGender = params.get('gender');
  const urlBrand = params.get('brand');
  const urlCategory = params.get('category');
  const urlSearch = params.get('search')?.toLowerCase() || '';

  // Filter states
  const [selectedBrands, setSelectedBrands] = useState([]);
  const [selectedGender, setSelectedGender] = useState([]);
  const [selectedColors, setSelectedColors] = useState([]);
  const [selectedSizes, setSelectedSizes] = useState([]);
  const [selectedPrices, setSelectedPrices] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);
  const [sizeDropdownOpen, setSizeDropdownOpen] = useState(false);

  // Set filters from URL on mount or when URL changes
  useEffect(() => {
    // Gender
    if (urlGender === 'men') setSelectedGender(['Men']);
    else if (urlGender === 'women') setSelectedGender(['Women']);
    else setSelectedGender([]);

    // Brand
    if (urlBrand && brandOptions.includes(urlBrand)) setSelectedBrands([urlBrand]);
    else setSelectedBrands([]);

    // Category
    if (urlCategory && categoryOptions.includes(urlCategory)) setSelectedCategories([urlCategory]);
    else setSelectedCategories([]);
  }, [urlGender, urlBrand, urlCategory]);

  // Filtering logic
  const filteredProducts = products.filter((product) => {
    // Brand filter
    if (selectedBrands.length && !selectedBrands.includes(product.brand)) return false;
    // Gender filter
    if (selectedGender.length && !selectedGender.includes(product.gender)) return false;
    // Category filter
    if (selectedCategories.length && !selectedCategories.includes(product.category)) return false;
    // Color filter
    if (selectedColors.length && !product.colors.some((c) => selectedColors.includes(c))) return false;
    // Size filter
    if (selectedSizes.length && !product.sizes.some((s) => selectedSizes.includes(s))) return false;
    // Price filter
    if (selectedPrices.length) {
      const inRange = selectedPrices.some(({ min, max }) =>
        product.price >= min && product.price < max
      );
      if (!inRange) return false;
    }
    // Search filter
    if (urlSearch && !product.name.toLowerCase().includes(urlSearch)) return false;
    return true;
  });

  const handleAddToCart = (product) => {
    addToCart({
      id: product.id,
      name: product.name,
      price: product.price,
      image: product.image,
      size: null,
    });
    setShowToast(true);
    if (toastTimeout.current) clearTimeout(toastTimeout.current);
    toastTimeout.current = setTimeout(() => setShowToast(false), 1800);
  };

  // Toggle helpers
  const toggleBrand = (brand) => {
    setSelectedBrands((prev) =>
      prev.includes(brand) ? prev.filter((b) => b !== brand) : [...prev, brand]
    );
  };
  const toggleGender = (gender) => {
    setSelectedGender((prev) =>
      prev.includes(gender) ? prev.filter((g) => g !== gender) : [...prev, gender]
    );
  };
  const toggleColor = (color) => {
    setSelectedColors((prev) =>
      prev.includes(color) ? prev.filter((c) => c !== color) : [...prev, color]
    );
  };
  const toggleSize = (size) => {
    setSelectedSizes((prev) =>
      prev.includes(size) ? prev.filter((s) => s !== size) : [...prev, size]
    );
  };
  const togglePrice = (range) => {
    setSelectedPrices((prev) =>
      prev.some((r) => r.min === range.min && r.max === range.max)
        ? prev.filter((r) => !(r.min === range.min && r.max === range.max))
        : [...prev, range]
    );
  };
  const toggleCategory = (category) => {
    setSelectedCategories((prev) =>
      prev.includes(category) ? prev.filter((c) => c !== category) : [...prev, category]
    );
  };

  return (
    <>
      <ToastNotification show={showToast} message="Product added to cart!" />
      <div className="products-layout">
        <aside className="filter-sidebar">
          <h2 style={{ fontSize: "1.6rem", margin: "0 0 24px 0" }}>Filter</h2>
          <nav className="filter-nav">
            <ul>
              {categoryOptions.map((cat) => (
                <li
                  key={cat}
                  style={{
                    fontWeight: selectedCategories.includes(cat) ? 700 : 500,
                    color: selectedCategories.includes(cat) ? "#7d2ae8" : undefined,
                    cursor: "pointer"
                  }}
                  onClick={() => toggleCategory(cat)}
                >
                  {cat}
                </li>
              ))}
            </ul>
          </nav>
          <hr className="filter-divider" />

          {/* Brand Filter */}
          <div className="filter-section">
            <div className="filter-title">Brand</div>
            <div className="filter-options brand-options">
              {brandOptions.map((brand) => (
                <label key={brand}>
                  <input
                    type="checkbox"
                    checked={selectedBrands.includes(brand)}
                    onChange={() => toggleBrand(brand)}
                  />{" "}
                  {brand}
                </label>
              ))}
            </div>
          </div>

          {/* Gender Filter */}
          <div className="filter-section">
            <div className="filter-title">Gender</div>
            <div className="filter-options gender-options">
              <label>
                <input
                  type="checkbox"
                  checked={selectedGender.includes("Men")}
                  onChange={() => toggleGender("Men")}
                /> Men
              </label>
              <label>
                <input
                  type="checkbox"
                  checked={selectedGender.includes("Women")}
                  onChange={() => toggleGender("Women")}
                /> Women
              </label>
              <label>
                <input
                  type="checkbox"
                  checked={selectedGender.includes("Unisex")}
                  onChange={() => toggleGender("Unisex")}
                /> Unisex
              </label>
            </div>
          </div>

          {/* Size Filter as Dropdown */}
          <div className="filter-section">
            <div
              className="filter-title size-title"
              onClick={() => setSizeDropdownOpen((open) => !open)}
              style={{ cursor: "pointer", userSelect: "none" }}
            >
              Size {sizeDropdownOpen ? "▲" : "▼"}
            </div>
            {sizeDropdownOpen && (
              <div className="filter-options size-options">
                {sizeOptions.map((size) => (
                  <label key={size}>
                    <input
                      type="checkbox"
                      checked={selectedSizes.includes(size)}
                      onChange={() => toggleSize(size)}
                    />{" "}
                    {size}
                  </label>
                ))}
              </div>
            )}
          </div>

          {/* Color Filter */}
          <div className="filter-section">
            <div className="filter-title">Color</div>
            <div className="filter-options color-options">
              {colorOptions.map((color) => (
                <div key={color.value} className="color-circle-label">
                  <span
                    className={`color-circle-filter${selectedColors.includes(color.value) ? " selected" : ""}`}
                    style={{ backgroundColor: color.value }}
                    title={color.name}
                    onClick={() => toggleColor(color.value)}
                  >
                    {selectedColors.includes(color.value) && (
                      <span className="color-checkmark">&#10003;</span>
                    )}
                  </span>
                  <span className="color-name">{color.name}</span>
                </div>
              ))}
            </div>
          </div>

          {/* Price Filter */}
          <div className="filter-section">
            <div className="filter-title">Price</div>
            <div className="filter-options price-options">
              {priceOptions.map((range) => (
                <label key={range.label}>
                  <input
                    type="checkbox"
                    checked={selectedPrices.some((r) => r.min === range.min && r.max === range.max)}
                    onChange={() => togglePrice(range)}
                  /> {range.label}
                </label>
              ))}
            </div>
          </div>
        </aside>
        <main className="products-main">
          <div className="page-wrapper">
            <div className="products-header">
              <h2>Our Collection</h2>
              <div className="products-subheader">Discover our latest sneakers for every style</div>
              <hr className="products-divider" />
            </div>
            <div className="product-grid">
              {filteredProducts.length === 0 && (
                <div style={{ gridColumn: "1/-1", textAlign: "center", color: "#888" }}>
                  No products match your filters.
                </div>
              )}
              {filteredProducts.map((product) => (
                <div className="product-card" key={product.id}>
                  {product.image && (
                    <img src={product.image} alt={product.name} className="product-image" />
                  )}
                  <div className="product-details">
                    <div className="product-title">{product.name}</div>
                    <div className="product-price">
                      {product.price > 0 ? `${product.price} kr` : 'Coming soon'}
                    </div>
                  </div>
                  <button
                    className="view-details"
                    onClick={() => navigate(`/products/${product.id}`)}
                  >
                    View details
                  </button>
                  <button
                    className="add-to-cart"
                    onClick={() => handleAddToCart(product)}
                    disabled={product.price === 0}
                  >
                    Add to cart
                  </button>
                </div>
              ))}
            </div>
          </div>
        </main>
      </div>
    </>
  );
}

export default Products;