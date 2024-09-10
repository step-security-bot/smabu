import { useState, useEffect } from 'react';
import axiosConfig from "../../../configs/axiosConfig";
import { CustomerDTO, UpdateCustomerCommand } from '../../../types/domain';
import { useParams } from 'react-router-dom';
import { Button, ButtonGroup, Grid2 as Grid, Paper, Stack, TextField, Typography } from '@mui/material';
import DetailPageContainer from '../../../containers/DefaultPageContainer';
import { grey } from '@mui/material/colors';
import { deepValueChange } from '../../../utils/deepValueChange';

const CustomerDetails = () => {
    const params = useParams()
    const [data, setData] = useState<CustomerDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

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

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        console.log(99, "handleSubmit", data);
        axiosConfig.put<UpdateCustomerCommand>('customers/' + params.id, {
            id: data?.id,
            name: data?.name,
            shortName: data?.shortName,
            industryBranch: data?.industryBranch,
            mainAddress: data?.mainAddress,
            communication: data?.communication
        })
            .then(response => {
                setLoading(false);
                alert("Kunde erfolgreich gespeichert");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <DetailPageContainer subtitle={data?.name} loading={loading} error={error} >
            <form id="form" onSubmit={handleSubmit}>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12 }}>
                        <Paper sx={{ p: 2 }}>
                            <Stack spacing={{ xs: 1, sm: 2 }} direction="row" useFlexGap sx={{ flexWrap: 'wrap' }}>
                                <TextField label="#" name="number" value={data?.displayName} disabled />
                                <TextField label="Name" name="name" value={data?.name} onChange={handleChange} required />
                                <TextField label="Kurzname" name="shortName" value={data?.shortName} onChange={handleChange} required disabled
                                    slotProps={{ htmlInput: { minLength: 5, maxLength: 5 } }} />
                                <TextField label="Branche" name="industryBranch" value={data?.industryBranch} onChange={handleChange} required />
                                <TextField label="Währung" name="currency" value={data?.currency?.name} disabled />
                            </Stack>
                        </Paper>
                    </Grid>
                    <Grid size={{ xs: 12, md: 8 }}>
                        <Typography variant="h6" component="h2" sx={{ color: grey[300], fontWeight: "bold" }}>Adresse</Typography>
                        <Paper sx={{ p: 2 }}>
                            <Stack spacing={{ xs: 1, sm: 2 }} direction="row" useFlexGap sx={{ flexWrap: 'wrap' }}>
                                <TextField label="Name 1" name="mainAddress.name1" value={data?.mainAddress?.name1} onChange={handleChange} />
                                <TextField label="Name 2" name="mainAddress.name2" value={data?.mainAddress?.name2} onChange={handleChange} />
                                <TextField label="Street" name="mainAddress.street" value={data?.mainAddress?.street} onChange={handleChange} />
                                <TextField label="House Number" name="mainAddress.houseNumber" value={data?.mainAddress?.houseNumber} onChange={handleChange} />
                                <TextField label="Postal Code" name="mainAddress.postalCode" value={data?.mainAddress?.postalCode} onChange={handleChange} />
                                <TextField label="City" name="mainAddress.city" value={data?.mainAddress?.city} onChange={handleChange} />
                                <TextField label="Country" name="mainAddress.country" value={data?.mainAddress?.country} onChange={handleChange} />
                            </Stack>
                        </Paper>
                    </Grid>
                    <Grid size={{ xs: 12, md: 4 }}>
                        <Typography variant="h6" component="h2" sx={{ color: grey[300], fontWeight: "bold" }}>Kommunikation</Typography>
                        <Paper sx={{ p: 2 }}>
                            <Stack spacing={{ xs: 1, sm: 2 }} direction="column">
                                <TextField label="Email" name="communication.email" value={data?.communication?.email} onChange={handleChange} />
                                <TextField label="Mobile" name="communication.mobil" value={data?.communication?.mobil} onChange={handleChange} />
                                <TextField label="Phone" name="communication.phone" value={data?.communication?.phone} onChange={handleChange} />
                                <TextField label="Website" name="communication.website" value={data?.communication?.website} onChange={handleChange} />
                            </Stack>
                        </Paper>
                    </Grid>
                </Grid>
            </form>

            <ButtonGroup sx={{ mt: 2 }}>
                <Button type="submit" form="form" variant="contained" color="success">
                    Speichern
                </Button>
            </ButtonGroup>
        </DetailPageContainer >
    );
};

export default CustomerDetails;


