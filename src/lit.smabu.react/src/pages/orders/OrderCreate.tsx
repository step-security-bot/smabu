import { useState, useEffect } from 'react';
import { Button, ButtonGroup, Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import { deepValueChange } from '../../utils/deepValueChange';
import createId from '../../utils/createId';
import { useNavigate } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import DefaultContentContainer from '../../components/ContentBlocks/DefaultContentBlock';
import { createOrder } from '../../services/order.service';
import { CreateOrderCommand, CustomerDTO, OrderId } from '../../types/domain';
import { getCustomers } from '../../services/customer.service';
import { CreateActions } from '../../components/ContentBlocks/PageActionsBlock';

const OrderCreate = () => {
    const [data, setData] = useState<CreateOrderCommand>({
        id: createId<OrderId>(),
        name: 'Neuer Auftrag',
        orderDate: new Date(),
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [customers, setCustomers] = useState<CustomerDTO[]>();
    const navigate = useNavigate();
    const { toast } = useNotification();

    useEffect(() => {
        setLoading(true);
        getCustomers()
            .then(response => {
                setCustomers(response.data);
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
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        createOrder({
            id: data!.id,
            name: data!.name,
            orderDate: data!.orderDate,
            customerId: data!.customerId
        })
            .then((_response) => {
                setLoading(false);
                toast("Auftrag erfolgreich erstellt", "success");
                navigate(`/orders/${data?.id?.value}`);
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
                            <Grid size={{ xs: 12, sm: 6 }}>
                                <TextField select fullWidth label="Kunde" name="customerId"
                                    value={data?.customerId?.value} onChange={handleChange} required
                                    slotProps={{
                                        select: {
                                            native: true,
                                        }
                                    }}
                                >
                                    <option value="" disabled>
                                        Einheit wählen
                                    </option>
                                    {customers?.map((customer) => (
                                        <option key={customer.id?.value} value={customer.id?.value}>
                                            {customer.name}
                                        </option>
                                    ))}
                                </TextField>
                            </Grid>
                            <Grid size={{ xs: 12, sm: 6 }}><TextField type='date' fullWidth label="Datum" name="orderDate" value={data?.orderDate?.toISOString().split('T')[0]} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 12, sm: 12 }}><TextField fullWidth label="Name" name="name" value={data?.name} onChange={handleChange} required /></Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer >
                <CreateActions formId="form" disabled={loading} />
            </Stack>
        </form>
    );
};

export default OrderCreate;
