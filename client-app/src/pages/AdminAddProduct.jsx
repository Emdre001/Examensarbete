import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/EditProduct.css"; // reuse styles

const API_BASE = "http://localhost:5066/api";

const AdminAddProduct = () => {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    productName: "",
    productType: "",
    productDescription: "",
    productPrice: 0,
    productRating: 0,
    productGender: "",
  });

  const [brands, setBrands] = useState([]);
  const [colors, setColors] = useState([]);
  const [sizes, setSizes] = useState([]);
  const [selectedBrandId, setSelectedBrandId] = useState("");
  const [selectedColorIds, setSelectedColorIds] = useState([]);
  const [sizeStocks, setSizeStocks] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchInitialData = async () => {
      try {
        const [brandsRes, colorsRes, sizesRes] = await Promise.all([
          fetch(`${API_BASE}/Brand/GetAllBrands`),
          fetch(`${API_BASE}/Color/GetAllColors`),
          fetch(`${API_BASE}/Size/GetAllSizes`)
        ]);

        const [brandsData, colorsData, sizesData] = await Promise.all([
          brandsRes.json(),
          colorsRes.json(),
          sizesRes.json()
        ]);

        const brandList = brandsData?.$values ?? [];
        const colorList = colorsData?.$values ?? [];
        const sizeList = sizesData?.$values ?? [];

        // ✅ Sort sizes here
        const sortedSizes = sizeList.sort((a, b) => {
        const aVal = parseFloat(a.sizeValue);
        const bVal = parseFloat(b.sizeValue);
        if (isNaN(aVal) || isNaN(bVal)) {
            return a.sizeValue.localeCompare(b.sizeValue);
        }
        return aVal - bVal;
        });

        setBrands(brandList);
        setColors(colorList);
        setSizes(sortedSizes);

        const initialStocks = {};
        sortedSizes.forEach((size) => {
        initialStocks[size.sizeId] = "0";
        });
        setSizeStocks(initialStocks);
    } catch (error) {
        console.error("Failed to fetch initial data", error);
    } finally {
        setLoading(false);
    }
};

    fetchInitialData();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleBrandChange = (e) => {
    setSelectedBrandId(e.target.value);
  };

  const handleColorChange = (e) => {
    const { value, checked } = e.target;
    setSelectedColorIds((prev) =>
      checked ? [...prev, value] : prev.filter((id) => id !== value)
    );
  };

  const handleStockChange = (sizeId, stockValue) => {
    setSizeStocks((prev) => ({
      ...prev,
      [sizeId]: stockValue,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    

    const newProduct = {
      ...formData,
      productPrice: parseInt(formData.productPrice, 10),
      productRating: parseInt(formData.productRating, 10),
      brandId: selectedBrandId,
      colorsId: selectedColorIds,
      ordersId: [],
      sizesId: sizes.map((s) => s.sizeId),
    };

    try {
      const res = await fetch(`${API_BASE}/Product/Create`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newProduct),
        });

        if (!res.ok) throw new Error("Failed to create product");

        const createdProduct = await res.json(); // assumes backend returns created product
        const productId = createdProduct.productId;

        const productSizes = Object.entries(sizeStocks).map(([sizeId, stock]) => ({
        sizeId,
        stock: parseInt(stock, 10),
        }));
        
      alert("Product created successfully!");
      navigate("/admin");
    } catch (err) {
      console.error(err);
      alert("An error occurred while creating the product.");
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="edit-product-container">
      <h2 className="edit-product-title">Add New Product</h2>
      <form onSubmit={handleSubmit} className="edit-product-form">
        <TextField label="Name" name="productName" value={formData.productName} onChange={handleInputChange} />
        <TextField label="Type" name="productType" value={formData.productType} onChange={handleInputChange} />
        <TextField label="Description" name="productDescription" value={formData.productDescription} onChange={handleInputChange} />
        <TextField label="Price" name="productPrice" type="number" value={formData.productPrice} onChange={handleInputChange} />
        <TextField label="Rating" name="productRating" type="number" value={formData.productRating} onChange={handleInputChange} />
        <TextField label="Gender" name="productGender" value={formData.productGender} onChange={handleInputChange} />

        <div className="form-group">
          <label className="form-label">Brand:</label>
          <select className="form-select" value={selectedBrandId} onChange={handleBrandChange}>
            <option value="">Select a brand</option>
            {brands.map((brand) => (
              <option key={brand.brandId} value={brand.brandId}>
                {brand.brandName}
              </option>
            ))}
          </select>
        </div>

        <div className="form-group">
          <label className="form-label">Colors:</label>
          <div className="color-options">
            {colors.map((color) => (
              <label key={color.colorId} className="checkbox-label">
                <input
                  type="checkbox"
                  value={color.colorId}
                  checked={selectedColorIds.includes(color.colorId)}
                  onChange={handleColorChange}
                />
                <span>{color.colorName}</span>
              </label>
            ))}
          </div>
        </div>

        <div className="sizes-section">
          <h3 className="sizes-title">Sizes & Stock</h3>
          <table className="sizes-table">
            <thead>
              <tr>
                <th>Size</th>
                <th>Stock</th>
              </tr>
            </thead>
            <tbody>
              {sizes.map((size) => (
                <tr key={size.sizeId}>
                  <td>{size.sizeValue}</td>
                  <td>
                    <input
                      type="number"
                      className="stock-input"
                      value={sizeStocks[size.sizeId]}
                      onChange={(e) =>
                        handleStockChange(size.sizeId, e.target.value)
                      }
                    />
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <button type="submit" className="btn-submit">Create Product</button>
      </form>
    </div>
  );
};

const TextField = ({ label, name, value, onChange, type = "text" }) => (
  <div className="form-group">
    <label htmlFor={name} className="form-label">{label}:</label>
    <input
      type={type}
      name={name}
      id={name}
      value={value}
      onChange={onChange}
      className="form-input"
    />
  </div>
);

export default AdminAddProduct;
