import {
  Autocomplete,
  AutocompleteChangeDetails,
  AutocompleteChangeReason,
  AutocompleteOwnerState,
  AutocompleteRenderGetTagProps,
  TextField,
} from '@mui/material';
import React from 'react';

type Props = {
  label?: string;
  options: ReadonlyArray<any>;
  defaultValue: any;
  getOptionLabel?: ((option: any) => string) | undefined;
  renderTags?:
    | ((
        value: any[],
        getTagProps: AutocompleteRenderGetTagProps,
        ownerState: AutocompleteOwnerState<any, true, false, false, 'div'>
      ) => React.ReactNode)
    | undefined;
  onChange?:
    | ((
        event: React.SyntheticEvent<Element, Event>,
        value: any[],
        reason: AutocompleteChangeReason,
        details?: AutocompleteChangeDetails<any> | undefined
      ) => void)
    | undefined;
};

const Selector = (props: Props) => {
  return (
    <Autocomplete
      {...props}
      isOptionEqualToValue={(option, value) => {
        return option.id === value.id;
      }}
      renderInput={(params) => (
        <TextField
          {...params}
          variant='outlined'
          label={props.label}
          color='secondary'
          sx={{
            '& .MuiInputBase-root': {
              display: 'flex',
              minWidth: '200px',
              maxWidth: '500px',
              flexWrap: 'wrap',
            },
          }}
        />
      )}
      onChange={props.onChange}
    />
  );
};

export default Selector;
