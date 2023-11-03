import {
  Autocomplete,
  AutocompleteChangeDetails,
  AutocompleteChangeReason,
  AutocompleteOwnerState,
  AutocompleteRenderGetTagProps,
  Chip,
  TextField,
} from '@mui/material';
import React from 'react';

type Props = {
  label?: string;
  options: ReadonlyArray<any>;
  value: any | undefined;
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

const MultipleSelector = (props: Props) => {
  return (
    <Autocomplete
      {...props}
      multiple
      isOptionEqualToValue={(option, value) => {
        return option.id === value.id;
      }}
      renderTags={
        !!props.renderTags
          ? props.renderTags
          : (values, props, _) =>
              values.map((value, index) => (
                <Chip
                  variant='outlined'
                  label={value.name}
                  {...props({ index })}
                />
              ))
      }
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

export default MultipleSelector;
