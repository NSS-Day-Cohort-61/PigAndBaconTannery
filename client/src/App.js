import logo from './logo.svg';
import './App.css';
import React, { useEffect, useState } from "react";
import { Spinner } from "reactstrap";
import { BrowserRouter } from "react-router-dom";
import 'bootstrap/dist/css/bootstrap.min.css';
import ApplicationViews from "./components/ApplicationViews";
import { onLoginStatusChange } from "./modules/authManager";
import Header from "./components/Header";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null);

  useEffect(() => {
      onLoginStatusChange(setIsLoggedIn);
  }, []);

   // The "isLoggedIn" state variable will be null until //  the app's connection to firebase has been established.
  //  Then it will be set to true or false by the "onLoginStatusChange" function
  if (isLoggedIn === null) {
    // Until we know whether or not the user is logged in or not, just show a spinner
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <div className="App">
      <BrowserRouter>
      <Header isLoggedIn={isLoggedIn}/>
        <ApplicationViews isLoggedIn={isLoggedIn} />
      </BrowserRouter>
    </div>
      
  );
}

export default App;
