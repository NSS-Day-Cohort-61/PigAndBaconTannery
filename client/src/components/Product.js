import React, { useEffect, useState } from "react";
import {Card, CardBody} from "reactstrap";

export default function Product({ product }) {
  return (
    <Card>
      <CardBody>
        <h3>{product.name}</h3>
        <p>{product.productDetail.description}</p>
        <p>{product.price}</p>
        <p>{product.quantity}</p>
      </CardBody>
    </Card>
  );
}