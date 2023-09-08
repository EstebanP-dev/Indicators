import { createSlice } from "@reduxjs/toolkit";
import { AccountInfo } from "../../models";

export const AccountInfoEmptyState: AccountInfo = {
    token: '', user: {
        email: '',
        roles: []
    }
}

export const accountInfoSlice = createSlice({
    name: 'accountInfo',
    initialState: AccountInfoEmptyState,
    reducers: {
        createAccountInfo: (state, action) => action.payload,
        modifyAccountInfo: (state, action) => ({ ...state, ...action.payload }),
        resetAccountInfo: () => AccountInfoEmptyState
    }
});

export const { createAccountInfo, modifyAccountInfo, resetAccountInfo } = accountInfoSlice.actions

export default accountInfoSlice.reducer;