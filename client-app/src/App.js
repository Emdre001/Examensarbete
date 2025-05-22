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
import AdminPage from './admin';
import { Toaster } from 'react-hot-toast';
import './styles/App.css';
import 'bootstrap/dist/css/bootstrap.min.css';

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
              <Route path="/products/:id" element={<ProductDetail />} />
              <Route path="/cart" element={<Cart />} />
              <Route path="/about" element={<About />} />
              <Route path="/contact" element={<Contact />} />
              <Route path="/login" element={<Login />} />
              <Route path="/signup" element={<SignUp />} />
              <Route path="/forgot-password" element={<ForgotPassword />} />
              <Route path="/admin" element={<AdminPage />} />
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