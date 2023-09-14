import { Box, Typography, useTheme } from "@mui/material";

type Props = {
  title: string;
  subtitle?: string;
};

const Header = (props: Props) => {
  const theme = useTheme();
  return (
    <Box>
      <Typography
        variant="h2"
        color={theme.palette.secondary.main}
        fontWeight="bold"
        sx={{
          mb: "5px",
        }}
      >
        {props.title}
      </Typography>
      <Typography variant="h5" color={theme.palette.secondary.light}>
        {props.subtitle}
      </Typography>
    </Box>
  );
};

export default Header;
