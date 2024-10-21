import { Box, Button, Card, CardContent, Container, Grid2 as Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, } from "@mui/material";
import LandingPageImg from "./../../assets/landing-desk.png";
import { useAuth } from "../../contexts/authContext";
import { getWelcomeDashboard } from "../../services/dashboard.service";
import { useEffect, useState } from "react";
import { AuthenticatedTemplate, UnauthenticatedTemplate } from "@azure/msal-react";
import OverviewTabPanel from "./OverviewTabPanel";
import { GetWelcomeDashboardReadModel } from "../../types/domain";
import LandingCharts from '../../assets/landing-charts.jpg';

export function Home() {
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


        <Grid container size={{ xs: 12 }} spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Wertvollste Kunden
            </Typography>
          </Grid>

          <Grid size={{ xs: 12, sm: 6 }} sx={{ display: 'flex' }}>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Seit Beginn</TableCell>
                    <TableCell align="right">Umsatz</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.top3CustomersEver?.map((row) => (
                    <TableRow
                      key={row.name}
                    >
                      <TableCell component="th" scope="row">
                        {row.name}
                      </TableCell>
                      <TableCell align="right">{row.total} {data.currency?.sign}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>

          <Grid size={{ xs: 12, sm: 6 }} sx={{ display: 'flex' }}>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Letzte 12 Monate</TableCell>
                    <TableCell align="right">Umsatz</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.top3CustomersLast12Month?.map((row) => (
                    <TableRow
                      key={row.name}
                    >
                      <TableCell component="th" scope="row">
                        {row.name}
                      </TableCell>
                      <TableCell align="right">{row.total} {data.currency?.sign}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>

          <Grid size={{ xs: 12, sm: 6 }} sx={{ display: 'flex' }}>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Dieses Jahr</TableCell>
                    <TableCell align="right">Umsatz</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.top3CustomersThisYear?.map((row) => (
                    <TableRow
                      key={row.name}
                    >
                      <TableCell component="th" scope="row">
                        {row.name}
                      </TableCell>
                      <TableCell align="right">{row.total} {data.currency?.sign}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>

          <Grid size={{ xs: 12, sm: 6 }} sx={{ display: 'flex' }}>
            <TableContainer component={Paper}>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell>Letztes Jahr</TableCell>
                    <TableCell align="right">Umsatz</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data?.top3CustomersLastYear?.map((row) => (
                    <TableRow
                      key={row.name}
                    >
                      <TableCell component="th" scope="row">
                        {row.name}
                      </TableCell>
                      <TableCell align="right">{row.total} {data.currency?.sign}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
        </Grid>

        <Grid container spacing={2}>
          <Grid size={{ xs: 12 }}>
            <Typography variant="h6" component="h1" sx={{ mt: 4 }}>
              Umsätze nach Monaten
            </Typography>
          </Grid>
          <Grid size={{ xs: 12 }}>
            <Paper elevation={2} sx={{ p: 0 }}>
              <Box>
                <img src={LandingCharts} alt="Charts" style={{ width: '100%' }} />
              </Box>
            </Paper>
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
