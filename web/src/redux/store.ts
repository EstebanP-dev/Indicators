import { configureStore } from "@reduxjs/toolkit";
import { accountInfoSlice } from "./states/accountInfo";
import { AccountInfo } from "../models";
import { loadingDataSlice } from "./states/loadingData";
import { appThemeSlice } from "./states/appTheme";

export interface AppStore {
    accountInfo: AccountInfo;
    loadingData: boolean;
    appTheme: any;
}

export default configureStore<AppStore>({
    reducer: {
        accountInfo: accountInfoSlice.reducer,
        loadingData: loadingDataSlice.reducer,
        appTheme: appThemeSlice.reducer,
    }
})