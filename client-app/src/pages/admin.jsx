import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const API_BASE = "http://localhost:5066/api/Product";

const AdminPage = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  function dereferenceJsonNet(obj) {
    if (!obj || typeof obj !== "object") return obj;

    const idMap = new Map();

    // 1. Traverse and collect all objects with $id
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

    // 2. Recursively replace $ref objects with their real objects
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
          if (seen.has(refObj)) {
            // To avoid circular refs infinite loop
            return refObj;
          }
          seen.add(refObj);
          return resolveRefs(refObj, seen);
        } else {
          // For normal object, resolve its properties
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

  const fetchProducts = async () => {
    try {
      const auth = localStorage.getItem('auth');
      const res = await fetch(`${API_BASE}/GetAll`, {
        headers: {
          'Authorization': `Basic ${auth}`
        }
      });
      const data = await res.json();

      const dereferencedData = dereferenceJsonNet(data);

      // dereferencedData.$values is now a fully dereferenced array of products
      const actualProducts = dereferencedData?.$values ?? [];

      // Sort alphabetically by productName (case-insensitive)
      actualProducts.sort((a, b) => {
        const nameA = (a.productName || "").toLowerCase();
        const nameB = (b.productName || "").toLowerCase();
        if (nameA < nameB) return -1;
        if (nameA > nameB) return 1;
        return 0;
      });

      setProducts(actualProducts);
    } catch (err) {
      console.error("Error fetching products", err);
    } finally {
      setLoading(false);
    }
  };


  useEffect(() => {
    fetchProducts();
  }, []);

  const handleDelete = async (productId) => {
      if (!window.confirm("Are you sure you want to delete this product?")) {
        return; // User canceled deletion
      }

      try {
        const res = await fetch(`${API_BASE}/Delete/${productId}`, {
          method: "DELETE",
        });

        if (!res.ok) {
          throw new Error(`Failed to delete product with id ${productId}`);
        }

        // On success, remove product from state
        setProducts((prevProducts) =>
          prevProducts.filter((p) => p.productId !== productId)
        );
      } catch (error) {
        console.error("Error deleting product:", error);
        alert("Failed to delete product. Please try again.");
      }
    };

//Add above H2 - <button className="bg-blue-600 hover:bg-blue-700 text-white font-semibold px-4 py-2 rounded" onClick={() => navigate("/admin/addProduct")}>Add New Product</button>
  
return (
    <div className="p-6">
      <h2 className="text-2xl font-bold mb-4">Admin Page</h2>
      <h3>All Products</h3>
      {loading ? (
        <p>Loading...</p>
      ) : products.length === 0 ? (
        <p>No products found.</p>
      ) : (
        <div className="flex flex-col divide-y divide-gray-200">
          {products.map((product, i) => (
            <div key={i} className="product-row hover:bg-gray-50">
              <span className="product-name">
                {product.productName || "Unnamed Product"}
              </span>
              <div className="product-actions">
                <button onClick={() => navigate(`/admin/editProduct/${product.productId}`)} className="update-btn">Update</button>
                <button onClick={() => handleDelete(product.productId)} className="delete-btn">Delete</button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default AdminPage;
