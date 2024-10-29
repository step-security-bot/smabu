import { useState, useEffect } from 'react';
import { CreateInvoiceCommand, Currency, CustomerDTO, InvoiceDTO, InvoiceId } from '../../types/domain';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import { deepValueChange } from '../../utils/deepValueChange';
import createId from '../../utils/createId';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import DefaultContentContainer from '../../containers/DefaultContentContainer';
import { getCustomers } from '../../services/customer.service';
import { createInvoice, getInvoices } from '../../services/invoice.service';
import { formatDate, formatToDateOnly } from '../../utils/formatDate';

const defaultCurrency: Currency = {
    isoCode: 'EUR',
    name: 'Euro',
    sign: '€'
};

const InvoiceCreate = () => {
    const [searchParams] = useSearchParams();
    const templateId = searchParams.get('templateId');
    
    const [data, setData] = useState<CreateInvoiceCommand>({
        id: createId<InvoiceId>(),
        fiscalYear: new Date().getFullYear(),
        currency: defaultCurrency,
        performancePeriod: { from: formatToDateOnly(new Date().toISOString()) },
        customerId: { value: '' },
        templateId: { value: templateId ?? '' }
    });
    
    const [loading, setLoading] = useState(true);
    const [customers, setCustomers] = useState<CustomerDTO[]>();
    const [invoices, setInvoices] = useState<InvoiceDTO[]>();
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { toast } = useNotification();

    useEffect(() => {
        getCustomers()
            .then(response => {
                setCustomers(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });

        getInvoices()
            .then(response => {
                if (templateId) { 
                    const template = response.data.find(x => x.id?.value === templateId);
                    if (template) {
                        data.customerId = template.customer?.id ?? { value: '' };
                    }
                }
                setInvoices(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleChange = (e: any) => {
        let { name, value } = e.target;
        if (name === 'customerId') {
            value = { value: value };
        }
        if (name === 'templateId') {
            value = { value: value };
            data.customerId = invoices?.find(x => x.id?.value === value.value)?.customer?.id ?? { value: '' };
        }
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        createInvoice(data)
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
                                <Grid size={{ xs: 12, sm: 6 }}>
                                    <TextField select fullWidth label="Vorlage" name="templateId"
                                        value={data?.templateId?.value} onChange={handleChange}
                                        disabled={!!templateId}
                                        slotProps={{
                                            select: {
                                                native: true,
                                            },
                                        }}
                                    >
                                        <option value="">
                                            Keine Vorlage
                                        </option>
                                        {invoices?.map((invoice) => (
                                            <option key={invoice.id?.value} value={invoice.id?.value}>
                                                {invoice.displayName} {formatDate(invoice.releasedAt)} ({invoice.amount?.toFixed(2)} {invoice.currency?.isoCode})
                                            </option>
                                        ))}
                                    </TextField>
                                </Grid>
                                <Grid size={{ xs: 12, sm: 6 }}>
                                    <TextField select fullWidth label="Kunde" name="customerId" required
                                        value={data?.customerId.value} onChange={handleChange}
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
