import { createSlice } from "@reduxjs/toolkit";
import { AccountInfo } from "../../models";

const localStorageKey: string = 'accountInfo'

export const AccountInfoEmptyState: AccountInfo = {
    token: '', user: {
        email: '',
        roles: []
    }
}

export const persistAccountInfoInLocalStorage = (accountInfo: AccountInfo) => {
    localStorage.setItem(localStorageKey, JSON.stringify({ ...accountInfo }));
}

export const clearAccountInfoFromLocalStorage = () => {
    localStorage.removeItem(localStorageKey);
}

export const accountInfoSlice = createSlice({
    name: localStorageKey,
    initialState: localStorage.getItem(localStorageKey) 
        ? JSON.parse(localStorage.getItem(localStorageKey) as string)
        : AccountInfoEmptyState,
    reducers: {
        createAccountInfo: (_, action) => {
            persistAccountInfoInLocalStorage(action.payload);
            return action.payload;
        },
        modifyAccountInfo: (state, action) => {
            persistAccountInfoInLocalStorage(action.payload);
            return ({ ...state, ...action.payload });
        },
        resetAccountInfo: () => {
            clearAccountInfoFromLocalStorage();
            return AccountInfoEmptyState;
        }
    }
});

export const { createAccountInfo, modifyAccountInfo, resetAccountInfo } = accountInfoSlice.actions

export default accountInfoSlice.reducer;