import { PaletteMode } from "@mui/material";

// color design tokens export
export const tokensDark = {
    grey: {
        0: "#ffffff", // manually adjusted
        10: "#f6f6f6", // manually adjusted
        50: "#f0f0f0", // manually adjusted
        100: "#e0e0e0",
        200: "#c2c2c2",
        300: "#a3a3a3",
        400: "#858585",
        500: "#666666",
        600: "#525252",
        700: "#3d3d3d",
        800: "#292929",
        900: "#141414",
        1000: "#000000", // manually adjusted
    },
    primary: {
      light: "#7a7c7f",
      main: "#212529",
      dark: "#0d0f10",
      contrastText: "#fffff",
    },
    secondary: {
      light: "#fbcda5",
      main: "#f4811e",
      dark: "#311a06",
      contrastText: "#141414",
    },
  };
  
  // function that reverses the color palette
  function reverseTokens(tokensDark: any) {
    const reversedTokens: any = {};
    Object.entries(tokensDark).forEach(([key, val]: any[]) => {
      const keys: string[] = Object.keys(val);
      const values = Object.values(val);
      const length = keys.length;
      const reversedObj: any = {};
      for (let i = 0; i < length; i++) {
        reversedObj[keys[i]] = values[length - i - 1];
      }
      reversedTokens[key] = reversedObj;
    });
    return reversedTokens;
  }
  export const tokensLight = reverseTokens(tokensDark);
  
  // mui theme settings
  export const themeSettings = (mode: string): any => {
    return {
      palette: {
        mode: mode as PaletteMode,
        ...(mode === "dark"
          ? {
              primary: {
                ...tokensDark.primary,
              },
              secondary: {
                ...tokensDark.secondary,
              },
              common: {
                ...tokensDark.grey,
              },
              background: {
                default: tokensDark.primary.main,
                paper: tokensDark.primary.dark,
              },
            }
          : {
              primary: {
                main: tokensDark.grey[200],
                light: tokensDark.grey[300],
                contrastText: tokensDark.secondary.dark
              },
              secondary: {
                ...tokensDark.secondary
              },
              common: {
                ...tokensLight.grey,
                main: tokensDark.grey[700],
              },
              background: {
                default: tokensDark.grey[50],
                paper: tokensDark.grey[100],
              },
            }),
      },
      typography: {
        fontFamily: ["Inter", "sans-serif"].join(","),
        fontSize: 12,
        h1: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 40,
        },
        h2: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 32,
        },
        h3: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 24,
        },
        h4: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 20,
        },
        h5: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 16,
        },
        h6: {
          fontFamily: ["Inter", "sans-serif"].join(","),
          fontSize: 14,
        },
      },
    };
  };