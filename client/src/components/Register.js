import React, { useState } from "react";
import { Button, Form, FormGroup, Label, Input } from "reactstrap";
import { useNavigate, Link } from "react-router-dom";
import { register } from "../modules/authManager";

export default function Login() {
  let navigate = useNavigate();

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [Name, setName] = useState();


  const registerSubmit = (e) => {
    e.preventDefault();
    register(email, password)
      .then(() => navigate("/login"))
      .catch(() => alert("Registration Failed"));
  };

  return (
    <Form onSubmit={registerSubmit}>
      <fieldset>
        <FormGroup>
          <Label for="email">Email</Label>
          <Input
            id="email"
            type="text"
            autoFocus
            onChange={(e) => setEmail(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="password">Password</Label>
          <Input
            id="password"
            type="password"
            onChange={(e) => setPassword(e.target.value)}
          />
        </FormGroup>
        <FormGroup>
          <Button>Register</Button>
        </FormGroup>
      </fieldset>
    </Form>
  );
}