import { GridColDef } from "@mui/x-data-grid";
import React, { useEffect, useState } from "react";
import { useAxiosApi } from "../hooks";
import { endpoints } from "../enviroments";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { loadAbort } from "../utilities";
import { CoverPage } from ".";
import {
  Box,
  Button,
  IconButton,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
  TextField,
  Typography,
  useTheme,
} from "@mui/material";
import { Close } from "@mui/icons-material";

type Props = {
  slug: string;
  columns: GridColDef[];
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setRefresh?: React.Dispatch<React.SetStateAction<boolean>>;
  selectionDataUrl?: string;
  adapterFuction?: (data: any) => any;
};

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
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
  const [multipleValue, setMultipleValue] = React.useState<string[]>([]);
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
        if (!!props.setRefresh){
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

  const handleMultipleSelectionChange = (
    event: SelectChangeEvent<typeof multipleValue>,
    column: GridColDef
  ) => {
    const {
      target: { value },
    } = event;

    setMultipleValue(typeof value === "string" ? value.split(",") : value);

    setData({
      ...data,
      [column.field]: value,
    });
  };

  useEffect(() => {
    if (!!props.selectionDataUrl) {
      fetchSelectionData();
    }
  }, []);

  return (
    <CoverPage>
      <Box
        position="relative"
        padding="1rem"
        borderRadius=".5rem"
        sx={{
          backgroundColor: theme.palette.background.default,
        }}
      >
        <Box
          display="flex"
          flexDirection="row"
          justifyContent="space-between"
          alignItems="center"
        >
          <Box display="flex" justifyContent="center" width="100%">
            <Typography
              variant="h1"
              fontSize="1.25rem"
              textTransform="uppercase"
              fontWeight="bold"
            >
              Agregar nuevo
            </Typography>
          </Box>
          <IconButton onClick={() => props.setOpen(false)}>
            <Close />
          </IconButton>
        </Box>
        <Box
          component="form"
          display="flex"
          flexDirection="row"
          flexWrap="wrap"
          minWidth="300px"
          maxWidth="500px"
          onSubmit={handleSubmit}
          mt="1"
        >
          {props.columns
            .filter((item) => item.field !== "id" && item.field !== "img")
            .map((column) => (
              <Box
                display="flex"
                flexDirection="column"
                width="40%"
                gap="1rem"
                m="1rem"
                key={column.field}
              >
                {column.type === "multipleSelect" ? (
                  <>
                    <InputLabel
                      id="multiple-label-id"
                      key={"label" + column.field}
                    >
                      {column.headerName}
                    </InputLabel>
                    <Select
                      multiple
                      required
                      key={"multipleSelect" + column.field}
                      id={"multiple-select-id" + column.field}
                      name={column.headerName}
                      labelId="multiple-label-id"
                      variant="outlined"
                      color="secondary"
                      value={multipleValue}
                      onChange={(e) => handleMultipleSelectionChange(e, column)}
                      MenuProps={MenuProps}
                    >
                      {selectionData.map((selection) => (
                        <MenuItem key={selection.id} value={selection.id}>
                          {selection.name}
                        </MenuItem>
                      ))}
                    </Select>
                  </>
                ) : (
                  <TextField
                    margin="normal"
                    required
                    key={"textField" + column.field}
                    id={column.field}
                    label={column.headerName}
                    name={column.headerName}
                    color="secondary"
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
            variant="outlined"
            type="submit"
            fullWidth
            color="secondary"
            sx={{
              padding: ".8rem",
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
