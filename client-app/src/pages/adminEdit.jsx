import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { dereferenceJsonNet } from "../Dereferencer";
import "../styles/EditProduct.css";

const API_BASE = "http://localhost:5066/api";

const EditProduct = () => {
  const { id } = useParams();
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
  const [selectedBrandId, setSelectedBrandId] = useState("");
  const [selectedColorIds, setSelectedColorIds] = useState([]);
  const [loading, setLoading] = useState(true);

  const [sizes, setSizes] = useState([]);
  const [sizeStocks, setSizeStocks] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [productRes, brandsRes, colorsRes] = await Promise.all([
          fetch(`${API_BASE}/Product/Get/${id}`),
          fetch(`${API_BASE}/Brand/GetAllBrands`),
          fetch(`${API_BASE}/Color/GetAllColors`),
        ]);

        const [productData, brandsRaw, colorsRaw] = await Promise.all([
          productRes.json(),
          brandsRes.json(),
          colorsRes.json(),
        ]);

        const dereferencedProduct = dereferenceJsonNet(productData);

        setFormData({
          productName: dereferencedProduct.productName || "",
          productType: dereferencedProduct.productType || "",
          productDescription: dereferencedProduct.productDescription || "",
          productPrice: dereferencedProduct.productPrice || 0,
          productRating: dereferencedProduct.productRating || 0,
          productGender: dereferencedProduct.productGender || "",
        });

        setSelectedBrandId(dereferencedProduct.brandId || "");

        setSelectedColorIds(
          dereferencedProduct.colors?.$values?.map((color) => color.colorId) || []
        );

        setBrands(brandsRaw.$values || []);
        setColors(colorsRaw.$values || []);
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        setLoading(false);
      }
    };

    const fetchSizes = async () => {
      try {
        const res = await fetch(`${API_BASE}/GetProductSizesByProductId/${id}`);
        const data = await res.json();
        const dereferenced = dereferenceJsonNet(data);

        const sizeDetails = await Promise.all(
          dereferenced.$values.map(async (entry) => {
            const sizeRes = await fetch(`${API_BASE}/Size/GetSizeById/${entry.sizeId}`);
            const sizeData = await sizeRes.json();
            const derefSize = dereferenceJsonNet(sizeData);
            return {
              sizeId: entry.sizeId,
              sizeStock: entry.stock,
              sizeValue: derefSize.sizeValue,
            };
          })
        );

        const sortedSizes = sizeDetails.sort((a, b) => {
          const sizeA = parseFloat(a.sizeValue);
          const sizeB = parseFloat(b.sizeValue);

          if (isNaN(sizeA) || isNaN(sizeB)) {
            return a.sizeValue.localeCompare(b.sizeValue);
          }

          return sizeA - sizeB;
        });

        setSizes(sortedSizes);

        const initialStocks = {};
        sortedSizes.forEach((s) => {
          initialStocks[s.sizeId] = s.sizeStock?.toString() ?? "0";
        });
        setSizeStocks(initialStocks);
      } catch (err) {
        console.error("Failed to fetch sizes:", err);
      }
    };

    fetchData();
    fetchSizes();
  }, [id]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleBrandChange = (e) => {
    setSelectedBrandId(e.target.value);
  };

  const handleColorChange = (e) => {
    const value = e.target.value;
    const checked = e.target.checked;

    setSelectedColorIds((prev) =>
      checked ? [...prev, value] : prev.filter((id) => id !== value)
    );
  };

  const handleStockChange = (sizeId, newStock) => {
    setSizeStocks((prev) => ({
      ...prev,
      [sizeId]: newStock,
    }));
  };

const handleSubmit = async (e) => {
    e.preventDefault();

    // Prepare productSizes array
    const productSizes = Object.entries(sizeStocks).map(([sizeId, stockValue]) => ({
      sizeId,
      stock: parseInt(stockValue, 10),
      productId: id, // Optional if backend doesn't require it
    }));

    const updatedProduct = {
    productName: formData.productName,
    productType: formData.productType,
    productDescription: formData.productDescription,
    productPrice: parseInt(formData.productPrice, 10),
    productRating: parseInt(formData.productRating, 10),
    productGender: formData.productGender,
    brandId: selectedBrandId,
    colorsId: selectedColorIds,
    ordersId: [],
    sizesId: sizes.map(size => size.sizeId),
    productSizes: Object.entries(sizeStocks).map(([sizeId, stock]) => ({
      productId: id,    // autofilled product ID here
      sizeId: sizeId,
      stock: parseInt(stock, 10),
    })),
  };

    try {
      console.log("Sending to backend:", JSON.stringify(updatedProduct, null, 2));
      const productResponse = await fetch(`${API_BASE}/Product/Update/Update/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedProduct),
      });

      if (!productResponse.ok) {
        throw new Error("Failed to update product");
      }

      alert("Product updated successfully!");
      navigate("/");
    } catch (error) {
      console.error("Error updating product:", error);
      alert("An error occurred while updating the product.");
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="edit-product-container">
      <h2 className="edit-product-title">Edit Product</h2>
      <form onSubmit={handleSubmit} className="edit-product-form">
        <TextField
          label="Name"
          name="productName"
          value={formData.productName}
          onChange={handleInputChange}
        />
        <TextField
          label="Type"
          name="productType"
          value={formData.productType}
          onChange={handleInputChange}
        />
        <TextField
          label="Description"
          name="productDescription"
          value={formData.productDescription}
          onChange={handleInputChange}
        />
        <TextField
          label="Price"
          name="productPrice"
          type="number"
          value={formData.productPrice}
          onChange={handleInputChange}
        />
        <TextField
          label="Rating"
          name="productRating"
          type="number"
          value={formData.productRating}
          onChange={handleInputChange}
        />
        <TextField
          label="Gender"
          name="productGender"
          value={formData.productGender}
          onChange={handleInputChange}
        />

        <div className="form-group">
          <label htmlFor="brand" className="form-label">
            Brand:
          </label>
          <select
            id="brand"
            value={selectedBrandId}
            onChange={handleBrandChange}
            className="form-select"
          >
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
                      value={sizeStocks[size.sizeId] ?? ""}
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

        <button type="submit" className="btn-submit">
          Save Changes
        </button>
      </form>
    </div>
  );
};

const TextField = ({ label, name, value, onChange, type = "text" }) => (
  <div className="form-group">
    <label htmlFor={name} className="form-label">
      {label}:
    </label>
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

export default EditProduct;