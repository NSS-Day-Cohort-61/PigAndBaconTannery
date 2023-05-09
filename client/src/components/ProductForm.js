import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { addProduct } from '../modules/productManager';
import { getAllCategories } from '../modules/categoryManager';


export default function ProductForm() {
    const navigate = useNavigate();
    const [product, setProduct] = useState({
        Name: "",
        Price:"",
        VendorId:"",
        Quantity:"",
        CategoryIds: [],
        ProductDetail: {
            Description: "",
            Weight: ""
        }
    });
    // const [productDetail, setProductDetail] = useState({
    //     Description: "",
    //     Weight: ""
    // });
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        getAllCategories().then(categories => setCategories(categories));
    }, []);

    const handleSaveProduct = (e) =>
    {
        e.preventDefault();
        addProduct(product).then((p) => {
            navigate("/product");
        })
        .catch((err) => alert(`An error ocurred: ${err.message}`));
    }

    const handleInputChange = (e) => {
        const {name, value} = e.target;
        setProduct((prevProduct) => ({
            ...prevProduct,
            [name]: value
        }));
    };

    const handleProductDetailChange = (e) => {
        const {name, value} = e.target;
        setProduct((prevProduct) => ({
            ...prevProduct,
            ProductDetail: {
                ...prevProduct.ProductDetail,
                [name]: value
            }
        }));
    };

    const handleCategoryChange = (e) => {
        const options = e.target.options;
        const selectedCategories = [];

        for (let i = 0; i < options.length; i++) {
            if (options[i].selected) {
                selectedCategories.push(parseInt(options[i].value));
            }
        }

        setProduct((prevProduct) => ({  
            //prevProduct is the previous state of product
            ...prevProduct,
            CategoryIds: selectedCategories
        }));
    }

    return(
        <form onSubmit={handleSaveProduct}>
            <h2>Add Product</h2>
            <div>
                <label htmlFor="name">Name:</label>
            <input
                type="text"
                id="name"
                name="Name"
                value={product.Name}
                onChange={handleInputChange}
                />
            </div>
            <div>
                <label htmlFor="price">Price:</label>
                <input
                    type="number"
                    id="price"
                    name="Price"
                    value={product.Price}
                    onChange={handleInputChange}
                    />
            </div>
            <div>
                <label htmlFor="vendorId">VendorId:</label>
                <input
                    type="text"
                    id="vendorId"
                    name="VendorId"
                    value={product.VendorId}
                    onChange={handleInputChange}
                    />
            </div>
            <div>
            <label htmlFor="quantity">Quantity:</label>
                <input
                    type="number"
                    id="quantity"
                    name="Quantity"
                    value={product.Quantity}
                    onChange={handleInputChange}
                    />
            </div>
            <div>
                <label htmlFor="categoryIds">Categories:</label>
                <select 
                    id="categories"
                    multiple={true}
                    value={product.CategoryIds}
                    onChange={handleCategoryChange}
                    >
                    {categories.map((c) => (
                        <option key={c.id} value={c.id}>
                            {c.name}
                            </option>
                    ))}
                </select>
            </div>
            <div>
                <label htmlFor="weight">Weight:</label>
                <input
                    type="number"
                    id="weight"
                    name="Weight"
                    value={product.ProductDetail.Weight}
                    onChange={handleProductDetailChange}
                    />
            </div>
            <div>
                <label htmlFor="description">Description:</label>
                <input
                    type="textarea"
                    id="description"
                    name="Description"
                    value={product.ProductDetail.Description}
                    onChange={handleProductDetailChange}
                    />
            </div>
            <button type="submit">Save</button>
        </form>

    );
}