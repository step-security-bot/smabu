import { useState, useEffect } from 'react';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/ContentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { getOffer, deleteOfferItem } from '../../services/offer.service';
import { OfferDTO, OfferItemDTO } from '../../types/domain';
import { DeleteActions } from '../../components/ContentBlocks/PageActionsBlock';

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
            <Stack spacing={2}>
                <DetailPageContainer title={offer?.displayName} subtitle={`Pos: ${data?.displayName}`} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        Soll die Position "{data?.position}" wirklich gelöscht werden?
                        <br />
                        Details: {data?.details}
                    </Paper>
                </DetailPageContainer >
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default OfferDelete;
