import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { SnackbarUtilities, loadAbort, urlUtility } from '../../utilities';
import { useAxiosApi } from '../../hooks';
import { useDispatch } from 'react-redux';
import { Box, Typography, useTheme } from '@mui/material';
import { Body, Field, Loading } from '../../components';
import { AddColDef, ErrorOr, IndicatorByIdResponse } from '../../models';

const columns: AddColDef[] = [
  {
    field: 'code',
    headerName: 'Código',
    type: 'string',
  },
  {
    field: 'name',
    headerName: 'Nombre',
    type: 'string',
  },
  {
    field: 'objective',
    headerName: 'Objetivo',
    type: 'string',
  },
  {
    field: 'scope',
    headerName: 'Alcance',
    type: 'string',
  },
  {
    field: 'formula',
    headerName: 'Formula',
    type: 'string',
  },
  {
    field: 'goal',
    headerName: 'Meta',
    type: 'string',
  },
  {
    field: 'indicatorType',
    headerName: 'Tipo de Indicador',
    type: 'select',
    slug: 'indicators/indicatortypes',
    getOptionLabel(option) {
      return option.name;
    },
  },
  {
    field: 'measurementUnit',
    headerName: 'Unidad de Medición',
    type: 'select',
    slug: 'measurementunits',
    getOptionLabel(option) {
      return option.description;
    },
  },
  {
    field: 'meaning',
    headerName: 'Sentido',
    type: 'select',
    slug: 'meanings',
    getOptionLabel(option) {
      return option.name;
    },
  },
  {
    field: 'frequency',
    headerName: 'Frecuencia',
    type: 'select',
    slug: 'frequencies',
    getOptionLabel(option) {
      return option.description;
    },
  },
  {
    field: 'displays',
    headerName: 'Representaciones Visuales',
    type: 'multipleSelect',
    slug: 'displays',
    getOptionLabel(option) {
      return option.name;
    },
  },
  {
    field: 'variables',
    headerName: 'Variables',
    type: 'multipleSelect',
    slug: 'variables',
    getOptionLabel(option) {
      return option.name;
    },
  },
  {
    field: 'sources',
    headerName: 'Fuentes',
    type: 'multipleSelect',
    slug: 'sources',
    getOptionLabel(option) {
      return option.name;
    },
  },
  {
    field: 'actors',
    headerName: 'Actores',
    type: 'multipleSelect',
    slug: 'actors',
    getOptionLabel(option) {
      return option.name;
    },
  },
];

const SLUG = 'Indicators';
const Indicator = () => {
  const theme = useTheme();
  const abortController: AbortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { callEndpoint, getService, putService } = useAxiosApi(
    abortController,
    navigate,
    dispatch
  );
  const { urlDecode } = urlUtility;
  const { id } = useParams();
  const [data, setData] = useState<any | undefined>(undefined);
  const [newData, setNewData] = useState<any | undefined>(undefined);
  const [error, setError] = useState<ErrorOr | undefined>(undefined);

  const fetchData = () => {
    let idParameter: string = urlDecode(id ?? '');
    callEndpoint<IndicatorByIdResponse>(
      getService(`${SLUG.toLowerCase()}/${idParameter}`),
      setData,
      setError,
      undefined,
      (result) => {
        setNewData(result);
      }
    );

    columns.forEach((x) => {
      if (x.type === 'select' || x.type === 'multipleSelect') {
        callEndpoint(
          getService<any>(`${x.slug}?page=0&rows=100`),
          undefined,
          undefined,
          undefined,
          (result) => {
            x.options = result.response;
          }
        );
      }
    });
  };

  const handleSave = () => {
    let idParameter: string = urlDecode(id ?? '');
    callEndpoint(
      putService(`${SLUG.toLowerCase()}/${idParameter}`, newData),
      undefined,
      undefined,
      undefined,
      (_) => {
        SnackbarUtilities.success('Indicador actualizado.');
        navigate(-1);
      }
    );
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <Body
      title='Detalle de Indicador'
      slug='indicators'
      showAdd={false}
      isEditing={true}
      disableDelete={true}
      onSaveButton={handleSave}
    >
      <>
        <Box
          component='form'
          display='flex'
          flexDirection='row'
          flexWrap='wrap'
          width='100%'
          height='100%'
          gap='1rem'
          mt='1'
        >
          {!!error ? (
            <Typography>{error.title}</Typography>
          ) : !!data ? (
            columns.map((field) => {
              return (
                <Field
                  key={'id-' + field.field}
                  field={field}
                  value={data?.[field.field]}
                  options={field.options ?? []}
                  getOptionLabel={field.getOptionLabel}
                  onChanged={(newValue) => {
                    newData({
                      ...data,
                      [field.field]: newValue,
                    });
                  }}
                />
              );
            })
          ) : (
            <Loading
              message='Cargando'
              canCancel={false}
              cancelTitle={undefined}
            />
          )}
          <Box display='flex' flexDirection='column'>
            {/* {!!roles && !!newData && !!newData.roles ? (
                  <MultipleSelector
                    value={newData?.roles}
                    options={roles}
                    defaultValue={data?.roles}
                    onChange={(_, newValue) => {
                      setNewData({
                        email: newData?.email ?? "",
                        roles: newValue,
                      });
                    }}
                  />
                ) : (
                  <></>
                )} */}
          </Box>
        </Box>
      </>
    </Body>
  );
};

export default Indicator;
