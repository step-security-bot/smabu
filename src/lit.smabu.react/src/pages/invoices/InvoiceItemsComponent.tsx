import React, { useEffect, useState } from 'react';
import { InvoiceDTO, InvoiceItemId } from '../../types/domain';
import { Button, ButtonGroup, Table, TableBody, TableCell, TableContainer, TableHead, TableRow } from '@mui/material';
import { Delete, Edit, MoveDown, MoveUp } from '@mui/icons-material';
import axiosConfig from '../../configs/axiosConfig';
import { useNotification } from '../../contexts/notificationContext';

interface InvoiceItemsComponentProps {
    invoiceId: string | undefined;
    setError: (error: any) => void;
}

const InvoiceItemsComponent: React.FC<InvoiceItemsComponentProps> = ({ invoiceId, setError }) => {

    const [data, setData] = useState<InvoiceDTO>();
    const { toast } = useNotification();

    const loadData = () => axiosConfig.get<InvoiceDTO>(`invoices/${invoiceId}?withItems=true`)
        .then(response => {
            setError(null);
            setData(response.data);
        })
        .catch(error => {
            setError(error);
        });
    useEffect(() => {
        loadData();
    }, []);

    const moveItemUp = (itemId: InvoiceItemId) => {
        axiosConfig.put(`invoices/${data?.id?.value}/items/${itemId.value}/moveup`)
            .then((_response) => {
                toast("Position nach oben verschoben", "success");
                loadData();
            })
            .catch(error => {
                setError(error);
            });
    };

    const moveItemDown = (itemId: InvoiceItemId) => {
        axiosConfig.put(`invoices/${data?.id?.value}/items/${itemId.value}/movedown`)
            .then((_response) => {
                toast("Position nach unten verschoben", "success");
                loadData();
            })
            .catch(error => {
                setError(error);
            });
    };

    return (
        <TableContainer sx={{ p: 0 }}>
            <Table size='small' >
                <TableHead>
                    <TableRow>
                        <TableCell variant='head'><b>Pos.</b></TableCell>
                        <TableCell variant='head'></TableCell>
                        <TableCell variant='head' align="right" style={{ width: 'auto' }}><b>Menge</b></TableCell>
                        <TableCell variant='head' align="right" style={{ width: 'auto' }}><b>Preis</b></TableCell>
                        <TableCell variant='head' align="right" style={{ width: 'auto' }}><b>Gesamt</b></TableCell>
                        <TableCell variant='head'></TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {data?.items?.map((item, index) => (
                        <React.Fragment  key={`${index}-1`}>
                            <TableRow key={index}>
                                <TableCell sx={{ borderBottom: 'none' }}>{item.position}</TableCell>
                                <TableCell component="th" scope="row" sx={{ borderBottom: 'none' }}></TableCell>
                                <TableCell sx={{ borderBottom: 'none' }} align="right">{item.quantity?.value} {item.quantity?.unit}</TableCell>
                                <TableCell sx={{ borderBottom: 'none' }} align="right">{item.unitPrice?.toFixed(2)} {data?.currency?.isoCode}</TableCell>
                                <TableCell sx={{ borderBottom: 'none' }} align="right">{item.totalPrice?.toFixed(2)} {data?.currency?.isoCode}</TableCell>
                                <TableCell sx={{ verticalAlign: 'top' }} rowSpan={2}>
                                    <ButtonGroup variant="text" size='small' color='secondary'>
                                        <Button onClick={() => moveItemUp(item.id!)}><MoveUp /></Button>
                                        <Button onClick={() => moveItemDown(item.id!)}><MoveDown /></Button>
                                        <Button LinkComponent="a" href={`/invoices/${data?.id?.value}/items/${item.id?.value}`}><Edit /></Button>
                                        <Button LinkComponent="a" color='error' href={`/invoices/${data?.id?.value}/items/${item.id?.value}/delete`}><Delete /></Button>
                                    </ButtonGroup>
                                </TableCell>
                            </TableRow>
                            <TableRow key={`${index}-2`}>
                                <TableCell colSpan={6} sx={{ fontSize: 'small' }}>{item.details}</TableCell>
                            </TableRow>
                        </React.Fragment>
                    ))}

                    <TableRow key="last">
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell></TableCell>
                        <TableCell variant='footer' align="right" style={{ fontWeight: 'bold' }}>{data?.amount?.toFixed(2)} {data?.currency?.isoCode}</TableCell>
                        <TableCell></TableCell>
                    </TableRow>
                </TableBody>
            </Table>
        </TableContainer>
    );
};

export default InvoiceItemsComponent;