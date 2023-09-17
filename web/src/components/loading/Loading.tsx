import {
  Box,
  Button,
  CircularProgress, 
  Typography,
  useTheme,
} from "@mui/material";
import "./loading.scss";
import { CoverPage } from "..";

type Props = {
  message: string | undefined;
  canCancel: boolean | undefined;
  cancelTitle: string | undefined;
};

const Loading = (props: Props) => {
  const theme = useTheme();

  return (
    <CoverPage
      sx={{
        backgroundColor: "#21252947 !i",
        position: "absolute",
        top: 0,
        left: 0,
        alignItems: "center",
        justifyContent: "center",
      }}
    >
      <Box
        display="flex"
        alignItems="center"
        justifyContent="center"
        flexDirection="column"
        borderRadius="1rem"
        sx={{
          backgroundColor: theme.palette.background.default,
          minWidth: props.canCancel
          ? "250px"
          : "min-content"
        }}
      >
        <Box
          width="100%"
          display="flex"
          flexDirection="column"
          justifyContent="center"
          padding="2rem 3rem"
          alignItems="center"
          maxWidth="300px"
        >
          <CircularProgress color="secondary" />
          <Typography
            margin="1rem 0"
            color={theme.palette.primary.contrastText}
            textAlign="center"
            sx={{
              wordWrap: "break-word",
            }}
          >
            {props.message}
          </Typography>
        </Box>
        {
            props.canCancel
            ? <Button fullWidth={true} variant="outlined" color="secondary" sx={{
                color:  theme.palette.primary.contrastText,
                border: "none",
                borderTop: "2px solid #f4811e",
                borderRadius: "0 0 1rem 1rem",
                padding: ".7rem"
            }}>
                {props.cancelTitle}
            </Button>
            : <></>
        }
      </Box>
    </CoverPage>
  );
};

export default Loading;
