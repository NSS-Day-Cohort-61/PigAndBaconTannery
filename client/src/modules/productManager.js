const baseUrl = '/api/product';

export const getAllProducts = () => {
  return fetch(baseUrl+"/GetAll")
    .then((res) => res.json())
};

// export const addProduct = (product) => {
//   return fetch(baseUrl, {
//     method: "POST",
//     headers: {
//       "Content-Type": "application/json",
//     },
//     body: JSON.stringify(product),
//   });
// };