import React, { useState } from "react";
import Login from "./login";
import Products from "./products";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = () => {
    setIsLoggedIn(true); // Set login state to true
  };

  return (
    <div className="App">
      {isLoggedIn ? (
        <Products />
      ) : (
        <Login onLogin={handleLogin} /> // Pass the login handler to Login
      )}
    </div>
  );
}

export default App;