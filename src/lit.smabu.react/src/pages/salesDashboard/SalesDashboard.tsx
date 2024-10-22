import { Container, Grid2 as Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, } from "@mui/material";
import { getSalesDashboard } from "../../services/dashboard.service";
import { useEffect, useState } from "react";
import { GetSalesDashboardReadModel } from "../../types/domain";

export function SalesDashboard() {
  const [data, setData] = useState<GetSalesDashboardReadModel>();
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    getSalesDashboard()
      .then(response => {
        setData(response);
        setLoading(false);
      })
      .catch(error => {
        setError(error);
        setLoading(false);
        console.log(error);
      });
  }, []);

  return (
    <Container>
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
    </Container>
  );
}
