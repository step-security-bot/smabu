import { useState, useEffect } from 'react';
import { CreateCustomerCommand, CustomerId } from '../../types/domain';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import { deepValueChange } from '../../utils/deepValueChange';
import createId from '../../utils/createId';
import { useNavigate } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import DefaultContentContainer from '../../components/ContentBlocks/DefaultContentBlock';
import { createCustomer } from '../../services/customer.service';
import { CreateActions } from '../../components/ContentBlocks/PageActionsBlock';

const CustomerCreate = () => {
    const [data, setData] = useState<CreateCustomerCommand>({
        id: createId<CustomerId>(),
        name: '',
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const { toast } = useNotification();

    useEffect(() => {
        setLoading(false);
    }, []);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        createCustomer({
            id: data!.id,
            name: data!.name
        })
            .then((_response) => {
                setLoading(false);
                toast("Kunde erfolgreich erstellt", "success");
                navigate(`/customers/${data.id.value}`);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DefaultContentContainer subtitle={data?.name} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={1}>
                            <Grid size={{ xs: 12 }}><TextField fullWidth label="Name" name="name" value={data?.name} onChange={handleChange} required /></Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer >
                <CreateActions formId="form" disabled={loading} />
            </Stack>
        </form>
    );
};

export default CustomerCreate;
