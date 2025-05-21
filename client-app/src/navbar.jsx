import React from 'react';
import { Navbar, Nav, Container, Button, Form, FormControl, Dropdown } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { FaShoppingCart, FaSearch, FaUser } from 'react-icons/fa';
import useCartStore from './CartStore';
import './styles/navbar.css';

const CustomNavbar = () => {
  const cartCount = useCartStore((state) =>
    state.cart.reduce((total, item) => total + item.quantity, 0)
  );

  return (
    <Navbar
      bg="dark"
      variant="dark"
      expand="lg"
      sticky="top"
      className="custom-navbar"
    >
      <Container>
        <Navbar.Brand as={Link} to="/">
          <img
            src="/img/logo.png"
            alt="SoleMate Logo"
            width="30"
            height="30"
            className="d-inline-block align-top"
          />{' '}
          SoleMate
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="navbar-nav" />

        <Navbar.Collapse id="navbar-nav">
          <Nav className="ms-auto">
            <Nav.Link as={Link} to="/">Home</Nav.Link>
            <Nav.Link as={Link} to="/products">Products</Nav.Link>
            <Nav.Link as={Link} to="/about">About</Nav.Link>
            <Nav.Link as={Link} to="/contact">Contact</Nav.Link>
          </Nav>

          <Form className="d-flex ms-3 my-2 my-lg-0">
            <FormControl
              type="search"
              placeholder="Search"
              className="me-2"
              aria-label="Search"
            />
            <Button variant="outline-light">
              <FaSearch />
            </Button>
          </Form>

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
                alignItems: 'center'
              }}
            >
              <FaUser size={22} />
            </Dropdown.Toggle>
            <Dropdown.Menu>
              <Dropdown.Item as={Link} to="/login">Login</Dropdown.Item>
              <Dropdown.Item as={Link} to="/signup">Sign Up</Dropdown.Item>
              <Dropdown.Divider />
              <Dropdown.Item as={Link} to="/profile">Profile</Dropdown.Item>
              <Dropdown.Item as={Link} to="/settings">Settings</Dropdown.Item>
              <Dropdown.Item as={Link} to="/orders">Orders</Dropdown.Item>
            </Dropdown.Menu>
          </Dropdown>

          <Nav className="ms-3">
            <Nav.Link as={Link} to="/cart">
              <FaShoppingCart />{' '}
              {cartCount > 0 && <span className="badge bg-danger">{cartCount}</span>}
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default CustomNavbar;