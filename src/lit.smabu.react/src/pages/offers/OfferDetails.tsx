import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../containers/DefaultContentContainer';
import { Delete, Print, Send } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import { getOffer, getOfferReport, updateOffer } from '../../services/offer.service';
import { OfferDTO } from '../../types/domain';
import OfferItemsComponent from './OfferItemsComponent';
import { deepValueChange } from '../../utils/deepValueChange';
import { openPdf } from '../../utils/openPdf';
import DetailsActions from '../../components/common/DetailsActions';

const OfferDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<OfferDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [errorItems, setErrorItems] = useState(null);
    const [toolbarItems, setToolbarItems] = useState<ToolbarItem[]>([]);

    const loadData = () => getOffer(params.id!, false)
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

    const pdf = () => {
        setLoading(true);
        getOfferReport(params.id!)
            .then((report) => {
                openPdf(report.data, `Angebot_${data?.number?.value}_${data?.customer?.corporateDesign?.shortName}.pdf`);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateOffer(params.id!, {
            id: data?.id!,
            taxRate: data?.taxRate!,
            expiresOn: data?.expiresOn!,
        })
            .then(() => {
                setLoading(false);
                toast("Angebot erfolgreich gespeichert", "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarDetails: ToolbarItem[] = [
        {
            text: "PDF",
            action: () => pdf(),
            icon: <Print />,
        }
    ];

    return (
        <Grid container spacing={2}>
            <Grid size={{ xs: 12 }}>
                <form id="form" onSubmit={handleSubmit} >
                    <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarDetails} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12, sm: 4, md: 4 }}><TextField fullWidth label="#" name="number" value={data?.displayName} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 8, md: 8 }}><TextField fullWidth label="Kunde" name="customer.name" value={data?.customer?.name} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="datetime-local" fullWidth label="Erstellt" name="createdOn" value={data?.createdAt?.toString()} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 6, md: 6 }}><TextField type="date" fullWidth label="Läuft ab" name="expiresOn" value={data?.expiresOn?.toString()} onChange={handleChange} /></Grid>
                                <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Steuer" name="tax" value={data?.taxRate?.rate} required disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 10, md: 10 }}><TextField fullWidth label="Steuerdetails" name="taxDetails" value={data?.taxRate?.details} disabled /></Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </form>
            </Grid>

            <Grid size={{ xs: 12 }}>
                <DetailsActions formId="form" deleteUrl={`/offers/${data?.id?.value}/delete`} disabled={loading} />
            </Grid>

            <Grid size={{ xs: 12, md: 12 }}>
                <DefaultContentContainer title="Positionen" loading={loading} error={errorItems} toolbarItems={toolbarItems} >
                    <OfferItemsComponent offerId={params.id} setError={(error) => setErrorItems(error)} setToolbar={setToolbarItems} />
                </DefaultContentContainer >
            </Grid>
        </Grid>
    );
};

export default OfferDetails;


