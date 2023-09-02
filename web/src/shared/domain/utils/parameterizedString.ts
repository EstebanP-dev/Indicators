export const parameterizedString = (value : string, ...args : string[]) =>
{
    let replaceValues = [].slice.call(args, 1), i = 0;

    return value.replace(/%s/g, () => replaceValues[i++]);
}