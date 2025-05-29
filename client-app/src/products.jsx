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

const productImages = {
  "79eee69d-a383-4303-b0be-021070a57d45": '/Assets/img/NewBalanceBasic.jpg',
  "1dbcfcb3-090f-45f0-a78d-3eb7da6c5468": '/Assets/img/NewBalanceBeige.jpg',
  "cb45cbe5-600f-41bc-84fc-81997c96e5f3": '/Assets/img/AIR force.jpg',
  "7fb89bfd-a86e-4cfa-8eae-3acbde1abbfd": '/Assets/img/AirMaxPlus.webp',
  "5d4c227b-fcca-42f3-b0b1-b1a89de47ef5": '/Assets/img/AirMaxWomen.png',
  "59d157e9-7e82-4254-8163-341acef2cc51": '/Assets/img/UggsLow.jpg',
  "02ac4c55-eef5-4722-b19d-877e917cd7cb": '/Assets/img/uggMiniSvart.jpg',
  "134890f0-fa0e-4d71-826b-829cb5deab30": '/Assets/img/AdidasCampus.jpg',
  "fe07d1b5-af4f-4392-ad03-a03cd825dd22": 'Assets/img/BabyDunk.jpg',
  "eb9aea7c-a7fa-4ed5-8799-4dbe130f8d76": '/Assets/img/NikeGreen.jpg',
  "73d20b7a-1a01-42b7-baf6-58070f3e1954": '/Assets/img/NikeDunkBlue.jpg',
  "f2ebfe0c-080d-4951-b6da-55bbb2b7337c": '/Assets/img/NikebabyPink.jpg',
  "5f21de1b-b31c-4b47-9192-4cb8a9d09213": '/Assets/img/NikePanda.jpg',
  "ca3dbabf-c551-4bae-82d7-feafa6c0e3bb": '/Assets/img/AxelArigato.jpg',
  "f7e815f3-06a4-4d56-a6ad-d2c49c5849b7": '/Assets/img/Arigattooo.jpg',
  "a502ef7b-b3d0-4fb0-949a-c4aabae18060": '/Assets/img/Dior.jpg',
  //"ProductID here": 'Image Link Here',

};

function resolveRefs(obj) {
  const byId = {};
  const refs = [];

  function recurse(obj) {
    if (obj && typeof obj === 'object') {
      if (obj.$id) {
        byId[obj.$id] = obj;
      }

      if (obj.$ref) {
        refs.push(obj);
      }

      for (const key in obj) {
        if (key !== '$id' && key !== '$ref') {
          obj[key] = recurse(obj[key]);
        }
      }
    }

    return obj;
  }

  const root = recurse(obj);

  for (const ref of refs) {
    const resolved = byId[ref.$ref];
    Object.assign(ref, resolved);
    delete ref.$ref;
  }

  return root;
}


export function Products() {
  const navigate = useNavigate();
  const location = useLocation();
  const addToCart = useCartStore((state) => state.addToCart);

  const [products, setProducts] = useState([]); // ← fetched products here
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [showToast, setShowToast] = useState(false);
  const toastTimeout = useRef(null);

  // Filters from URL
  const params = new URLSearchParams(location.search);
  const urlGender = params.get('gender');
  const urlBrand = params.get('brand');
  const urlCategory = params.get('category');
  const urlSearch = params.get('search')?.toLowerCase() || '';

  const [selectedBrands, setSelectedBrands] = useState([]);
  const [selectedGender, setSelectedGender] = useState([]);
  const [selectedColors, setSelectedColors] = useState([]);
  const [selectedSizes, setSelectedSizes] = useState([]);
  const [selectedPrices, setSelectedPrices] = useState([]);
  const [selectedCategories, setSelectedCategories] = useState([]);
  const [sizeDropdownOpen, setSizeDropdownOpen] = useState(false);

  // Fetch products from backend
  useEffect(() => {
  async function fetchProducts() {
    setLoading(true);
    setError(null);
    try {
      const response = await fetch('http://localhost:5066/api/Product/GetAll');
      const data = await response.json();
      const resolved = resolveRefs(data);

      const formatted = resolved?.$values?.map(product => ({
        id: product.productId,
        name: product.productName,
        type: product.productType,
        description: product.productDescription,
        price: product.productPrice,
        rating: product.productRating,
        gender: product.productGender,
        brand: product.brand?.brandName || 'Unknown',
        colors: product.colors?.$values?.map(c => c.colorName) || [],
        sizes: product.sizes?.$values || [], // <— include sizes if used in filtering
        image: productImages[product.productId] || product.imageUrl || '', // <— make sure `image` is available
        category: product.productType || '', // assuming productType is the category

      })) || [];

      setProducts(formatted);
    } catch (err) {
      console.error('Error fetching products:', err);
      setError('Failed to load products.');
    } finally {
      setLoading(false);
    }
  }

  fetchProducts();
}, []);

  // Filters from URL
  useEffect(() => {
    if (urlGender === 'men') setSelectedGender(['Men']);
    else if (urlGender === 'women') setSelectedGender(['Women']);
    else setSelectedGender([]);

    if (urlBrand && brandOptions.includes(urlBrand)) setSelectedBrands([urlBrand]);
    else setSelectedBrands([]);

    if (urlCategory && categoryOptions.includes(urlCategory)) setSelectedCategories([urlCategory]);
    else setSelectedCategories([]);
  }, [urlGender, urlBrand, urlCategory]);

  const filteredProducts = products.filter((product) => {
    if (selectedBrands.length && !selectedBrands.includes(product.brand)) return false;
    if (selectedGender.length && !selectedGender.includes(product.gender)) return false;
    if (selectedCategories.length && !selectedCategories.includes(product.category)) return false;
    if (selectedColors.length && !product.colors?.some((c) => selectedColors.includes(c))) return false;
    if (selectedSizes.length && !product.sizes?.some((s) => selectedSizes.includes(s))) return false;
    if (selectedPrices.length) {
      const inRange = selectedPrices.some(({ min, max }) =>
        product.price >= min && product.price < max
      );
      if (!inRange) return false;
    }
    if (urlSearch && !product.name?.toLowerCase().includes(urlSearch)) return false;
    return true;
  });

  const handleAddToCart = (product) => {
    const defaultSize = product.sizes && product.sizes.length > 0 ? product.sizes[0] : null;
    const defaultColor = product.colors && product.colors.length > 0 ? product.colors[0] : null;

    addToCart({
      id: product.id,
      name: product.name,
      price: product.price,
      image: product.image,
      size: defaultSize,
      color: defaultColor,
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
              {loading && <div>Loading products...</div>}
              {error && <div style={{ color: 'red' }}>{error}</div>}
              {!loading && !error && filteredProducts.length === 0 && (
                <div style={{ gridColumn: "1/-1", textAlign: "center", color: "#888" }}>
                  No products match your filters.
                </div>
              )}
              {!loading && !error && filteredProducts.map((product) => (
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
