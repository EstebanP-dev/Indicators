import { GridColDef } from '@mui/x-data-grid';
import React, { useEffect, useState } from 'react';
import { useAxiosApi } from '../hooks';
import { endpoints } from '../enviroments';
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { loadAbort } from '../utilities';
import { CoverPage, MultipleSelector } from '.';
import {
  Box,
  Button,
  IconButton,
  TextField,
  Typography,
  useTheme,
} from '@mui/material';
import { Close } from '@mui/icons-material';

type Props = {
  slug: string;
  columns: GridColDef[];
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setRefresh?: React.Dispatch<React.SetStateAction<boolean>>;
  selectionDataUrl?: string;
  adapterFuction?: (data: any) => any;
};

const Add = (props: Props) => {
  const abortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { callEndpoint, postService, getService } = useAxiosApi(
    abortController,
    dispatch,
    navigate
  );
  const [selectionData, setSelectionData] = useState<any[]>([]);
  const [data, setData] = useState<any>({});
  const theme = useTheme();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    callEndpoint<any>(
      postService(endpoints.api.slug(props.slug), data),
      undefined,
      undefined,
      undefined,
      () => {
        if (!!props.setRefresh) {
          props.setRefresh(true);
        }

        props.setOpen(false);
      }
    );
  };

  const fetchSelectionData = async () => {
    callEndpoint<any>(
      getService(props.selectionDataUrl!),
      setSelectionData,
      undefined,
      undefined,
      undefined
    );
  };

  useEffect(() => {
    if (!!props.selectionDataUrl) {
      fetchSelectionData();
    }
  }, []);

  return (
    <CoverPage>
      <Box
        position='relative'
        padding='1rem'
        borderRadius='.5rem'
        sx={{
          backgroundColor: theme.palette.background.default,
        }}
      >
        <Box
          display='flex'
          flexDirection='row'
          justifyContent='space-between'
          alignItems='center'
        >
          <Box display='flex' justifyContent='center' width='100%'>
            <Typography
              variant='h1'
              fontSize='1.25rem'
              textTransform='uppercase'
              fontWeight='bold'
            >
              Agregar nuevo
            </Typography>
          </Box>
          <IconButton onClick={() => props.setOpen(false)}>
            <Close />
          </IconButton>
        </Box>
        <Box
          component='form'
          display='flex'
          flexDirection='row'
          flexWrap='wrap'
          minWidth='300px'
          maxWidth='500px'
          onSubmit={handleSubmit}
          mt='1'
        >
          {props.columns
            .filter((item) => item.field !== 'id' && item.field !== 'img')
            .map((column) => (
              <Box
                display='flex'
                flexDirection='column'
                width='40%'
                gap='1rem'
                m='1rem'
                key={column.field}
              >
                {column.type === 'multipleSelect' ? (
                  <MultipleSelector
                    value={data?.[column.field]}
                    options={selectionData}
                    defaultValue={[]}
                    onChange={(_: any, newValue: any) => {
                      setData({
                        ...data,
                        [column.field]: newValue.map((value: any) => value.id),
                      });
                      console.log('Data', data);
                    }}
                  />
                ) : (
                  <TextField
                    margin='normal'
                    required
                    key={'textField' + column.field}
                    id={column.field}
                    label={column.headerName}
                    name={column.headerName}
                    color='secondary'
                    onChange={(e) => {
                      setData({
                        ...data,
                        [column.field]: e?.target.value,
                      });
                    }}
                  />
                )}
              </Box>
            ))}
          <Button
            variant='outlined'
            type='submit'
            fullWidth
            color='secondary'
            sx={{
              padding: '.8rem',
            }}
          >
            Crear nuevo
          </Button>
        </Box>
      </Box>
    </CoverPage>
  );
};

export default Add;
