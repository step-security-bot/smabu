import { useState, useEffect } from 'react';
import { CatalogItemDTO, CatalogItemPrice } from '../../types/domain';
import { useParams } from 'react-router-dom';
import { Grid2 as Grid, IconButton, InputAdornment, Paper, Stack, TextField } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { useNotification } from '../../contexts/notificationContext';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { getCatalogItem, updateCatalogItem } from '../../services/catalogs.service';
import { UnitSelectField } from '../../components/controls/SelectField';
import { Add, RemoveCircle, ToggleOff, ToggleOn } from '@mui/icons-material';
import { formatForTextField } from '../../utils/formatDate';

const CatalogItemDetails = () => {
    const params = useParams();
    const { toast } = useNotification();
    const [data, setData] = useState<CatalogItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);

    const loadData = () => getCatalogItem(params.catalogId!, params.catalogItemId!)
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
        console.log('handleChange', e);
        const { name, value } = e.target;
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateCatalogItem(params.catalogId!, params.catalogItemId!, {
            catalogItemId: data?.id!,
            catalogId: data?.catalogId!,
            name: data?.name!,
            description: data?.description!,
            isActive: data?.isActive!,
            unit: data?.unit!,
            prices: data?.prices!
        })
            .then(() => {
                setLoading(false);
                toast("Erfolgreich gespeichert", "success");
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    const toolbarDetails: ToolbarItem[] = [
        {
            text: data?.isActive ? "Aktiviert" : "Deaktiviert",
            color: data?.isActive ? "success" : "error",
            icon: data?.isActive ? <ToggleOn /> : <ToggleOff />,
            action: () => {
                setData(deepValueChange(data, 'isActive', !data?.isActive))
            }
        }
    ];

    return (
        <form id="form" onSubmit={handleSubmit} >
            <Stack spacing={2}>
                {renderDetails(data, loading, error, toolbarDetails, handleChange)}
                {renderPrices(data, setData)}
                <DetailsActions formId="form" deleteUrl={`/catalogs/${data?.catalogId?.value}/items/${data?.id?.value}/delete`} disabled={loading} />
            </Stack>
        </form>
    );
};

const renderDetails = (data: CatalogItemDTO | undefined, loading: boolean, error: undefined, toolbarDetails: ToolbarItem[], handleChange: (e: any) => void) => {
    return <DefaultContentContainer subtitle={`${data?.displayName}/${data?.name}`} loading={loading} error={error} toolbarItems={toolbarDetails}>
        <Paper sx={{ p: 2 }}>
            <Grid container spacing={2}>
                <Grid size={{ xs: 12, sm: 2 }}>
                    <TextField
                        fullWidth
                        label="#"
                        name="number"
                        value={data?.number?.long || ''}
                        disabled />
                </Grid>
                <Grid size={{ xs: 12, sm: 8 }}>
                    <TextField
                        fullWidth
                        label="Name"
                        name="name"
                        value={data?.name || ''}
                        onChange={handleChange} />
                </Grid>
                <Grid size={{ xs: 6, sm: 2 }}>
                    <UnitSelectField name="unit" label='Einheit' value={data?.unit?.value} required
                        onChange={handleChange} />
                </Grid>
                <Grid size={{ xs: 12, sm: 12 }}>
                    <TextField
                        fullWidth multiline
                        rows={4}
                        label="Description"
                        name="description"
                        value={data?.description || ''}
                        onChange={handleChange} />
                </Grid>
            </Grid>
        </Paper>
    </DefaultContentContainer>;
}

const renderPrices = (data: CatalogItemDTO | undefined, setData: any) => {
    const handleChange = (item: CatalogItemPrice, e: any) => {
        const { value, name } = e.target;
        if (name === 'price') {
            item.price = parseFloat(value);
            setData(deepValueChange(data, 'prices', data!.prices));
        }
        if (name === 'validFrom') {
            item.validFrom = new Date(value);
            setData(deepValueChange(data, 'prices', data!.prices));
        }
    }

    const toolbar: ToolbarItem[] = [
        {
            text: "Neuer Preis",
            icon: <Add />,
            action: () => {
                const newPrice = {
                    price: 0,
                    validFrom: new Date()
                };
                setData(deepValueChange(data, 'prices', [newPrice, ...data!.prices || []]));
            }
        }
    ];

    return <Grid container spacing={2}>
        <Grid size={{ xs: 12, sm: 6 }}>
            <DefaultContentContainer title="Standardpreise" loading={!data} toolbarItems={toolbar}>
                <Paper sx={{ p: 2 }}>
                    <Stack>
                        {data?.prices?.map((price, index) => (
                            <Grid container spacing={2} key={index}>
                                <Grid size={{ xs: "grow" }}>
                                    <TextField
                                        fullWidth
                                        type="number"
                                        label="Preis"
                                        name="price"
                                        slotProps={{
                                            input: {
                                                startAdornment: <InputAdornment position="start">{price.currency?.isoCode}</InputAdornment>,
                                                inputMode: 'decimal'
                                            }
                                        }}
                                        onChange={(e) => handleChange(price, e)}
                                        value={price.price} />
                                </Grid>
                                <Grid size={{ xs: "auto" }}>
                                    <TextField
                                        fullWidth
                                        label="Gültig ab"
                                        name="validFrom"
                                        type="datetime-local"
                                        onChange={(e) => handleChange(price, e)}
                                        value={formatForTextField(price.validFrom, 'datetime')} />
                                </Grid>
                                <Grid size={{ xs: "auto" }}  sx={{ display: 'flex', alignItems: 'self-end' }}>
                                    <IconButton size='small' onClick={() => {
                                        const updatedPrices = data!.prices!.filter((_, i) => i !== index);
                                        setData(deepValueChange(data, 'prices', updatedPrices));
                                    }}>
                                        <RemoveCircle />
                                    </IconButton>
                                </Grid>
                            </Grid>
                        ))}
                    </Stack>
                </Paper>
            </DefaultContentContainer>
        </Grid>
        <Grid size={{ xs: 12, sm: 6 }}>
            <DefaultContentContainer title="Kundenpreise" loading={!data}>
                <Paper sx={{ p: 2 }}>
                    Coming soon
                </Paper>
            </DefaultContentContainer>
        </Grid>
    </Grid>;
}

export default CatalogItemDetails;