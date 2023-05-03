import React from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import ProductList from "./ProductList";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={<ProductList />}
        />
        <Route path="*" element={<p>Whoops, nothing here...</p>} />
      </Route>
    </Routes>
  );
}