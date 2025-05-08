import React from 'react';
import { Navbar, Nav, Container, Button, Form, FormControl } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import { FaShoppingCart, FaSearch } from 'react-icons/fa';
import './styles/navbar.css'; // se till att du pekar rätt här

const CustomNavbar = () => {
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
            <Nav.Link as={Link} to="/login">Login</Nav.Link>
            <Nav.Link as={Link} to="/signup">Sign Up</Nav.Link>
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

          <Nav className="ms-3">
            <Nav.Link as={Link} to="/cart">
              <FaShoppingCart /> <span className="badge bg-danger">3</span>
            </Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default CustomNavbar;
