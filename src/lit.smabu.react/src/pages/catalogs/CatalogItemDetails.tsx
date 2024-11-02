import { useState, useEffect } from 'react';
import { CatalogItemDTO } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { getCatalogItem, updateCatalogItem } from '../../services/catalogs.service';
import { UnitSelectField } from '../../components/controls/SelectField';
import { ToggleOff, ToggleOn } from '@mui/icons-material';

const CatalogItemDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<CatalogItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);

    const loadData = () => getCatalogItem(params.catalogId!, params.id!)
        .then(response => {
            setData(response.data);
            setLoading(false);
        })
        .catch(error => {
            setError(error);
            setLoading(false);
        });

    useEffect(() => {
        loadData();
    }, []);

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        console.log(name, value);
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateCatalogItem( params.catalogId!, params.id!, {
            id: data?.id!,
            catalogId: data?.catalogId!,
            name: data?.name!,
            description: data?.description!,
            isActive: data?.isActive!,
            unit: data?.unit!,
            prices: data?.prices!
        })
            .then(() => {
                setLoading(false);
                toast("Rechnung erfolgreich gespeichert", "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarDetails: ToolbarItem[] = [
        {
            text: data?.isActive ? "Deaktivieren" : "Aktivieren",
            icon: data?.isActive ? <ToggleOn /> : <ToggleOff />,
            action: () => {
                setData(deepValueChange(data, 'isActive', !data?.isActive))}
        }
    ];

    return (
        <Stack spacing={2}>
            <form id="form" onSubmit={handleSubmit} >
                <DefaultContentContainer subtitle={`${data?.displayName}/${data?.name}`} loading={loading} error={error} toolbarItems={toolbarDetails} >
                    <Paper sx={{ p: 2 }}>
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12, sm: 2}}>
                                <TextField
                                    fullWidth
                                    label="#"
                                    name="number"
                                    value={data?.number?.long || ''}
                                    disabled
                                />
                            </Grid>
                            <Grid size={{ xs: 12, sm: 8}}>
                                <TextField
                                    fullWidth
                                    label="Name"
                                    name="name"
                                    value={data?.name || ''}
                                    onChange={handleChange}
                                />
                            </Grid>
                            <Grid size={{ xs: 6, sm: 2}}>
                                <UnitSelectField name="unit" label='Einheit' value={data?.unit?.value} required
                                    onChange={handleChange}
                                />
                            </Grid>
                            <Grid size={{ xs: 12, sm: 12}}>
                                <TextField
                                    fullWidth multiline
                                    rows={4}
                                    label="Description"
                                    name="description"
                                    value={data?.description || ''}
                                    onChange={handleChange}
                                />
                            </Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer >
            </form>
            <DetailsActions formId="form" deleteUrl={`/catalogs/${data?.id?.value}/items/${data?.id}/delete`} disabled={loading} />
        </Stack>
    );
};

export default CatalogItemDetails;


