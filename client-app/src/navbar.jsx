import React, { useState } from 'react';
import {
  Navbar,
  Nav,
  Container,
  Button,
  Form,
  FormControl,
  Dropdown,
} from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { FaShoppingCart, FaSearch, FaUser } from 'react-icons/fa';
import useCartStore from './CartStore';
import './styles/navbar.css';

// Only these categories
const categoryOptions = ['Sneaker', 'Boots'];
const brandOptions = [
  'Nike', 'Adidas', 'New Balance', 'Axel Arigato', 'Ugg', 'Dior'
];

const CustomNavbar = () => {
  const cartCount = useCartStore((state) =>
    state.cart.reduce((total, item) => total + item.quantity, 0)
  );
  const [language, setLanguage] = useState('en');
  const [search, setSearch] = useState('');
  const toggleLanguage = () =>
    setLanguage((prev) => (prev === 'en' ? 'sv' : 'en'));
  const navigate = useNavigate();

  const handleNavigate = (gender, filterType, value) => {
    let url = `/products?gender=${gender.toLowerCase()}`;
    if (filterType && value) {
      url += `&${filterType}=${encodeURIComponent(value)}`;
    }
    navigate(url);
  };

  const handleSearch = (e) => {
    e.preventDefault();
    if (search.trim()) {
      navigate(`/products?search=${encodeURIComponent(search.trim())}`);
    }
  };

  return (
    <Navbar
      bg="dark"
      variant="dark"
      expand="lg"
      sticky="top"
      className="custom-navbar"
    >
      <Container>
        <Navbar.Brand
          as={Link}
          to="/"
          style={{ fontWeight: 700, fontSize: '1.5rem' }}
        >
          SoleMate
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="navbar-nav" />

        <Navbar.Collapse id="navbar-nav">
          <Nav className="ms-auto">
            {/* Men's Shoes */}
            <Dropdown as={Nav.Item} className="me-2">
              <Dropdown.Toggle as={Nav.Link} id="men-dropdown">
                Men's Shoes
              </Dropdown.Toggle>
              <Dropdown.Menu className="mega-menu-dropdown" data-dropdown="true">
                <div className="mega-menu-content">
                  <div>
                    <div className="mega-menu-title">Categories</div>
                    {categoryOptions.map((cat) => (
                      <div
                        key={cat}
                        className="dropdown-item"
                        onClick={() =>
                          handleNavigate('men', 'category', cat)
                        }
                        style={{ cursor: 'pointer' }}
                      >
                        {cat}
                      </div>
                    ))}
                  </div>
                  <div>
                    <div className="mega-menu-title">Brands</div>
                    {brandOptions.map((brand) => (
                      <div
                        key={brand}
                        className="dropdown-item"
                        onClick={() =>
                          handleNavigate('men', 'brand', brand)
                        }
                        style={{ cursor: 'pointer' }}
                      >
                        {brand}
                      </div>
                    ))}
                  </div>
                </div>
              </Dropdown.Menu>
            </Dropdown>

            {/* Women's Shoes */}
            <Dropdown as={Nav.Item} className="me-2">
              <Dropdown.Toggle as={Nav.Link} id="women-dropdown">
                Women's Shoes
              </Dropdown.Toggle>
              <Dropdown.Menu className="mega-menu-dropdown" data-dropdown="true">
                <div className="mega-menu-content">
                  <div>
                    <div className="mega-menu-title">Categories</div>
                    {categoryOptions.map((cat) => (
                      <div
                        key={cat}
                        className="dropdown-item"
                        onClick={() =>
                          handleNavigate('women', 'category', cat)
                        }
                        style={{ cursor: 'pointer' }}
                      >
                        {cat}
                      </div>
                    ))}
                  </div>
                  <div>
                    <div className="mega-menu-title">Brands</div>
                    {brandOptions.map((brand) => (
                      <div
                        key={brand}
                        className="dropdown-item"
                        onClick={() =>
                          handleNavigate('women', 'brand', brand)
                        }
                        style={{ cursor: 'pointer' }}
                      >
                        {brand}
                      </div>
                    ))}
                  </div>
                </div>
              </Dropdown.Menu>
            </Dropdown>

            <Nav.Link as={Link} to="/about">
              About
            </Nav.Link>
            <Nav.Link as={Link} to="/contact">
              Contact
            </Nav.Link>
          </Nav>

          {/* Search */}
          <Form className="d-flex ms-3 my-2 my-lg-0" onSubmit={handleSearch}>
            <FormControl
              type="search"
              placeholder="Search"
              className="me-2"
              aria-label="Search"
              value={search}
              onChange={e => setSearch(e.target.value)}
            />
            <Button variant="outline-light" type="submit">
              <FaSearch />
            </Button>
          </Form>

          {/* Language toggle */}
          <Button
            variant="outline-light"
            className="ms-3"
            style={{
              border: 'none',
              background: 'transparent',
              boxShadow: 'none',
              padding: 0,
            }}
            onClick={toggleLanguage}
            title={language === 'en' ? 'Switch to Swedish' : 'Byt till engelska'}
          >
            {language === 'en' ? '🇸🇪' : '🇬🇧'}
          </Button>

          {/* User Icon */}
          <Dropdown align="end" className="ms-3">
            <Dropdown.Toggle
              variant="outline-light"
              id="user-dropdown"
              style={{
                border: 'none',
                background: 'transparent',
                boxShadow: 'none',
                padding: 0,
                display: 'flex',
                alignItems: 'center',
              }}
            >
              <FaUser size={22} />
            </Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item as={Link} to="/login">
                Login
              </Dropdown.Item>
              <Dropdown.Item as={Link} to="/signup">
                Sign Up
              </Dropdown.Item>
              <Dropdown.Divider />
              <Dropdown.Item as={Link} to="/profile">
                Profile
              </Dropdown.Item>
              <Dropdown.Item as={Link} to="/settings">
                Settings
              </Dropdown.Item>
              <Dropdown.Item as={Link} to="/orders">
                Orders
              </Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>

          {/* Cart */}
          <Nav className="ms-3">
            <Nav.Link as={Link} to="/cart">
              <FaShoppingCart />{' '}
              {cartCount > 0 && (
                <span className="badge bg-danger">{cartCount}</span>
              )}
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default CustomNavbar;