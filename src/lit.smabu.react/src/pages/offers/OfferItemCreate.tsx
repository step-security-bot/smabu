import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import createId from '../../utils/createId';
import { getQuantityUnits } from '../../services/common.service';
import { getOffer, addOfferItem } from '../../services/offer.service';
import { OfferDTO, AddOfferItemCommand } from '../../types/domain';
import { CreateActions } from '../../components/contentBlocks/PageActionsBlock';

const OfferItemCreate = () => {
    const params = useParams();
    const navigate = useNavigate();
    const { toast } = useNotification();
    const [offer, setOffer] = useState<OfferDTO>();
    const [data, setData] = useState<AddOfferItemCommand>({
        id: createId(),
        offerId: { value: params.offerId },
        quantity: { value: 0, unit: "" },
        unitPrice: 0,
        details: ""
    });
    const [units, setUnits] = useState<string[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        getOffer(params.offerId!, true)
            .then(response => {
                setOffer(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
        getQuantityUnits()
            .then(response => {
                setUnits(response);
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
        addOfferItem(params.offerId!, data)
            .then(() => {
                setLoading(false);
                setError(null);
                toast("Angebotsposition erfolgreich erstellt", "success");
                navigate(`/offers/${params.offerId}`);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DefaultContentContainer title={offer?.displayName} subtitle="Position erstellen" loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12, sm: 2, md: 2 }}><TextField fullWidth label="Position" name="position" value="Wird erstellt" disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 4 }}><TextField fullWidth label="Angebot" name="offer" value={offer?.displayName} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 5, md: 6 }}><TextField fullWidth label="Kunde" name="customer" value={offer?.customer?.name} disabled /></Grid>

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

                <DefaultContentContainer title="Details" loading={loading}>
                    <TextField multiline variant='filled' minRows={5} maxRows={10} fullWidth
                        name="details" value={data?.details} onChange={handleChange} />
                </DefaultContentContainer>

                <CreateActions formId="form" disabled={loading} />
            </Stack>
        </form>
    );
};

export default OfferItemCreate;


