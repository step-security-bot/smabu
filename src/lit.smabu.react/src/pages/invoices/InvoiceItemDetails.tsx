import { useState, useEffect } from 'react';
import { InvoiceDTO, InvoiceItemDTO } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import { getInvoice, updateInvoiceItem } from '../../services/invoice.service';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { UnitSelectField } from '../../components/controls/SelectField';

const InvoiceItemDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [invoice, setInvoice] = useState<InvoiceDTO>();
    const [data, setData] = useState<InvoiceItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const loadData = () => getInvoice(params.invoiceId!, true)
        .then(response => {
            setInvoice(response.data);
            setData(response.data.items?.find((item: InvoiceItemDTO) => item.id!.value === params.invoiceItemId));
            setLoading(false);
        })
        .catch(error => {
            setError(error);
            setLoading(false);
        });

    useEffect(() => {
        loadData();
    }, []);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateInvoiceItem(params.invoiceId!, params.invoiceItemId!, {
            id: data?.id!,
            invoiceId: data?.invoiceId!,
            quantity: data?.quantity!,
            unitPrice: data?.unitPrice!,
            details: data?.details!
        })
            .then(() => {
                setLoading(false);
                setError(null);
                toast("Rechnungsposition erfolgreich gespeichert", "success");
                loadData();
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarItems: ToolbarItem[] = [];

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <Grid size={{ xs: 12 }}>
                    <DefaultContentContainer title={invoice?.displayName} subtitle={"#" + data?.displayName} loading={loading} error={error} toolbarItems={toolbarItems} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Position" name="position" value={data?.position} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 4 }}><TextField fullWidth label="Rechnung" name="invoice" value={invoice?.displayName} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 6 }}><TextField fullWidth label="Kunde" name="customer" value={invoice?.customer?.name} disabled /></Grid>

                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Anzahl" name="quantity.value" value={data?.quantity?.value} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}>
                                    <UnitSelectField name="quantity.unit" value={data?.quantity?.unit?.value} required
                                        onChange={handleChange} label={'Einheit'} />
                                </Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Einzelpreis" name="unitPrice" value={data?.unitPrice} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Gesamt" name="totalPrice" value={data?.totalPrice} disabled /></Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </Grid>

                <Grid size={{ xs: 12 }}>
                    <DefaultContentContainer title="Details" loading={loading}>
                        <TextField multiline variant='filled' minRows={5} maxRows={10} fullWidth
                            name="details" value={data?.details} onChange={handleChange} />
                    </DefaultContentContainer>
                </Grid>

                <DetailsActions formId="form" deleteUrl={`/invoices/${params.invoiceId}/items/${data?.id?.value}/delete`} disabled={loading} />
            </Stack>
        </form>
    );
};

export default InvoiceItemDetails;


