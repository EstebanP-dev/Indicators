import { Box, Button, Typography, useTheme } from '@mui/material';

type Props = {
  title: string;
  showAdd: boolean;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
  subtitle?: string;
};

const Header = (props: Props) => {
  const theme = useTheme();

  return (
    <Box
      display='flex'
      flexDirection='row'
      justifyContent='space-between'
      width='100%'
      mb='1rem'
    >
      <Box>
        <Typography
          variant='h2'
          color={theme.palette.secondary.main}
          fontWeight='bold'
          sx={{
            mb: '5px',
          }}
        >
          {props.title}
        </Typography>
        <Typography variant='h5' color={theme.palette.secondary.light}>
          {props.subtitle}
        </Typography>
      </Box>
      <Box
        display={props.showAdd ? 'flex' : 'none'}
        alignItems='center'
        justifyContent='center'
      >
        <Button
          color='secondary'
          variant='outlined'
          onClick={() => props.setOpen(true)}
          sx={{
            padding: '.6rem',
          }}
        >
          Agregar Nuevo
        </Button>
      </Box>
    </Box>
  );
};

export default Header;
