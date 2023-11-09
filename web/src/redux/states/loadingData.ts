import { createSlice } from "@reduxjs/toolkit";

export const loadingDataSlice = createSlice({
    name: "loadingData",
    initialState: false,
    reducers: {
        showLoading: () => true,
        hideLoading: () => false,
    }
});

export const { showLoading, hideLoading } = loadingDataSlice.actions;

export default loadingDataSlice.reducer;