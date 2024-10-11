import { useState, useEffect } from 'react';
import { Button, ButtonGroup, Grid2 as Grid, Paper } from '@mui/material';
import DetailPageContainer from '../../containers/DefaultContentContainer';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { getOffer, deleteOfferItem } from '../../services/offer.service';
import { OfferDTO, OfferItemDTO } from '../../types/domain';

const OfferDelete = () => {
    const [offer, setOffer] = useState<OfferDTO>();
    const [data, setData] = useState<OfferItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getOffer(params.offerId!, true)
            .then(response => {
                setOffer(response.data);
                setData(response.data.items?.find((item: OfferItemDTO) => item.id!.value === params.id));
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        deleteOfferItem(params.offerId!, params.id!)
            .then((_response) => {
                setLoading(false);
                toast("Position erfolgreich gelöscht", "success");
                navigate(`/offers/${params.offerId}`);
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
                    <DetailPageContainer title={offer?.displayName} subtitle={`Pos: ${data?.displayName}`} loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}>
                            Soll die Position "{data?.position}" wirklich gelöscht werden?
                            <br />
                            Details: {data?.details}
                        </Paper>
                    </DetailPageContainer >
                </Grid>
                <Grid size={{ xs: 12 }}>
                    <ButtonGroup disabled={loading}>
                        <Button type="submit" variant="contained" color="warning">
                            Löschen
                        </Button>
                    </ButtonGroup>
                </Grid>
            </Grid>
        </form>
    );
};

export default OfferDelete;
