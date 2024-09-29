import { useState, useEffect } from 'react';
import axiosConfig from "../../configs/axiosConfig";
import { CreateInvoiceCommand, Currency, CustomerDTO, InvoiceId } from '../../types/domain';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import { deepValueChange } from '../../utils/deepValueChange';
import createId from '../../utils/createId';
import { useNavigate } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import DefaultContentContainer from '../../containers/DefaultContentContainer';
import { formatToDateOnly } from '../../utils/formatDate';

const defaultCurrency: Currency = {
    isoCode: 'EUR',
    name: 'Euro',
    sign: '€'
};

const InvoiceCreate = () => {
    const [data, setData] = useState<CreateInvoiceCommand>({
        id: createId<InvoiceId>(),
        fiscalYear: new Date().getFullYear(),
        currency: defaultCurrency,
        performancePeriod: { from: formatToDateOnly(new Date().toISOString()) },
        customerId: { value: '' },
        tax: 0
    });
    const [loading, setLoading] = useState(true);
    const [customers, setCustomers] = useState<CustomerDTO[]>();
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { toast } = useNotification();

    useEffect(() => {
        axiosConfig.get<CustomerDTO[]>('customers')
            .then(response => {
                setCustomers(response.data);
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
        axiosConfig.post<CreateInvoiceCommand>('invoices', {
            id: data!.id,
            fiscalYear: data!.fiscalYear,
            customerId: { value: data!.customerId },
            currency: data!.currency,
            tax: data!.tax
        })
            .then((_response) => {
                setLoading(false);
                toast("Rechnung erfolgreich erstellt", "success");
                navigate(`/invoices/${data.id.value}`);
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
                    <DefaultContentContainer loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={1}>
                                <Grid size={{ xs: 6 }}><TextField type='number' fullWidth label="Geschäftsjahr" name="fiscalYear" value={data?.fiscalYear} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 6 }}><TextField fullWidth label="Währung" name="currency" value={data?.currency.isoCode} onChange={handleChange} required disabled /></Grid>
                                <Grid size={{ xs: 12 }}>
                                    <TextField select fullWidth label="Kunde" name="customerId"
                                        value={data?.customerId.value} onChange={handleChange} required
                                        slotProps={{
                                            select: {
                                                native: true,
                                            },
                                        }}
                                    >
                                        <option value="" disabled>
                                            Kunde auswählen
                                        </option>
                                        {customers?.map((customer) => (
                                            <option key={customer.id?.value} value={customer.id?.value}>
                                                {customer.name}
                                            </option>
                                        ))}
                                    </TextField>
                                </Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </Grid>
                <Grid size={{ xs: 12 }}>
                    <ButtonGroup>
                        <Button type="submit" variant="contained" color="success">
                            Erstellen
                        </Button>
                    </ButtonGroup>
                </Grid>
            </Grid>
        </form>
    );
};

export default InvoiceCreate;
