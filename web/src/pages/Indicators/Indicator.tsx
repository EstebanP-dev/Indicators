import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { SnackbarUtilities, loadAbort, urlUtility } from "../../utilities";
import { useAxiosApi } from "../../hooks";
import { useDispatch } from "react-redux"
import { Box, TextField, Typography, useTheme } from "@mui/material";
import { Body, MultipleSelector } from "../../components";
import { IndicatorByIdResponse } from "../../models";

const SLUG = "Indicators";
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
    const [data, setData] = useState<IndicatorByIdResponse | undefined>(undefined);
    const [newData, setNewData] = useState<IndicatorByIdResponse | undefined>(undefined);

    const fetchData = () => {
        let idParameter: string = urlDecode(id ?? "");
        callEndpoint<IndicatorByIdResponse>(
            getService(`${SLUG.toLowerCase()}/${idParameter}`),
            setData,
            undefined,
            undefined,
            (result) => {
                setNewData(result);
            }
        );
    }

    const handleSave = () => {
        let idParameter: string = urlDecode(id ?? "");
        callEndpoint(
            putService(`${SLUG.toLowerCase()}/${idParameter}`, newData),
            undefined,
            undefined,
            undefined,
            (_) => {
                SnackbarUtilities.success("Indicador actualizado.");
                navigate(-1);
            }
        );
    }

    useEffect(() => {
        fetchData();
    }, [])

    return (
        <Body
          title="Detalle de Indicador"
          slug="indicators"
          showAdd={false}
          isEditing={true}
          disableDelete={true}
          onSaveButton={handleSave}
        >
          <>
            <Box
              component="form"
              display="flex"
              flexDirection="row"
              flexWrap="wrap"
              width="100%"
              height="100%"
              gap="1rem"
              mt="1"
            >
              <Box display="flex" flexDirection="column">
                <TextField
                  label="Codigo"
                  name="code"
                  color="secondary"
                  defaultValue={data?.code}
                  onChange={(e) => console.log("pressed")}
                />
              </Box>
              <Box display="flex" flexDirection="column">
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
}

export default Indicator