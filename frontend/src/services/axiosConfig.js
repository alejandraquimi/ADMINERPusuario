import axios from "axios";

const apiUrl = process.env.REACT_APP_API_URL;

const axioInstance = axios.create({
  baseURL: `${process.env.REACT_APP_API_URL}`,
});
if (axioInstance.defaults.headers.common["Authorization"] === undefined) {
  const savedToken = localStorage.getItem("accessToken");

  if (savedToken) {
    axioInstance.defaults.headers.common[
      "Authorization"
    ] = `Bearer ${savedToken}`;
  }
}

const updateToken = (token) => {
  axioInstance.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  localStorage.setItem("accessToken", token);
};

axioInstance.updateToken = updateToken;

export default axioInstance;
