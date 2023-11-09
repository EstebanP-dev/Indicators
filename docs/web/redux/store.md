# Store

Configuramos la información que almacenaremos en Redux.

```ts
export interface AppStore {
    accountInfo: any;
}

export default configureStore<AppStore>({
    reducer: {
        accountInfo: accountInfoSlice.reducer
    }
})
```
