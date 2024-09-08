import { useEffect, useState } from "react";
import axiosConfig from "../../../configs/axiosConfig";
import { CustomerDTO } from "../../../types/domain";
import { Container, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Toolbar, Typography } from "@mui/material";
import { AddCircleOutline } from "@mui/icons-material";

const CustomerList = () => {

    const [data, setData] = useState<CustomerDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        // Beispiel für eine GET-Anfrage
        axiosConfig.get<CustomerDTO[]>('customers')
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
                console.log(99, error);
            });
    }, []);

    if (loading) return <div>Loading...</div>;
    // if (error) return <div>Error: {error}</div>;

    return (
        <Container>
            <Toolbar
                variant="dense" 
                sx={[
                    {
                        pl: { sm: 2 },
                        pr: { xs: 1, sm: 1 },
                    },
                ]}>
                <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                    Kunden
                </Typography>
                <div>
                <IconButton
                size="large"
                aria-label="account of current user"
                aria-controls="menu-appbar"
                aria-haspopup="true"
                color="inherit"
              >
                <AddCircleOutline />
              </IconButton>
                </div>
            </Toolbar>
            <TableContainer component={Paper} >
                <Table stickyHeader size="medium" >
                    <TableHead>
                        <TableRow>
                            <TableCell>#</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Kurzname</TableCell>
                            <TableCell>Branche</TableCell>
                            <TableCell>Ort</TableCell>
                            {/* Add more table headers as needed */}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.map((customer: CustomerDTO) => (
                            <TableRow key={customer.id?.value}>
                                <TableCell>{customer.number!.value}</TableCell>
                                <TableCell>{customer.name}</TableCell>
                                <TableCell>{customer.shortName}</TableCell>
                                <TableCell>{customer.industryBranch}</TableCell>
                                <TableCell>{customer.mainAddress?.city}</TableCell>
                                {/* Add more table cells as needed */}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
}

export default CustomerList;