export const localStorage =
{
    getValue(key: string)
    {
        const value = window.localStorage.getItem(key);

        if (!value)
        {
            return null;
        }

        return JSON.parse(value);
    },

    setValue(key: string, value: any)
    {
        window.localStorage.setItem(key, JSON.stringify(value));
    },

    removeValue(key: string)
    {
        window.localStorage.removeItem(key);
    },

    clearAll()
    {
        window.localStorage.clear();
    }
}