import { useState, useEffect } from 'react';
import { InvoiceDTO } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { CancelScheduleSend, ContentCopy, Print, Send } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import InvoiceItemsComponent from './InvoiceItemsComponent';
import { getInvoice, getInvoiceReport, releaseInvoice, updateInvoice, withdrawReleaseInvoice } from '../../services/invoice.service';
import { openPdf } from '../../utils/openPdf';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { formatForTextField } from '../../utils/formatDate';

const InvoiceDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<InvoiceDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);
    const [errorItems, setErrorItems] = useState(undefined);
    const [toolbarItems, setToolbarItems] = useState<ToolbarItem[]>([]);

    const loadData = () => getInvoice(params.invoiceId!, false)
        .then(response => {
            setData(response.data);
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
        updateInvoice(params.invoiceId!, {
            id: data?.id!,
            performancePeriod: data?.performancePeriod!,
            taxRate: data?.taxRate!,
        })
            .then(() => {
                setLoading(false);
                toast("Rechnung erfolgreich gespeichert", "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const release = () => {
        setLoading(true);
        releaseInvoice(params.invoiceId!, {
            id: data?.id!,
            releasedAt: undefined            
        })
            .then(() => {
                setLoading(false);
                toast("Rechnung freigegeben", "success");
                loadData();
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const withdrawRelease = () => {
        setLoading(true);
        withdrawReleaseInvoice(params.invoiceId!, {
            id: data?.id!,
        })
            .then(() => {
                setLoading(false);
                toast("Rechnungsfreigabe entzogen", "success");
                loadData();
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const pdf = () => {
        setLoading(true);
        getInvoiceReport(params.invoiceId!)
            .then((report) => {
                openPdf(report.data, `Rechnung_${data?.number?.value}_${data?.customer?.corporateDesign?.shortName}.pdf`);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarDetails: ToolbarItem[] = [
        {
            text: "Freigeben",
            action: () => data?.isReleased ? withdrawRelease() : release(),
            icon: data?.isReleased ? <CancelScheduleSend /> : <Send />,     
            color: data?.isReleased ? "warning" : "success",
            title: data?.isReleased ? "Freigabe entziehen" : "Freigeben"     
        },
        {
            text: "PDF",
            action: pdf,
            icon: <Print />,     
        },
        {
            text: "Kopieren",
            route: `/invoices/create?templateId=${data?.id?.value}`,
            icon: <ContentCopy />,     
        }
    ];

    return (
        <Stack spacing={2}>
            <form id="form" onSubmit={handleSubmit} >
                <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarDetails} >
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12, sm: 4, md: 4 }}><TextField fullWidth label="#" name="number" value={data?.displayName} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 8, md: 8 }}><TextField fullWidth label="Kunde" name="customer.name" value={data?.customer?.name} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Geschäftsjahr" name="fiscalYear" value={data?.fiscalYear} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 5 }}><TextField type="datetime-local" fullWidth label="Erstellt" name="createdOn" value={formatForTextField(data?.createdAt)} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 5 }}><TextField type="datetime-local" fullWidth label="Freigegeben" name="releasedOn" value={formatForTextField(data?.releasedAt)} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Steuer" name="tax" value={data?.taxRate?.rate} required disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 10, md: 10 }}><TextField fullWidth label="Steuerdetails" name="taxDetails" value={data?.taxRate?.details} disabled/></Grid>
                            <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="date" fullWidth label="Leistung Von" name="performancePeriod.from" value={data?.performancePeriod?.from} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="date" fullWidth label="Leistung Bis" name="performancePeriod.to" value={data?.performancePeriod?.to} onChange={handleChange} /></Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer>
            <DetailsActions formId="form" deleteUrl={`/invoices/${data?.id?.value}/delete`} disabled={loading || data?.isReleased}/> 
            </form>
          
            <DefaultContentContainer title="Positionen" loading={loading} error={errorItems} toolbarItems={toolbarItems} >
                <InvoiceItemsComponent invoiceId={params.invoiceId} setError={(error) => setErrorItems(error)} setToolbar={setToolbarItems} />
            </DefaultContentContainer >
        </Stack>
    );
};

export default InvoiceDetails;


