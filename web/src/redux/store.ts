import { configureStore } from "@reduxjs/toolkit";
import { accountInfoSlice } from "./states/accountInfo";
import { AccountInfo } from "../models";
import { loadingDataSlice } from "./states/loadingData";

export interface AppStore {
    accountInfo: AccountInfo;
    loadingData: boolean; 
}

export default configureStore<AppStore>({
    reducer: {
        accountInfo: accountInfoSlice.reducer,
        loadingData: loadingDataSlice.reducer
    }
})