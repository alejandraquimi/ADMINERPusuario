// authSlice.js
import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
  name: "auth",
  initialState: {
    userInfo: {
      idUsuario: 0,
      usuario: null,
    },
    contactos: null,
  },
  reducers: {
    setUserInfo: (state, action) => {
      state.userInfo = action.payload;
    },
    setContactos: (state, action) => {
      state.contactos = action.payload;
    },
  },
});

export const { setUserInfo, setContactos } = authSlice.actions;

export default authSlice.reducer;
