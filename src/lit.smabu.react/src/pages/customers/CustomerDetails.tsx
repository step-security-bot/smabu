import { useState, useEffect } from 'react';
import { CustomerDTO } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../containers/DefaultContentContainer';
import { deepValueChange } from '../../utils/deepValueChange';
import { Delete } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import { getCustomer, updateCustomer } from '../../services/customer.service';

const CustomerDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<CustomerDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

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

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateCustomer(params.id!, {
            id: data?.id!,
            name: data?.name!,
            //shortName: data?.shortName!,
            industryBranch: data?.industryBranch!,
            mainAddress: data?.mainAddress,
            communication: data?.communication
        })
            .then(response => {
                setLoading(false);
                toast("Kunde erfolgreich gespeichert: " + response.statusText, "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };
    
    const toolbarItems: ToolbarItem[] = [
        {
            text: "Löschen",
            route: `/customers/${data?.id?.value}/delete`,
            icon: <Delete />
        }
    ];

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Grid container spacing={2}>
                <Grid size={{ xs: 12 }}>
                    <DefaultContentContainer subtitle={data?.name} loading={loading} error={error} toolbarItems={toolbarItems} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12, sm: 6, md: 3}}><TextField fullWidth label="#" name="number" value={data?.displayName} disabled /></Grid>
                                <Grid size={{ xs: 12, sm: 6, md: 3}}>
                                    <TextField fullWidth label="Kurzname" name="shortName" value={data?.shortName} required 
                                        disabled slotProps={{ htmlInput: { minLength: 5, maxLength: 5 } }} />
                                    </Grid>
                                <Grid size={{ xs: 12, sm: 12, md: 6}}><TextField fullWidth label="Name" name="name" value={data?.name} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 12, sm: 8, md: 8}}><TextField fullWidth label="Branche" name="industryBranch" value={data?.industryBranch} onChange={handleChange} required /></Grid>
                                <Grid size={{ xs: 12, sm: 4, md: 4}}><TextField fullWidth label="Währung" name="currency" value={data?.currency?.name} disabled /></Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </Grid>
                <Grid size={{ xs: 12, md: 8 }}>
                    <DefaultContentContainer title="Adresse" loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}><div>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Name 1" name="mainAddress.name1" value={data?.mainAddress?.name1} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Name 2" name="mainAddress.name2" value={data?.mainAddress?.name2} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 9, sm: 8}}>
                                    <TextField fullWidth label="Straße" name="mainAddress.street" value={data?.mainAddress?.street} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 3, sm: 4 }}>
                                    <TextField fullWidth label="Nr." name="mainAddress.houseNumber" value={data?.mainAddress?.houseNumber} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12, sm: 3 }}>
                                    <TextField fullWidth label="PLZ" name="mainAddress.postalCode" value={data?.mainAddress?.postalCode} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12, sm: 6 }}>
                                    <TextField fullWidth label="Ort" name="mainAddress.city" value={data?.mainAddress?.city} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12, sm: 3 }}>
                                    <TextField fullWidth label="Land" name="mainAddress.country" value={data?.mainAddress?.country} onChange={handleChange} />
                                </Grid>
                            </Grid></div>
                        </Paper>
                    </DefaultContentContainer >
                </Grid>
                <Grid size={{ xs: 12, md: 4 }}>
                    <DefaultContentContainer title="Kommunikation" loading={loading} error={error} >
                        <Paper sx={{ p: 2 }}>
                            <Grid container spacing={2}>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Email" name="communication.email" value={data?.communication?.email} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Mobil" name="communication.mobil" value={data?.communication?.mobil} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Telefon" name="communication.phone" value={data?.communication?.phone} onChange={handleChange} />
                                </Grid>
                                <Grid size={{ xs: 12 }}>
                                    <TextField fullWidth label="Website" name="communication.website" value={data?.communication?.website} onChange={handleChange} />
                                </Grid>
                            </Grid>
                        </Paper>
                    </DefaultContentContainer >
                </Grid>
                <Grid size={{ xs: 12 }}>
                    <ButtonGroup disabled={loading}>
                        <Button type="submit" variant="contained" color="success">
                            Speichern
                        </Button>
                    </ButtonGroup>
                </Grid>
            </Grid>
        </form>
    );
};

export default CustomerDetails;


