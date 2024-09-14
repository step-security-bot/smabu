import { useState, useEffect } from 'react';
import axiosConfig from "../../configs/axiosConfig";
import { CustomerDTO } from '../../types/domain';
import { Button, ButtonGroup, Grid2 as Grid, Paper } from '@mui/material';
import DetailPageContainer from '../../containers/DefaultContentContainer';
import { useNavigate, useParams } from 'react-router-dom';

const CustomerDelete = () => {
    const [data, setData] = useState<CustomerDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams()

    useEffect(() => {
        axiosConfig.get<CustomerDTO>('customers/' + params.id)
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
        axiosConfig.delete('customers/' + params.id)
            .then((_response) => {
                setLoading(false);
                alert("Kunde erfolgreich gelöscht");
                navigate('/customers');
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
                    <DetailPageContainer subtitle={data?.name} loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}>
                            Soll der Kunde "{data?.name}" wirklich gelöscht werden?
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

export default CustomerDelete;
