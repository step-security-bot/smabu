import { useState, useEffect } from 'react';
import { CustomerDTO } from '../../types/domain';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/contentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { deleteCustomer, getCustomer } from '../../services/customer.service';
import { DeleteActions } from '../../components/contentBlocks/PageActionsBlock';

const CustomerDelete = () => {
    const [data, setData] = useState<CustomerDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getCustomer(params.id!)
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
        deleteCustomer(params.id!)
            .then((_response) => {
                setLoading(false);
                toast("Kunde erfolgreich gelöscht", "success");
                navigate('/customers');
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DetailPageContainer subtitle={data?.name} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        Soll der Kunde "{data?.name}" wirklich gelöscht werden?
                    </Paper>
                </DetailPageContainer >
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default CustomerDelete;
