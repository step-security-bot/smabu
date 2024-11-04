import { useState, useEffect } from 'react';
import { InvoiceDTO, AddInvoiceItemCommand } from '../../types/domain';
import { useNavigate, useParams } from 'react-router-dom';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import createId from '../../utils/createId';
import { addInvoiceItem, getInvoice } from '../../services/invoice.service';
import { CreateActions } from '../../components/contentBlocks/PageActionsBlock';
import { UnitSelectField } from '../../components/controls/SelectField';

const InvoiceItemCreate = () => {
    const params = useParams();
    const navigate = useNavigate();
    const { toast } = useNotification();
    const [invoice, setInvoice] = useState<InvoiceDTO>();
    const [data, setData] = useState<AddInvoiceItemCommand>({
        invoiceItemId: createId(),
        invoiceId: { value: params.invoiceId },
        quantity: { value: 0, unit: undefined },
        unitPrice: 0,
        details: ""
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getInvoice(params.invoiceId!, true)
            .then(response => {
                setInvoice(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleChange = (e: any) => {
        let { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        addInvoiceItem(params.invoiceId!, data)
            .then(() => {
                setLoading(false);
                setError(null);
                toast("Rechnungsposition erfolgreich erstellt", "success");
                navigate(`/invoices/${params.invoiceId}`);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DefaultContentContainer title={invoice?.displayName} subtitle="Position erstellen" loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Position" name="position" value="Wird erstellt" disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 4 }}><TextField fullWidth label="Rechnung" name="invoice" value={invoice?.displayName} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 6 }}><TextField fullWidth label="Kunde" name="customer" value={invoice?.customer?.name} disabled /></Grid>

                            <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Anzahl" name="quantity.value" value={data?.quantity?.value} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 6, sm: 6, md: 3 }}>
                                <UnitSelectField label="Einheit" name="quantity.unit" value={data?.quantity?.unit?.value} required onChange={handleChange} />
                            </Grid>
                            <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Einzelpreis" name="unitPrice" value={data?.unitPrice} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Gesamt" name="totalPrice" value={(data.unitPrice * (data.quantity?.value ?? 0))} disabled /></Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer >

                <DefaultContentContainer title="Details" loading={loading}>
                    <TextField multiline variant='filled' minRows={5} maxRows={10} fullWidth
                        name="details" value={data?.details} onChange={handleChange} />
                </DefaultContentContainer>

                <CreateActions formId="form" />
            </Stack>
        </form>
    );
};

export default InvoiceItemCreate;


