# Status

En esta clase almacenamos los estados de la información. Es equivalente al contexto de React Dom para el useState.

**NOTA:**

- Para la actualización de la información el contexto se reescribe con la nueva información por buenas prácticas.
- La información de AccountInfo es lo que nos llega posterior al login.

```ts
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
```
