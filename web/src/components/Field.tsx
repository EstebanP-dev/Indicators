import { AddColDef } from '../models';
import { Box, TextField } from '@mui/material';
import { MultipleSelector, Selector } from '.';

type Props = {
  field: AddColDef;
  value: any | undefined;
  defaultValue: any;
  onChanged: (newValue: any) => void;
  getOptionLabel?: ((option: any) => string) | undefined;
  getOptionValue?: ((option: any) => any) | undefined;
  options: ReadonlyArray<any>;
};

const Field = (props: Props) => {
  const onChangeValueHandler = (targetValue: any) => {
    let value = !!props.getOptionValue
      ? props.getOptionValue(targetValue)
      : targetValue;

    props.onChanged(value);
  };

  return (
    <Box display='flex' flexDirection='column'>
      {props.field.type === 'multipleSelect' ? (
        <>
          <MultipleSelector
            {...props}
            label={props.field.headerName}
            onChange={(_, newValue) => {
              onChangeValueHandler(newValue);
            }}
          />
        </>
      ) : props.field.type === 'select' ? (
        <>
          <Selector
            {...props}
            label={props.field.headerName}
            onChange={(_, newValue) => {
              onChangeValueHandler(newValue);
            }}
          />
        </>
      ) : (
        <TextField
          {...props}
          label={props.field.headerName}
          name={props.field.field}
          color='secondary'
          onChange={(e) => {
            onChangeValueHandler(e.target.value);
          }}
        />
      )}
    </Box>
  );
};

export default Field;
