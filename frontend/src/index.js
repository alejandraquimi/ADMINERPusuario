import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import Routes from "./routes/Routes";
import reportWebVitals from "./reportWebVitals";
import { Provider } from "react-redux";
import store from "./redux/store";
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  // <React.StrictMode>
  <Provider store={store}>
    <Routes />
  </Provider>,
  // </React.StrictMode>,
  document.getElementById("root")
);

reportWebVitals();
