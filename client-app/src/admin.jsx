import React, { useEffect, useState } from "react";

const API_BASE = "http://localhost:5066/api/Product";

const AdminPage = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  const fetchProducts = async () => {
    try {
      const res = await fetch(`${API_BASE}/GetAll`);
      const data = await res.json();
      const actualProducts = data?.$values ?? [];
      console.log("Fetched Products:", actualProducts); // ← DEBUG
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

  return (
    <div className="p-4">
      <h2>Admin Page</h2>
      {loading ? (
        <p>Loading...</p>
      ) : (
        <div>
          {products.length === 0 ? (
            <p>No products found.</p>
          ) : (
            products.map((product, i) => (
              <div key={i} style={{ border: "1px solid gray", margin: "10px", padding: "10px" }}>
                <strong>{product.productName || "Unnamed Product"}</strong>
                <p>Type: {product.productType}</p>
                <p>Price: ${product.productPrice}</p>
                <button onClick={() => alert("Update not implemented")}>Update</button>
                <button onClick={() => alert("Delete not implemented")}>Delete</button>
              </div>
            ))
          )}
        </div>
      )}
    </div>
  );
};

export default AdminPage;
