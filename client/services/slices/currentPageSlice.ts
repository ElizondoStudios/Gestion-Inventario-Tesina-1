import { createSlice } from "@reduxjs/toolkit";

const currentPageSlice = createSlice({
  name: "value",
  initialState: {
    value: "Inicio",
  },
  reducers: {
    changeCurrentPage: (state, page) => {
      state.value= page.payload
    }
  },
});

export const { changeCurrentPage } =
  currentPageSlice.actions;
export default currentPageSlice;