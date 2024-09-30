import { useState, useEffect } from 'react';
import axiosConfig from "../../configs/axiosConfig";
import { InvoiceDTO, AddInvoiceItemCommand } from '../../types/domain';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import DefaultContentContainer from '../../containers/DefaultContentContainer';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import createId from '../../utils/createId';

const InvoiceItemCreate = () => {
    const params = useParams();
    const navigate = useNavigate();
    const { toast } = useNotification();
    const [invoice, setInvoice] = useState<InvoiceDTO>();
    const [data, setData] = useState<AddInvoiceItemCommand>({
        id: createId(),
        invoiceId: { value: params.invoiceId },
        quantity: { value: 0, unit: "" },
        unitPrice: 0,
        details: ""
    });
    const [units, setUnits] = useState<string[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        axiosConfig.get<InvoiceDTO>(`invoices/${params.invoiceId}?withItems=true`)
        .then(response => {
            setInvoice(response.data);
            setLoading(false);
        })
        .catch(error => {
            setError(error);
            setLoading(false);
        });
        axiosConfig.get<string[]>(`common/quantityunits`)
        .then(response => {
            setUnits(response.data);
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
        axiosConfig.post<AddInvoiceItemCommand>(`invoices/${params.invoiceId}/items`, {
            id: data?.id,
            invoiceId: data?.invoiceId,
            quantity: data?.quantity,
            unitPrice: data?.unitPrice,
            details: data?.details            
        })
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
        <Grid container spacing={2}>
            <Grid size={{ xs: 12 }}>
                    <DefaultContentContainer title={invoice?.displayName} subtitle="Position erstellen" loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Position" name="position" value="Wird erstellt" disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 4 }}><TextField fullWidth label="Rechnung" name="invoice" value={invoice?.displayName} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 5, md: 6 }}><TextField fullWidth label="Kunde" name="customer" value={invoice?.customer?.name} disabled /></Grid>

                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Anzahl" name="quantity.value" value={data?.quantity?.value} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}>
                                    <TextField select fullWidth label="Einheit" name="quantity.unit"
                                        value={data?.quantity?.unit} onChange={handleChange} required
                                        slotProps={{
                                            select: {
                                                native: true,
                                            }
                                        }}
                                    >
                                        <option value="" disabled>
                                            Einheit wählen
                                        </option>
                                        {units.map((unit) => (
                                            <option key={unit} value={unit}>
                                                {unit}
                                            </option>
                                        ))}
                                    </TextField>
                                </Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Einzelpreis" name="unitPrice" value={data?.unitPrice} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 6, sm: 6, md: 3 }}><TextField type='number' fullWidth label="Gesamt" name="totalPrice" value={(data.unitPrice * (data.quantity?.value ?? 0))} disabled /></Grid>
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

            <Grid size={{ xs: 12 }}>
                <ButtonGroup disabled={loading}>
                    <Button type="submit" variant="contained" form="form" color="success">
                        Speichern
                    </Button>
                </ButtonGroup>
            </Grid>

            {/* <Grid size={{ xs: 12, md: 12 }}>
                <DefaultContentContainer title="Positionen" loading={loading} error={error} >
                    <InvoiceItemsComponent invoiceId={params.id} />
                </DefaultContentContainer >
            </Grid> */}
        </Grid>
        </form>
    );
};

export default InvoiceItemCreate;


