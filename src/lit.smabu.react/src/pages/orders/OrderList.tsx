import { useEffect, useState } from "react";
import { OrderDTO } from "../../types/domain";
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import DefaultContentContainer, { ToolbarItem } from "../../containers/DefaultContentContainer";
import { Add, Edit } from "@mui/icons-material";
import { getOrders } from "../../services/order.service";

const OrderList = () => {
    const [data, setData] = useState<OrderDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getOrders()
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
            route: "/orders/create",
            icon: <Add />
        }
    ];

    return (
        <DefaultContentContainer loading={loading} error={error} toolbarItems={toolbarItems}>
            <TableContainer component={Paper} >
                <Table size="medium">
                    <TableHead>
                        <TableRow>
                            <TableCell>#</TableCell>
                            <TableCell>Datum</TableCell>
                            <TableCell>Bezeichung</TableCell>
                            <TableCell>Kunde</TableCell>
                            <TableCell>Gruppe</TableCell>
                            <TableCell>Deadline</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.map((order: OrderDTO) => (
                            <TableRow key={order.id?.value}>
                                <TableCell>{order.number!.value}</TableCell>
                                <TableCell>{order.orderDate}</TableCell>
                                <TableCell>{order.name}</TableCell>
                                <TableCell>{order.customer?.name}</TableCell>
                                <TableCell>{order.bunchKey}</TableCell>
                                <TableCell>{order.deadline ? order.deadline?.toString() : ''}</TableCell>
                                <TableCell>
                                    <IconButton size="small" LinkComponent="a" href={`/orders/${order.id?.value}`}>
                                        <Edit />
                                    </IconButton>
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

export default OrderList;