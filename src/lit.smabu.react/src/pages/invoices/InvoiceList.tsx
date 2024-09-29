import { useEffect, useState } from "react";
import axiosConfig from "../../configs/axiosConfig";
import { InvoiceDTO } from "../../types/domain";
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from "@mui/material";
import DefaultContentContainer, { ToolbarItem } from "../../containers/DefaultContentContainer";
import { Add, Edit } from "@mui/icons-material";
import { formatDate } from "../../utils/formatDate";

const InvoiceList = () => {
    const [data, setData] = useState<InvoiceDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axiosConfig.get<InvoiceDTO[]>('invoices')
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
            route: "/invoices/create",
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
                            <TableCell>Jahr</TableCell>
                            <TableCell>Erstellt</TableCell>
                            <TableCell>Kunde</TableCell>
                            <TableCell>Summe</TableCell>
                            <TableCell>Freigegeben</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {data.map((invoice: InvoiceDTO) => (
                            <TableRow key={invoice.id?.value}>
                                <TableCell>{invoice.number!.value}</TableCell>
                                <TableCell>{invoice.fiscalYear}</TableCell>
                                <TableCell>{formatDate(invoice.createdOn)}</TableCell>
                                <TableCell>{invoice.customer?.name}</TableCell>
                                <TableCell align="right">{invoice.amount?.toFixed(2)} {invoice.currency?.isoCode}</TableCell>
                                <TableCell>{formatDate(invoice?.releasedOn)}</TableCell>
                                <TableCell>
                                    <IconButton size="small" LinkComponent="a" href={`/invoices/${invoice.id?.value}`}>
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

export default InvoiceList;