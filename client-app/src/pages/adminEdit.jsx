import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { dereferenceJsonNet } from "../Dereferencer"; // <-- centralized dereferencer

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
        const res = await fetch(`${API_BASE}/GetAllProductSizes`);
        const data = await res.json();
        const dereferenced = dereferenceJsonNet(data);

        const filteredSizes = dereferenced.$values.filter(
          (item) => item.productId.toString() === id.toString()
        );

        const sizeDetails = await Promise.all(
          filteredSizes.map(async (size) => {
            const res = await fetch(`${API_BASE}/Size/GetSizeById/${size.sizeId}`);
            const sizeData = await res.json();
            const derefSize = dereferenceJsonNet(sizeData);
            return {
              sizeId: size.sizeId,
              sizeStock: size.sizeStock,
              sizeValue: derefSize.sizeValue,
            };
          })
        );

        setSizes(sizeDetails);

        // Autofill stocks as strings
        const initialStocks = {};
        sizeDetails.forEach((s) => {
          initialStocks[s.sizeId] = s.sizeStock.toString();
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

  const updateSizeStock = async (sizeId) => {
    try {
      const response = await fetch(
        `${API_BASE}/updateProductSize/${id}/${sizeId}`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ sizeStock: parseInt(sizeStocks[sizeId], 10) }),
        }
      );

      if (!response.ok) {
        throw new Error("Failed to update stock");
      }

      alert("Stock updated!");
    } catch (err) {
      console.error(err);
      alert("Error updating stock");
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const updatedProduct = {
      productId: id,
      ...formData,
      brandId: selectedBrandId,
      colorIds: selectedColorIds,
    };

    try {
      const response = await fetch(`${API_BASE}/Product/Update/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedProduct),
      });

      if (!response.ok) {
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
    <div className="p-6 max-w-xl mx-auto">
      <h2 className="text-2xl font-bold mb-4">Edit Product</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
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

        <div>
          <label htmlFor="brand" className="block font-medium mb-1">
            Brand:
          </label>
          <select
            id="brand"
            value={selectedBrandId}
            onChange={handleBrandChange}
            className="w-full border p-2 rounded"
          >
            <option value="">Select a brand</option>
            {brands.map((brand) => (
              <option key={brand.brandId} value={brand.brandId}>
                {brand.brandName}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label className="block font-medium mb-1">Colors:</label>
          <div className="flex flex-wrap gap-2">
            {colors.map((color) => (
              <label key={color.colorId} className="flex items-center space-x-2">
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

        <button
          type="submit"
          className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
        >
          Save Changes
        </button>
      </form>

      {/* Sizes Table */}
      <div className="mt-10">
        <h3 className="text-xl font-semibold mb-4">Sizes & Stock</h3>
        <table className="w-full border border-gray-300 text-left">
          <thead>
            <tr className="bg-gray-100">
              <th className="p-2 border">Size</th>
              <th className="p-2 border">Stock</th>
              <th className="p-2 border">Action</th>
            </tr>
          </thead>
          <tbody>
            {sizes.map((size) => (
              <tr key={size.sizeId}>
                <td className="p-2 border">{size.sizeValue}</td>
                <td className="p-2 border">
                  <input
                    type="number"
                    className="border rounded p-1 w-20"
                    value={sizeStocks[size.sizeId] ?? ""}
                    onChange={(e) =>
                      handleStockChange(size.sizeId, e.target.value)
                    }
                  />
                </td>
                <td className="p-2 border">
                  <button
                    onClick={() => updateSizeStock(size.sizeId)}
                    className="bg-green-600 text-white px-3 py-1 rounded hover:bg-green-700"
                  >
                    Save
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

const TextField = ({ label, name, value, onChange, type = "text" }) => (
  <div>
    <label htmlFor={name} className="block font-medium mb-1">
      {label}:
    </label>
    <input
      type={type}
      name={name}
      id={name}
      value={value}
      onChange={onChange}
      className="w-full border p-2 rounded"
    />
  </div>
);

export default EditProduct;
