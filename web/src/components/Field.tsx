import { AddColDef } from '../models';
import { Box, TextField } from '@mui/material';
import { MultipleSelector, Selector } from '.';

type Props = {
  field: AddColDef;
  value?: any | undefined;
  onChanged: (newValue: any) => void;
  getOptionLabel?: ((option: any) => string) | undefined;
  options: ReadonlyArray<any>;
};

const Field = (props: Props) => {
  return (
    <Box display='flex' flexDirection='column'>
      {props.field.type === 'multipleSelect' ? (
        <>
          <MultipleSelector
            {...props}
            label={props.field.headerName}
            getOptionLabel={props.getOptionLabel}
            defaultValue={props.field.value}
            onChange={(_, newValue) => {
              props.onChanged(newValue);
            }}
          />
        </>
      ) : props.field.type === 'select' ? (
        <>
          <Selector
            {...props}
            label={props.field.headerName}
            getOptionLabel={props.getOptionLabel}
            defaultValue={props.field.value}
            onChange={(_, newValue) => {
              props.onChanged(newValue);
            }}
          />
        </>
      ) : (
        <TextField
          label={props.field.headerName}
          name={props.field.field}
          color='secondary'
          defaultValue={props.value}
          onChange={(e) => {
            props.onChanged(e.target.value);
          }}
        />
      )}
    </Box>
  );
};

export default Field;
