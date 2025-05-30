import React from 'react';
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
              <Route path="/profile" element={<Profile />} />
              <Route path="/orders" element={<Orders />} />
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