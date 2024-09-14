import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { Box, Button, Card, CardActions, CardContent, Container, Grid2 as Grid, Paper, Typography, } from "@mui/material";
import LandingPageImg from "./../../assets/landing-desk.png";
import { useAuth } from "../../contexts/authContext";
import OverviewTabPanel from "./OverviewTabPanel";

export function Home() {
  const { login } = useAuth();

  function handleLogin() {
    login();
  }

  return (
    <Container>
      <AuthenticatedTemplate>
        <Grid container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1">
              Willkommen bei <b>smabu</b>!
            </Typography>
          </Grid>
          <Grid size={{ xs: 12, sm: 12, lg: 4 }} sx={{ display: 'flex' }}>
            <Paper elevation={2} sx={{ p: 0, flex: 1, display: 'flex', flexDirection: 'column', overflow: 'hidden' }}>
              <Box
                component="img"
                src={LandingPageImg}
                alt="Small Business"
                sx={{ width: '100%', height: '100%', objectFit: 'cover' }}
              />
            </Paper>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, lg: 4 }} sx={{ display: 'flex' }}>
            <Card sx={{ p: 2, flex: 1, display: 'flex', flexDirection: 'column' }}>
              <CardContent>
                <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                  Word of the Day
                </Typography>
                <Typography variant="h5" component="div">
                  Feature 1
                </Typography>
                <Typography sx={{ color: 'text.secondary', mb: 1.5 }}>adjective</Typography>
                <Typography variant="body2">
                  well meaning and kindly.
                  <br />
                  {'"a benevolent smile"'}
                </Typography>
              </CardContent>
              <CardActions>
                <Button size="small">Learn More</Button>
              </CardActions>
            </Card>
          </Grid>
          <Grid size={{ xs: 12, sm: 6, lg: 4 }} sx={{ display: 'flex' }}>
            <Card sx={{ p: 2, flex: 1, display: 'flex', flexDirection: 'column' }}>
              <CardContent>
                <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                  Word of the Day
                </Typography>
                <Typography variant="h5" component="div">
                  Feature 2
                </Typography>
                <Typography sx={{ color: 'text.secondary', mb: 1.5 }}>adjective</Typography>
                <Typography variant="body2">
                  well meaning and kindly.
                  <br />
                  {'"a benevolent smile"'}
                </Typography>
              </CardContent>
              <CardActions>
                <Button size="small">Learn More</Button>
              </CardActions>
            </Card>
          </Grid>
        </Grid>

        <Grid container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Überblick
            </Typography>
          </Grid>
          <Grid size={{ xs: 12 }}>
            <Paper elevation={2} sx={{ p: 0 }}>
              <OverviewTabPanel />
            </Paper>
          </Grid>
        </Grid>

        <Grid container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Weitere Headline
            </Typography>
          </Grid>
          <Grid size={{ xs: 12, sm: 6 }}>
            <Paper elevation={2} sx={{ p: 2 }}>
              <Typography variant="h6" component="h2">
                Feature 3
              </Typography>
              <Typography>
                Beschreibung des zweiten Features. Optimiert für Leistung und Effizienz.
              </Typography>
            </Paper>
          </Grid>
          <Grid size={{ xs: 12, sm: 6 }}>
            <Paper elevation={2} sx={{ p: 2 }}>
              <Typography variant="h6" component="h2">
                Feature 4
              </Typography>
              <Typography>
                Beschreibung des zweiten Features. Optimiert für Leistung und Effizienz.
              </Typography>
            </Paper>
          </Grid>
        </Grid>
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <Grid container spacing={4} style={{ marginTop: '20px' }}>
          <Grid size={{ xs: 12, md: 8 }}>
            <Typography variant="h3" component="h1" gutterBottom>
              Willkommen bei <b>smabu</b>!
            </Typography>
            <Typography variant="subtitle1" component="h1" gutterBottom>
              Die Lösung für Kleinunternehmer.
            </Typography>
            <Button variant="contained" color="primary" size="large" onClick={handleLogin}>
              Jetzt anmelden
            </Button>
          </Grid>
          <Grid size={{ xs: 12, md: 4 }}>
            <img
              src={LandingPageImg}
              alt="Small Business"
              style={{ width: '100%', borderRadius: '8px' }}
            />
          </Grid>
        </Grid>
      </UnauthenticatedTemplate>
    </Container>
  );
}
