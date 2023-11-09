import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { SnackbarUtilities, loadAbort, urlUtility } from '../../utilities';
import { useAxiosApi } from '../../hooks';
import { useDispatch } from 'react-redux';
import { Box, Typography } from '@mui/material';
import { Body, Field, Loading } from '../../components';
import { AddColDef, ErrorOr, IndicatorByIdResponse } from '../../models';
import { indicatorAdapter } from '../../adapters';

const variableColumns: AddColDef[] = [
  {
    field: 'datum',
    headerName: 'Dato',
    type: 'double',
  },
  {
    field: 'date',
    headerName: 'Creado en: ',
    type: 'const',
  },
  {
    field: 'userId',
    headerName: 'Creado por: ',
    type: 'const',
  },
  {
    field: 'variable',
    headerName: 'Variable',
    type: 'select',
    slug: 'variables',
    getOptionLabel(option) {
      return option.name;
    },
  },
];

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
    propertyName: 'indicatorTypeId',
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
    getOptionValue(option) {
      return option?.map((x: any) => x.id);
    },
  },
  {
    field: 'variables',
    headerName: 'Variables',
    type: 'form',
    slug: 'variables',
    options: variableColumns,
  },
  {
    field: 'sources',
    headerName: 'Fuentes',
    type: 'multipleSelect',
    slug: 'sources',
    getOptionLabel(option) {
      return option.name;
    },
    getOptionValue(option) {
      return option?.map((x: any) => x.id);
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
    getOptionValue(option) {
      return option?.map((x: any) => x.id);
    },
  },
];

const SLUG = 'Indicators';
const Indicator = () => {
  const abortController: AbortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { callEndpoint, getService, putService } = useAxiosApi(
    abortController,
    navigate,
    dispatch
  );
  const {} = indicatorAdapter;
  const { urlDecode } = urlUtility;
  const { id } = useParams();
  const [data, setData] = useState<any | undefined>(undefined);
  const [newData, setNewData] = useState<any | undefined>(undefined);
  const [requestData, setRequestData] = useState<any>(
    columns.reduce<any>((previus, current) => {
      previus[current.propertyName ?? current.field] = undefined;
      return previus;
    }, {})
  );
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
        setRequestData({
          ...requestData,
          id: result.id,
        });
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
      } else if (x.type === 'form') {
        x.options!.map((subColumn) => {
          if (
            subColumn.type === 'select' ||
            subColumn.type === 'multipleSelect'
          ) {
            callEndpoint(
              getService<any>(`${subColumn.slug}?page=0&rows=100`),
              undefined,
              undefined,
              undefined,
              (result) => {
                subColumn.options = result.response;
              }
            );
          }
        });
      }

      if (!!data && !!requestData) {
        let field: string = x.propertyName ?? x.field;
        let value = data?.[field];
        setRequestData({
          ...requestData,
          [field]: !!x.getOptionValue ? x.getOptionValue(value) : value,
        });
      }
    });
  };

  const canSave = (): boolean => {
    return data !== newData;
  };

  const handleSave = () => {
    let idParameter: string = urlDecode(id ?? '');
    if (canSave()) {
      callEndpoint(
        putService(`${SLUG.toLowerCase()}/${idParameter}`, requestData),
        undefined,
        undefined,
        undefined,
        (_) => {
          SnackbarUtilities.success('Indicador actualizado.');
          navigate(-1);
        }
      );
    }
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
      disableSave={!canSave()}
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
            columns
              .filter((x) => x.type !== 'form')
              .map((field) => {
                return (
                  <Field
                    key={'id-' + field.field}
                    field={field}
                    value={newData?.[field.field]}
                    defaultValue={data?.[field.field]}
                    options={field.options ?? []}
                    getOptionLabel={field.getOptionLabel}
                    onChanged={(newValue) => {
                      setNewData({
                        ...newData,
                        [field.field]: newValue,
                      });

                      if (!!requestData) {
                        setRequestData({
                          ...requestData,
                          [field.propertyName ?? field.field]:
                            !!field.getOptionValue
                              ? field.getOptionValue(newValue)
                              : newValue,
                        });
                      }
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
        </Box>
        {columns
          .filter((x) => x.type === 'form')
          .map((field) => {
            return <Typography>{field.field}</Typography>;
          })}
      </>
    </Body>
  );
};

export default Indicator;
