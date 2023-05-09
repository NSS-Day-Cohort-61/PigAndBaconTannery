import React, { useEffect, useState } from "react";
import Product from "./Product";
import { getAllProducts } from "../modules/productManager";


export default function ProductList() {
  const [products, setProduct] = useState([]);

  useEffect(() => {
    getAllProducts().then((products) => {
      setProduct(products);
      console.log(products);
    });
  }, []);

  return (
    <section>
      {products.map((p) => (
        <Product key={p.id} product={p} />
      ))}
    </section>
  );
}