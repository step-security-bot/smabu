import { unstable_createMuiStrictModeTheme as createMuiTheme } from "@mui/material/styles";
import { red, blueGrey, grey } from "@mui/material/colors";

// Create a theme instance.
export const theme = createMuiTheme({
  palette: {
    mode: 'light',
    primary: {
      main: blueGrey[900],
    },
    secondary: {
      main: '#19857b',
    },
    error: {
      main: red.A400,
    },
    background: {
      default: grey[100],
    },
  },
  components: {
    // Name of the component
    MuiTextField: {
      defaultProps: {
        variant: "standard"
      },
    },
  },
});