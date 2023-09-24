import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { loadAbort, urlUtility } from "../../utilities";
import { useAxiosApi } from "../../hooks";
import { useDispatch } from "react-redux";
import { Role, UserById } from "../../models";
import {
  Autocomplete,
  Box,
  Chip,
  TextField,
  Typography,
  useTheme,
} from "@mui/material";
import { Body } from "../../components";

const SLUG = "Users";

const User = () => {
  const theme = useTheme();
  const abortController: AbortController = loadAbort();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { callEndpoint, getService } = useAxiosApi(
    abortController,
    navigate,
    dispatch
  );
  const [roles, setRoles] = useState<Role[]>([]);
  const [data, setData] = useState<UserById | undefined>(undefined);
  const [newData, setNewData] = useState<UserById | undefined>(undefined);
  const { email } = useParams();
  const { urlDecode } = urlUtility;

  const fetchData = async () => {
    let id: string = urlDecode(email ?? "");
    callEndpoint<UserById>(
      getService(`${SLUG.toLowerCase()}/${id}`),
      setData,
      undefined,
      undefined,
      (result) => {
        setNewData(result);
      }
    );

    callEndpoint<Role>(
      getService("roles/all"),
      setRoles,
      undefined,
      undefined,
      undefined
    );
  };

  const handleSubmit = () => {
    console.log("pressed");
  };
  useEffect(() => {
    fetchData();
  }, []);

  return (
    <Body title="Detalle de Usuario" slug="users" showAdd={false}>
      <>
        <Box display="flex" flexDirection="row" gap="1rem" m="2rem 0">
          <Box
            component="img"
            alt="profile"
            src="https://images.pexels.com/photos/697509/pexels-photo-697509.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            height="200px"
            width="200px"
            borderRadius="1rem"
            sx={{
              objectFit: "cover",
            }}
          />
          <Box
            display="flex"
            padding="1rem"
            justifyContent="center"
            flexDirection="column"
            gap=".7rem"
          >
            <Typography
              fontSize="0.9rem"
              sx={{
                color: theme.palette.primary.contrastText,
                "& span": {
                  color: theme.palette.secondary.main,
                  fontWeight: "bold",
                },
              }}
            >
              <span>Creado en: </span>
              24/09/2023
            </Typography>
            <Typography
              fontSize="0.9rem"
              sx={{
                color: theme.palette.primary.contrastText,
                "& span": {
                  color: theme.palette.secondary.main,
                  fontWeight: "bold",
                },
              }}
            >
              <span>Ultima actualización en: </span>
              24/09/2023
            </Typography>
            <Typography
              fontSize="0.9rem"
              sx={{
                color: theme.palette.primary.contrastText,
                "& span": {
                  color: theme.palette.secondary.main,
                  fontWeight: "bold",
                },
              }}
            >
              <span>Ultima sesión en: </span>
              24/09/2023
            </Typography>
          </Box>
        </Box>
        <Box
          component="form"
          display="flex"
          flexDirection="row"
          flexWrap="wrap"
          width="100%"
          height="100%"
          gap="1rem"
          onSubmit={handleSubmit}
          mt="1"
        >
          <Box display="flex" flexDirection="column">
            <TextField
              label="Correo electrónico"
              name="Correo electrónico"
              color="secondary"
              value={newData?.email}
              onChange={(e) => {
                setNewData({
                  email: e.target.value,
                  roles: newData?.roles!,
                });
              }}
            />
          </Box>
          <Box display="flex" flexDirection="column">
            {!!roles ? (
              <Autocomplete
                multiple
                value={newData?.roles}
                options={roles}
                getOptionLabel={(option) => option.name}
                defaultValue={newData?.roles}
                renderTags={(values, props) =>
                  values.map((value, index) => (
                    <Chip
                      variant="outlined"
                      label={value.name}
                      {...props({ index })}
                    />
                  ))
                }
                renderInput={(params) => (
                  <TextField
                    {...params}
                    variant="outlined"
                    label="Roles"
                    color="secondary"
                    sx={{
                      "& .MuiInputBase-root": {
                        display: "flex",
                        minWidth: "200px",
                        maxWidth: "500px",
                        flexWrap: "wrap",
                      },
                    }}
                  />
                )}
                onChange={(_, newValue) => {
                  setNewData({
                    email: newData?.email ?? "",
                    roles: newValue,
                  });
                }}
              />
            ) : (
              <></>
            )}
          </Box>
        </Box>
      </>
    </Body>
  );
};

export default User;
