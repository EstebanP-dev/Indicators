import { GridColDef } from "@mui/x-data-grid";
import React, { useState } from "react";
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
  TextField,
  Typography,
  useTheme,
} from "@mui/material";
import { Close } from "@mui/icons-material";

type Props = {
  slug: string;
  columns: GridColDef[];
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  setRefresh: React.Dispatch<React.SetStateAction<boolean>>;
};

const Add = (props: Props) => {
  const abortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { callEndpoint, postService } = useAxiosApi(
    abortController,
    dispatch,
    navigate
  );
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
        props.setRefresh(true);
        props.setOpen(false);
      }
    );
  };

  return (
    <CoverPage>
      <Box
        position="relative"
        padding="1rem"
        borderRadius=".5rem"
        minWidth="300px"
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
          flexDirection="column"
          flexWrap="wrap"
          maxWidth="1000px"
          justifyContent="space-between"
          onSubmit={handleSubmit}
          mt="1"
        >
          {props.columns
            .filter((item) => item.field !== "id" && item.field !== "img")
            .map((column) => (
              <Box
                display="flex"
                flexDirection="column"
                width="60%"
                gap="1rem"
                marginBottom="1rem"
              >
                <TextField
                  margin="normal"
                  required
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

  // return (
  //   <div className="add">
  //     <div className="modal">
  //       <span className="close" onClick={() => props.setOpen(false)}>
  //         X
  //       </span>
  //       <h1>Agregar nuevo {props.slug}</h1>
  //       <form onSubmit={handleSubmit}>
  //         {props.columns
  //           .filter((item) => item.field !== "id" && item.field !== "img")
  //           .map((column) => (
  //             <div className="item" key={column.field}>
  //               <label key={"label-" + column.field}>{column.headerName}</label>
  //               <input
  //                 key={"input-" + column.field}
  //                 type={column.type}
  //                 placeholder={column.field}
  //                 onChange={(e) => {
  //                   setData({
  //                     ...data,
  //                     [column.field]: e?.target?.value,
  //                   });
  //                 }}
  //               />
  //             </div>
  //           ))}
  //         <button>Send</button>
  //       </form>
  //     </div>
  //   </div>
  // );
};

export default Add;
