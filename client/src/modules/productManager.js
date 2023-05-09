import { getToken } from "./authManager";

const baseUrl = '/api/product';

export const getAllProducts = () => {
  return getToken().then((token) => {
    return fetch(baseUrl+"/GetAll", {
        method: "GET",
        headers: {
            Authorization: `Bearer ${token}`
        }
    })
    .then((resp) => {
        if (resp.ok) {
            return resp.json();
        } else {
            throw new Error("An unknown error occurred while trying to get products.");
        }
    })
}
)};

// export const addProduct = (product) => {
//   return fetch(baseUrl, {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify(product),
//   });
// };