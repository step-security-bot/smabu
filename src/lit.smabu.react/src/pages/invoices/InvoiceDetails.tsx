import { useState, useEffect } from 'react';
import axiosConfig from "../../configs/axiosConfig";
import { InvoiceDTO, UpdateInvoiceCommand } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../containers/DefaultContentContainer';
import { deepValueChange } from '../../utils/deepValueChange';
import { Delete } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import InvoiceItemsComponent from './InvoiceItemsComponent';

const InvoiceDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<InvoiceDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axiosConfig.get<InvoiceDTO>(`invoices/${params.id}?withItems=false`)
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        axiosConfig.put<UpdateInvoiceCommand>('invoices/' + params.id, {
            id: data?.id,
            performancePeriod: data?.performancePeriod,
            tax: data?.tax,
            taxDetails: data?.taxDetails
        })
            .then(response => {
                setLoading(false);
                toast("Rechnung erfolgreich gespeichert: " + response.statusText, "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarItems: ToolbarItem[] = [
        {
            text: "Löschen",
            route: `/invoices/${data?.id?.value}/delete`,
            icon: <Delete />
        }
    ];

    return (
        <Grid container spacing={2}>
            <Grid size={{ xs: 12 }}>
                <form id="form" onSubmit={handleSubmit}>
                    <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarItems} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12, sm: 4, md: 4 }}><TextField fullWidth label="#" name="number" value={data?.displayName} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 8, md: 8 }}><TextField fullWidth label="Kunde" name="customer.name" value={data?.customer?.name} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Geschäftsjahr" name="fiscalYear" value={data?.fiscalYear} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 5 }}><TextField type="datetime-local" fullWidth label="Erstellt" name="createdOn" value={data?.createdOn?.toString()} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 5 }}><TextField type="datetime-local" fullWidth label="Freigegeben" name="releasedOn" value={data?.releasedOn} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Steuer" name="tax" value={data?.tax} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 12, sm: 10, md: 10 }}><TextField fullWidth label="Steuerdetails" name="taxDetails" value={data?.taxDetails} onChange={handleChange} /></Grid>
                                <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="date" fullWidth label="Leistung Von" name="performancePeriodFrom" value={data?.performancePeriod?.from} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="date" fullWidth label="Leistung Bis" name="performancePeriodTo" value={data?.performancePeriod?.to} onChange={handleChange} /></Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </form>
            </Grid>

            <Grid size={{ xs: 12 }}>
                <ButtonGroup disabled={loading}>
                    <Button type="submit" variant="contained" form="form" color="success">
                        Speichern
                    </Button>
                </ButtonGroup>
            </Grid>

            <Grid size={{ xs: 12, md: 12 }}>
                <DefaultContentContainer title="Positionen" loading={loading} error={error} >
                    <InvoiceItemsComponent invoiceId={params.id} />
                </DefaultContentContainer >
            </Grid>
        </Grid>
    );
};

export default InvoiceDetails;


