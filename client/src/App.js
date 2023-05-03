import logo from './logo.svg';
import './App.css';
import React, { useEffect, useState } from "react";
import { Spinner } from "reactstrap";
import { BrowserRouter } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import ApplicationViews from "./components/ApplicationViews";

function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <ApplicationViews/>
      </BrowserRouter>
    </div>
      
  );
}

export default App;
