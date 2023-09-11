import { CircularProgress, ThemeProvider, createTheme } from '@mui/material'
import './loading.scss'

const theme = createTheme({
    palette: {
        primary: {
            main: '#2a3447',
            light: '#384259',
            dark: '#222b3c',
            contrastText: '#ffffff'
        },
        secondary: {
            main: '#ffffff'
        }
    },
});

type Props = {
    message: string | undefined;
    canCancel: boolean | undefined;
    cancelTitle: string | undefined;
}

const Loading = (props: Props) => {
  return (
    <div className="loading">
        <div className="container">
            <div className="information">
                <ThemeProvider theme={theme}>
                    <CircularProgress color='secondary' />
                </ThemeProvider>
                <p>{props.message ?? "Loading"}</p>
            </div>
            {
                props.canCancel
                ? <button>{props.cancelTitle ?? "Cancel"}</button>
                : <></>
            }
            
        </div>
    </div>
  )
}

export default Loading