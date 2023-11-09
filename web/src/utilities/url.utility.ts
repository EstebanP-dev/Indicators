const POINT_ENCODE_CODE = "&89";

const urlEnconde = (value: string) => {
  let encodeValue: string = encodeURIComponent(value);
  encodeValue  = encodeValue.replace('.', POINT_ENCODE_CODE);

  return encodeValue;
}

const urlDecode = (encodeValue: string) => {
  let value: string = decodeURIComponent(encodeValue);
  value = value.replace(POINT_ENCODE_CODE, '.');
  
  return value;
}

export default {
  urlEnconde,
  urlDecode
}