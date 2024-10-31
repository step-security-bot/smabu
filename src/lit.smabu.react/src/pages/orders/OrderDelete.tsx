import { useState, useEffect } from 'react';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/contentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { OrderDTO } from '../../types/domain';
import { deleteOrder, getOrder } from '../../services/order.service';
import { DeleteActions } from '../../components/contentBlocks/PageActionsBlock';

const OrderDelete = () => {
    const [data, setData] = useState<OrderDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getOrder(params.id!)
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        deleteOrder(params.id!)
            .then((_response) => {
                setLoading(false);
                toast("Auftrag erfolgreich gelöscht", "success");
                navigate('/orders');
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DetailPageContainer subtitle={data?.number?.value?.toString()} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        Soll der Auftrag "{data?.number?.value}" wirklich gelöscht werden?
                    </Paper>
                </DetailPageContainer >
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default OrderDelete;