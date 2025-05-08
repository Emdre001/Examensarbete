import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './home';
import Products from './products';
import About from './about';
import Contact from './contact';
import Login from './login';
import SignUp from './SignUp';
import ForgotPassword from './ForgotPassword';
import Footer from './footer';
import Navbar from './navbar';
import './styles/App.css';
import 'bootstrap/dist/css/bootstrap.min.css';




function App() {
  return (
    <Router>
      <div className="app">
      <Navbar />
      <div className="page-content">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/products" element={<Products />} />
          <Route path="/about" element={<About />} />
          <Route path="/contact" element={<Contact />} />
          <Route path="/login" element={<Login />} />
          <Route path="/signup" element={<SignUp />} />
          <Route path="/forgot-password" element={<ForgotPassword />} />
        </Routes>
         </div>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
