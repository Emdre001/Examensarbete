import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";

const API_BASE = "http://localhost:5066/api/Product";

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
  const [loading, setLoading] = useState(true);

  function dereferenceJsonNet(obj) {
    if (!obj || typeof obj !== "object") return obj;
    const idMap = new Map();

    function collectIds(node) {
      if (Array.isArray(node)) {
        node.forEach(collectIds);
      } else if (node && typeof node === "object") {
        if (node.$id) {
          idMap.set(node.$id, node);
        }
        Object.values(node).forEach(collectIds);
      }
    }

    function resolveRefs(node, seen = new Set()) {
      if (Array.isArray(node)) {
        return node.map((item) => resolveRefs(item, seen));
      } else if (node && typeof node === "object") {
        if (node.$ref) {
          const refObj = idMap.get(node.$ref);
          if (!refObj) {
            console.warn(`Reference id ${node.$ref} not found`);
            return node;
          }
          if (seen.has(refObj)) return refObj;
          seen.add(refObj);
          return resolveRefs(refObj, seen);
        } else {
          const result = {};
          for (const key in node) {
            if (key !== "$id") {
              result[key] = resolveRefs(node[key], seen);
            }
          }
          return result;
        }
      } else {
        return node;
      }
    }

    collectIds(obj);
    return resolveRefs(obj);
  }

  useEffect(() => {
    fetch(`${API_BASE}/Get/${id}`)
      .then((res) => res.json())
      .then((data) => {
        const deref = dereferenceJsonNet(data);

        setFormData({
          productName: deref.productName || "",
          productType: deref.productType || "",
          productDescription: deref.productDescription || "",
          productPrice: deref.productPrice || 0,
          productRating: deref.productRating || 0,
          productGender: deref.productGender || "",
        });
        setLoading(false);
      })
      .catch((err) => {
        console.error("Failed to fetch product:", err);
        alert("Error loading product data.");
        setLoading(false);
      });
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`${API_BASE}/Update/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          productId: id,
          ...formData,
        }),
      });

      if (!response.ok) {
        throw new Error("Update failed");
      }

      alert("Product updated!");
      navigate("/");
    } catch (err) {
      console.error("Update error:", err);
      alert("Failed to update product.");
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="p-6 max-w-xl mx-auto">
      <h2 className="text-2xl font-bold mb-4">Edit Product</h2>
      <form onSubmit={handleSubmit} className="space-y-4">
        <TextField label="Name" name="productName" value={formData.productName} onChange={handleChange} />
        <TextField label="Type" name="productType" value={formData.productType} onChange={handleChange} />
        <TextField label="Description" name="productDescription" value={formData.productDescription} onChange={handleChange} />
        <TextField label="Price" name="productPrice" value={formData.productPrice} type="number" onChange={handleChange} />
        <TextField label="Rating" name="productRating" value={formData.productRating} type="number" onChange={handleChange} />
        <TextField label="Gender" name="productGender" value={formData.productGender} onChange={handleChange} />

        <button type="submit" className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
          Save Changes
        </button>
      </form>
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
