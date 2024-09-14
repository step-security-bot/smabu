import { useEffect, useState } from "react";
import axiosConfig from "../../configs/axiosConfig";
import { CustomerDTO } from "../../types/domain";
import { Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import { Link } from "react-router-dom";
import DefaultContentContainer, { ToolbarItem } from "../../containers/DefaultContentContainer";
import { Add } from "@mui/icons-material";

const CustomerList = () => {
    const [data, setData] = useState<CustomerDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axiosConfig.get<CustomerDTO[]>('customers')
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);
    
    const toolbarItems: ToolbarItem[] = [
        {
            text: "Neu",
            route: "/customers/create",
            icon: <Add />
        }
    ];

    return (
        <DefaultContentContainer loading={loading} error={error} toolbarItems={toolbarItems}>
            <TableContainer component={Paper} >
                <Table stickyHeader size="medium" >
                    <TableHead>
                        <TableRow>
                            <TableCell>#</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Kurzname</TableCell>
                            <TableCell>Branche</TableCell>
                            <TableCell>Ort</TableCell>
                            <TableCell></TableCell>
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
                                <TableCell>
                                    <Link to={`/customers/${customer.id?.value}`}>
                                        Details
                                    </Link>
                                </TableCell>
                                {/* Add more table cells as needed */}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </DefaultContentContainer>
    );
}

export default CustomerList;