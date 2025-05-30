import React from 'react';
import { useEffect, useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './home';
import Products from './products';
import ProductDetail from './ProductDetail';
import About from './about';
import Contact from './contact';
import Login from './login';
import SignUp from './SignUp';
import ForgotPassword from './ForgotPassword';
import Cart from './Cart';
import Footer from './footer';
import Navbar from './navbar';
import AdminPage from './pages/admin';
import AdminAddProduct from './pages/AdminAddProduct';
import EditProduct  from './pages/adminEdit';
import { Toaster } from 'react-hot-toast';
import './styles/App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Profile from './Profile';
import Orders from './Orders';
import Checkout from './Checkout';



function App() {
  const [user, setUser] = React.useState(null);

  useEffect(() => {
    const credentials = localStorage.getItem('auth');
    if (!credentials) return;
    fetch('http://localhost:5066/api/Account/GetCurrentUser', {
      headers: { 'Authorization': `Basic ${credentials}` }
    })
    .then(res => res.ok ? res.json() : null)
    .then(data => setUser(data))
    .catch(() => setUser(null));
  }, []);

  return (
    <Router>
      <div className="app">
        {/* Navbar is rendered once */}
        <Navbar />

        {/* Main content wrapper */}
        <div className="page-wrapper">
          <main>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/products" element={<Products />} />
              <Route path="/products/:productId" element={<ProductDetail />} />
              <Route path="/cart" element={<Cart />} />
              <Route path="/about" element={<About />} />
              <Route path="/contact" element={<Contact />} />
              <Route path="/login" element={<Login />} />
              <Route path="/signup" element={<SignUp />} />
              <Route path="/forgot-password" element={<ForgotPassword />} />
              <Route path="/admin" element={<AdminPage />} />
              <Route path="/admin/editProduct/:id" element={<EditProduct />} />
              <Route path="/admin/AddProduct" element={<AdminAddProduct />} />
              <Route path="/profile" element={<Profile user={user} setUser={setUser}/>} />
              <Route path="/orders" element={<Orders user={user}/>} />
              <Route path="/checkout" element={<Checkout />} />
            </Routes>
          </main>
        </div>        
        <Footer />
        <Toaster position="top-right" />
      </div>
    </Router>
  );
}

export default App;