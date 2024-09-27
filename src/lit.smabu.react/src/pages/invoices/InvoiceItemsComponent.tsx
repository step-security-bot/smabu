import React, { useEffect, useState } from 'react';
import { InvoiceDTO, InvoiceItemId } from '../../types/domain';
import { IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { Delete, Edit, MoveDown, MoveUp } from '@mui/icons-material';
import axiosConfig from '../../configs/axiosConfig';
import { useNotification } from '../../contexts/notificationContext';

interface InvoiceItemsComponentProps {
    invoiceId: string | undefined;
}

const InvoiceItemsComponent: React.FC<InvoiceItemsComponentProps> = ({ invoiceId }) => {
    
    const [data, setData] = useState<InvoiceDTO>();
    const { toast } = useNotification();

    useEffect(() => {
        axiosConfig.get<InvoiceDTO>(`invoices/${invoiceId}?withItems=true`)
            .then(response => {
                setData(response.data);
            })
            .catch(error => {
                //setError(error);
            });
    }, []);

    const moveItemUp = (itemId: InvoiceItemId) => {
        axiosConfig.put(`invoices/${data?.id?.value}/items/${itemId.value}/moveup`)
            .then((_response) => {
                toast("Position nach oben verschoben", "success");
            })
            .catch(error => {
                //setError(error);
            });
    };

    const moveItemDown = (itemId: InvoiceItemId) => {

        axiosConfig.put(`invoices/${data?.id?.value}/items/${itemId.value}/movedown`)
            .then((_response) => {
                toast("Position nach unten verschoben", "success");
            })
            .catch(error => {
                //setError(error);
            });
    };

    return (
        <TableContainer component={Paper} sx={{ p: 2 }}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell></TableCell>
                        <TableCell><b>Pos.</b></TableCell>
                        <TableCell><b>Details</b></TableCell>
                        <TableCell align="right" style={{ width: 'auto' }}><b>Menge</b></TableCell>
                        <TableCell align="right" style={{ width: 'auto' }}><b>Preis</b></TableCell>
                        <TableCell align="right" style={{ width: 'auto' }}><b>Gesamt</b></TableCell>
                        <TableCell></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {data?.items?.map((item, index) => (
                        <TableRow key={index}>
                            <TableCell>
                                <IconButton size="small" onClick={() => moveItemUp(item.id!)}>
                                    <MoveUp />
                                </IconButton>
                                <IconButton size="small" onClick={() => moveItemDown(item.id!)}>
                                    <MoveDown />
                                </IconButton>
                            </TableCell>
                            <TableCell>
                                {item.position}
                            </TableCell>
                            <TableCell style={{ fontSize: 'small' }}>{item.details}</TableCell>
                            <TableCell align="right">{item.quantity?.value} {item.quantity?.unit}</TableCell>
                            <TableCell align="right">{item.unitPrice?.toFixed(2)} {data?.currency?.isoCode}</TableCell>
                            <TableCell align="right">{item.totalPrice?.toFixed(2)} {data?.currency?.isoCode}</TableCell>
                            <TableCell>
                                <IconButton size="small" LinkComponent="a" href={`/invoices/${data?.id?.value}/items/${item.id?.value}`}>
                                    <Edit />
                                </IconButton>
                                <IconButton size="small" LinkComponent="a" href={`/invoices/${data?.id?.value}/items/${item.id?.value}/delete`}>
                                    <Delete />
                                </IconButton>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default InvoiceItemsComponent;