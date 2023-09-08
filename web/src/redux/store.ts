import { configureStore } from "@reduxjs/toolkit";
import { accountInfoSlice } from "./states/accountInfo";

export interface AppStore {
    accountInfo: any;
}

export default configureStore<AppStore>({
    reducer: {
        accountInfo: accountInfoSlice.reducer
    }
})