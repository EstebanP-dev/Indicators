import { createSlice } from "@reduxjs/toolkit"

const localStorageKey: string = 'appTheme';

export const AppThemeEmptyState: any = {
    mode: "dark"
}

export const appThemeSlice = createSlice({
    name: localStorageKey,
    initialState: localStorage.getItem(localStorageKey)
        ? JSON.parse(localStorage.getItem(localStorageKey) as string)
        : AppThemeEmptyState,
    reducers:{
        setMode: (state) => {
            state.mode = state.mode === 'light' ? 'dark' : 'light';
        }
    }
});

export const {setMode} = appThemeSlice.actions;

export default appThemeSlice.reducer;