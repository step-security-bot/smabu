import { useEffect, useState } from "react";
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import DefaultContentContainer, { ToolbarItem } from "../../components/contentBlocks/DefaultContentBlock";
import { Add, Edit, Warning } from "@mui/icons-material";
import { handleAsyncTask } from "../../utils/handleAsyncTask";
import { PaymentDTO } from "../../types/domain";
import { getPayments } from "../../services/payments.services";
import { formatDate } from "../../utils/formatDate";

const PaymentList = () => {
    const [data, setData] = useState<PaymentDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);
    const toolbarItems: ToolbarItem[] = [
        {
            text: "Neu",
            route: "/payments/create",
            icon: <Add />
        }
    ];

    useEffect(() => {
        handleAsyncTask({
            task: () => getPayments(),
            onLoading: setLoading,
            onSuccess: setData,
            onError: setError
        });
    }, []);

    return (
        <DefaultContentContainer loading={loading} error={error} toolbarItems={toolbarItems}>
            <TableContainer component={Paper} >
                <Table size="medium">
                    <TableHead>
                        <TableRow>
                            <TableCell>Status</TableCell>
                            <TableCell>Bezeichnung</TableCell>
                            <TableCell>Fällig</TableCell>

                            <TableCell align="right"></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data
                            .sort((a, b) => b.number?.value! - a.number?.value!)
                            .map((payment: PaymentDTO) => (
                                <TableRow key={payment.id?.value}>
                                    <TableCell>{payment.status?.value}</TableCell>
                                    <TableCell>{payment.displayName}</TableCell>
                                    <TableCell>
                                        <Typography sx={{ display: "flex", alignItems: "center" }}>
                                            {formatDate(payment.dueDate)}
                                            {payment.isOverdue && <Warning color="warning" />}
                                        </Typography>
                                    </TableCell>
                                    <TableCell align="right">
                                        <IconButton size="small" LinkComponent="a" href={`/payments/${payment.id?.value}`}>
                                            <Edit />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </DefaultContentContainer>
    );
}

export default PaymentList;