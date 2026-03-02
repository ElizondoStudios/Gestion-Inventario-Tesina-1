import { configureStore } from "@reduxjs/toolkit";
import loadingSlice from "./slices/loadingSlice";
import currentPageSlice from "./slices/currentPageSlice";

export const store = configureStore({
  reducer: {
    loading: loadingSlice.reducer,
    currentPage: currentPageSlice.reducer
  },
});