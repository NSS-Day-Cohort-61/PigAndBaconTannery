import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import ProductList from "./ProductList";
import Login from "./Login";
import Register from "./Register";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={isLoggedIn ? <ProductList /> : <Navigate to="/login" />}
        />
        <Route path="login" element={<Login/>}/>
        <Route path="register" element={<Register/>}/>
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}