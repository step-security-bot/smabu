import { Avatar, Box, Button, Card, CardContent, CardHeader, Container, Grid2 as Grid, IconButton, Paper, SvgIcon, Typography, } from "@mui/material";
import LandingPageImg from "./../../assets/landing-desk.png";
import { useAuth } from "../../contexts/authContext";
import { getWelcomeDashboard } from "../../services/dashboard.service";
import { useEffect, useState } from "react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import { GetWelcomeDashboardReadModel } from "../../types/domain";
import { getFlatItems, navConfig } from "../../configs/navConfig";
import { orange } from "@mui/material/colors";
import { Add as AddIcon, ChevronRight as ChevronRightIcon } from "@mui/icons-material";

export function Welcome() {
  const { login } = useAuth();
  const [data, setData] = useState<GetWelcomeDashboardReadModel>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);


  function handleLogin() {
    login();
  }

  useEffect(() => {
    getWelcomeDashboard()
      .then(response => {
        setData(response);
        setLoading(false);
        console.log(response);
      })
      .catch(error => {
        setError(error);
        setLoading(false);
        console.log(error);
      });
  }, []);

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
                  Letztes Jahr
                </Typography>
                <Typography variant="h5" component="div">
                  {data?.lastYear}
                </Typography>
                <Typography variant="body2">
                  Umsatz: {data?.salesVolumeLastYear} {data?.currency?.sign}
                  <br />
                  Neue Kunden: Coming soon
                  <br />
                  Aufträge: Coming soon
                </Typography>
              </CardContent>
            </Card>
          </Grid>

          <Grid size={{ xs: 12, sm: 6, lg: 4 }} sx={{ display: 'flex' }}>
            <Card sx={{ p: 2, flex: 1, display: 'flex', flexDirection: 'column' }}>
              <CardContent>
                <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                  Dieses Jahr
                </Typography>
                <Typography variant="h5" component="div">
                  {data?.thisYear}
                </Typography>
                <Typography variant="body2">
                  Umsatz: {data?.salesVolumeThisYear} {data?.currency?.sign}
                  <br />
                  Neue Kunden: Coming soon
                  <br />
                  Aufträge: Coming soon
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        </Grid>

        <Grid size={{ xs: 12 }} container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Features
            </Typography>
          </Grid>
          {navConfig.groups.filter(x => x.children.some(y => y.showInNav)).map((group) => (
            group.children.map((child: any) => (
              <Grid key={child.name} size={{ xs: 12, sm: 6, md: 4, lg: 3}}>
                <Card onClick={() => window.location.href = child.route} sx={{ cursor: 'pointer' }}>
                  <CardHeader
                    avatar={
                      <Avatar sx={{ bgcolor: orange[500] }} aria-label="recipe">
                        <SvgIcon component={child.icon} />
                      </Avatar>
                    }
                    action={
                      <IconButton aria-label="settings">
                        <ChevronRightIcon />
                      </IconButton>
                    }
                    title={child.name}
                    subheader={group.name}
                  />
                </Card>
              </Grid>
            ))
          ))}
        </Grid>

        <Grid size={{ xs: 12 }} container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Neu erstellen
            </Typography>
          </Grid>
          {getFlatItems().filter(x => x.showInNav).map((group) => (
            group.children?.filter(y => !y.route.includes(':') && y.route.includes('create')).map((child: any) => (
              <Grid key={child.name}  size={{ xs: 12, sm: 6, md: 4, lg: 4}}>
                <Card onClick={() => window.location.href = child.route} sx={{ cursor: 'pointer' }}>
                  <CardHeader
                    avatar={
                      <Avatar sx={{ bgcolor: orange[500] }} aria-label="recipe">
                        <SvgIcon component={child.icon} />
                      </Avatar>
                    }
                    action={
                      <IconButton aria-label="settings">
                        <AddIcon />
                      </IconButton>
                    }
                    title={child.name}
                    subheader={group.name}
                  />
                </Card>
              </Grid>
            ))
          ))}
        </Grid>

        <Grid container size={{ xs: 12 }} spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Arbeit fortsetzen
            </Typography>
          </Grid>
          Coming soon
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
