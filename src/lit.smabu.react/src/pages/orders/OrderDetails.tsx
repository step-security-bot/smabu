import { useState, useEffect } from 'react';
import { OrderDTO } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Button, ButtonGroup, Chip, Divider, Grid2 as Grid, ListItem, Paper, Stack, Table, TableBody, TableCell, TableHead, TableRow, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../containers/DefaultContentContainer';
import { deepValueChange } from '../../utils/deepValueChange';
import { Add, Delete } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import { getOrder, updateOrder } from '../../services/order.service';

const OrderDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<OrderDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

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

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateOrder(params.id!, {
            id: data?.id!,
            name: data?.name!,
            orderDate: data?.orderDate!,
            deadline: data?.deadline,
            description: data?.description!,
            orderGroup: data?.orderGroup,
            status: data?.status
        })
            .then(response => {
                setLoading(false);
                toast("Auftrag erfolgreich gespeichert: " + response.statusText, "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <Grid container spacing={2}>
            {renderDetails(handleSubmit, data, loading, error, handleChange)}
            {renderReferences(data, loading, error)}
        </Grid>
    );
};

export default OrderDetails;


function renderDetails(handleSubmit: (event: React.FormEvent) => void, data: OrderDTO | undefined, loading: boolean, error: null, handleChange: (e: any) => void) {

    const toolbarItems: ToolbarItem[] = [
        {
            text: "Löschen",
            route: `/orders/${data?.id?.value}/delete`,
            icon: <Delete />
        }
    ];

    return <form id="form" onSubmit={handleSubmit}>
        <Grid container spacing={2}>
            <Grid size={{ xs: 12 }}>
                <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarItems}>
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12, sm: 6, md: 2 }}><TextField fullWidth label="#" name="number" value={data?.number?.value} disabled /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 2 }}><TextField type='date' fullWidth label="Datum" name="orderDate" value={data?.orderDate} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 4 }}><TextField fullWidth label="Kunde" name="customer" value={data?.customer?.name} /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 4 }}><TextField fullWidth label="Gruppe" name="orderGroup" value={data?.orderGroup} onChange={handleChange} /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 8 }}><TextField fullWidth label="Bezeichung" name="name" value={data?.name} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 2 }}><TextField type='datetime-local' fullWidth label="Deadline" name="deadline" value={data?.deadline} onChange={handleChange} /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 2 }}><TextField fullWidth label="Status" name="status" value={data?.status} onChange={handleChange} required /></Grid>
                            <Grid size={{ xs: 12, sm: 12, md: 12 }}><TextField multiline fullWidth label="Beschreibung" name="description" value={data?.description} onChange={handleChange} minRows={4} /></Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer>
            </Grid>
            <Grid size={{ xs: 12 }}>
                <ButtonGroup disabled={loading}>
                    <Button type="submit" variant="contained" color="success">
                        Speichern
                    </Button>
                </ButtonGroup>
            </Grid>
        </Grid>
    </form>;
}

function renderReferences(data: OrderDTO | undefined, loading: boolean, error: null) {

    const toolbarItems: ToolbarItem[] = [
        {
            text: "Angebot",
            icon: <Add />
        },
        {
            text: "Rechnung",
            icon: <Add />
        }
    ];

    return <Grid size={{ xs: 12 }}>
        <DefaultContentContainer title='Verknüpfungen' loading={loading} error={error} toolbarItems={toolbarItems}>
            <Grid container spacing={2}>
                <Grid size={{ xs: 12 }}>
                    <Stack direction="row" spacing={1}
                        sx={{
                            p: 0,
                            justifyContent: "flex-start",
                        }}>
                        <Chip key={"Offers"} clickable label={"Angebote"}
                             variant='outlined' component="a" href="/offers" 
                             sx={{ width: "100px", fontWeight: 600}} />
                        {data?.offers?.map((data) => {
                            return (
                                <Chip clickable key={data.id?.value}
                                    label={data.name}
                                    onDelete={() => { }}
                                />
                            );
                        })}
                        <Chip key={"AddOffer"} clickable variant='outlined' icon={<Add />} label="Hinzufügen" />
                    </Stack>
                </Grid>
                <Grid size={{ xs: 12 }}>
                    <Stack direction="row" spacing={1}
                        sx={{
                            p: 0,
                            justifyContent: "flex-start",
                        }}>
                        <Chip key={"Invoices"} clickable label={"Rechnungen"} 
                            variant='outlined' component="a" href="/invoices"
                            sx={{ width: "100px", fontWeight: 600}} />
                        {data?.invoices?.map((data) => {
                            return (
                                <Chip clickable key={data.id?.value}
                                    label={data.name}
                                    onDelete={() => { }}
                                />
                            );
                        })}
                        <Chip key={"AddInvoice"} clickable variant='outlined' icon={<Add />} label="Hinzufügen" />
                    </Stack>
                </Grid>
            </Grid>
        </DefaultContentContainer>
    </Grid>
}

