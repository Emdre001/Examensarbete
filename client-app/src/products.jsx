import React, { useState } from 'react';
import './styles/products.css';
import { useNavigate } from 'react-router-dom';
import useCartStore from './CartStore';

// Example product data with gender, color, size, and brand
const products = [
  {
    id: 1,
    name: "Nike Air Max DN Women",
    brand: "Nike",
    price: 1499,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxWomen.png",
    gender: "Kvinnor",
    colors: ["#000000", "#ffffff", "#007aff"],
    sizes: [36, 37, 38, 39, 40],
  },
  {
    id: 2,
    name: "Nike Air Force 1 '07",
    price: 1499,
    brand: "Nike",
    image: process.env.PUBLIC_URL + "/Assets/img/AirForce1.png",
    gender: "Män",
    colors: ["#ffffff", "#888888"],
    sizes: [38, 39, 40, 41, 42, 43, 44],
  },
  {
    id: 3,
    name: "Nike Air Max Plus",
    brand: "Nike",
    price: 2399,
    image: process.env.PUBLIC_URL + "/Assets/img/AirMaxPlus.png",
    gender: "Män",
    colors: ["#000000", "#8B5C2A"],
    sizes: [40, 41, 42, 43, 44],
  },
  {
    id: 4,
    name: "Coming Soon Product",
    brand: "Adidas",
    price: 0,
    image: "",
    gender: "Kvinnor",
    colors: [],
    sizes: [],
  },
];

// Filter options
const brandOptions = ["Nike", "Adidas", "Puma", "Jordan"];
const colorOptions = [
  { name: "Svart", value: "#000000" },
  { name: "Vit", value: "#ffffff" },
  { name: "Grå", value: "#888888" },
  { name: "Blå", value: "#007aff" },
  { name: "Brun", value: "#8B5C2A" },
];
const sizeOptions = Array.from({ length: 9 }, (_, i) => 36 + i);
const priceOptions = [
  { label: "0 kr - 500 kr", min: 0, max: 500 },
  { label: "500 kr - 1000 kr", min: 500, max: 1000 },
  { label: "1000 kr - 1500 kr", min: 1000, max: 1500 },
  { label: "1500+ kr", min: 1500, max: Infinity },
];

export function Products() {
  const navigate = useNavigate();
  const addToCart = useCartStore((state) => state.addToCart);

  // Filter states
  const [selectedBrands, setSelectedBrands] = useState([]);
  const [selectedGender, setSelectedGender] = useState([]);
  const [selectedColors, setSelectedColors] = useState([]);
  const [selectedSizes, setSelectedSizes] = useState([]);
  const [selectedPrices, setSelectedPrices] = useState([]);
  const [sizeDropdownOpen, setSizeDropdownOpen] = useState(false);

  // Filtering logic
  const filteredProducts = products.filter((product) => {
    // Brand filter
    if (selectedBrands.length && !selectedBrands.includes(product.brand)) return false;
    // Gender filter
    if (selectedGender.length && !selectedGender.includes(product.gender)) return false;
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
    alert("Produkten har lagts till i shoppingbagen!");
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

  return (
    <div className="products-layout">
      <aside className="filter-sidebar">
        <h2 style={{ fontSize: "1.6rem", margin: "0 0 24px 0" }}>Filter</h2>
        <nav className="filter-nav">
          <ul>
            <li>Livsstil</li>
            <li>Löpning</li>
            <li>Basket</li>
            <li>Fotboll</li>
            <li>Träning och gym</li>
            <li>Tennis</li>
            <li>Gång</li>
          </ul>
        </nav>
        <hr className="filter-divider" />

        {/* Brand Filter */}
        <div className="filter-section">
          <div className="filter-title">Varumärke</div>
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
          <div className="filter-title">Kön</div>
          <div className="filter-options gender-options">
            <label>
              <input
                type="checkbox"
                checked={selectedGender.includes("Män")}
                onChange={() => toggleGender("Män")}
              /> Män
            </label>
            <label>
              <input
                type="checkbox"
                checked={selectedGender.includes("Kvinnor")}
                onChange={() => toggleGender("Kvinnor")}
              /> Kvinnor
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
            Storlek {sizeDropdownOpen ? "▲" : "▼"}
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
          <div className="filter-title">Färg</div>
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
          <div className="filter-title">Shoppa efter pris</div>
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
            <h2>Vår Kollektion</h2>
            <div className="products-subheader">Upptäck våra senaste sneakers för alla stilar</div>
            <hr className="products-divider" />
          </div>
          <div className="product-grid">
            {filteredProducts.length === 0 && (
              <div style={{ gridColumn: "1/-1", textAlign: "center", color: "#888" }}>
                Inga produkter matchar dina filter.
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
                    {product.price > 0 ? `${product.price} kr` : 'Snart tillgänglig'}
                  </div>
                </div>
                <button
                  className="view-details"
                  onClick={() => navigate(`/products/${product.id}`)}
                >
                  Visa detaljer
                </button>
                <button
                  className="add-to-cart"
                  onClick={() => handleAddToCart(product)}
                  disabled={product.price === 0}
                >
                  Lägg i shoppingbagen
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