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
  "8198b3f7-52cb-41f8-8fe9-122e751f6160": '/Assets/img/uggMiniSvart.jpg',
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
          {/* ... all your filter UI code stays unchanged */}
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
