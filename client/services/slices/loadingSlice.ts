import { createSlice } from "@reduxjs/toolkit";

const loadingSlice = createSlice({
  name: "value",
  initialState: {
    value: false,
    requestCount: 0,
  },
  reducers: {
    showLoader: (state) => {
      state.value = true;
    },
    hideLoader: (state) => {
      state.value = false;
    },
    incrementCount: (state) => {
      state.requestCount++;
    },
    decrementCount: (state) => {
      state.requestCount--;
    },
  },
});

export const { showLoader, hideLoader, incrementCount, decrementCount } =
  loadingSlice.actions;
export default loadingSlice;