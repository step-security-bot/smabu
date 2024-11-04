import { useState, useEffect } from 'react';
import { Grid2 as Grid, Paper, Stack, TextField } from '@mui/material';
import { deepValueChange } from '../../utils/deepValueChange';
import createId from '../../utils/createId';
import { useNavigate, useParams, useSearchParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import DefaultContentContainer from '../../components/contentBlocks/DefaultContentBlock';
import { CreateActions } from '../../components/contentBlocks/PageActionsBlock';
import { AddCatalogItemCommand, CatalogItemId } from '../../types/domain';
import { addCatalogItem } from '../../services/catalogs.service';

const CatalogItemCreate = () => {
    const params = useParams();
    const [searchParams] = useSearchParams();
    const [data, setData] = useState<AddCatalogItemCommand>({
        catalogItemId: createId<CatalogItemId>(),
        catalogId: { value: params.catalogId! },
        catalogGroupId: { value: searchParams.get('catalogGroupId')! },
        name: '',
        description: '',
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
        addCatalogItem(params.catalogId!, data)
            .then((_response) => {
                setLoading(false);
                toast("Erfolgreich erstellt", "success");
                navigate(`/catalogs/${data?.catalogId!.value}/items/${data?.catalogItemId?.value}`);
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
                        <Grid container spacing={2}>
                            <Grid size={{ xs: 12 }}>
                                <TextField fullWidth label="Name" name="name" value={data?.name} onChange={handleChange} required />
                            </Grid>
                            <Grid size={{ xs: 12 }}>
                                <TextField fullWidth label="Beschreibung" name="description" value={data?.description} onChange={handleChange} required />
                            </Grid>
                        </Grid>
                    </Paper>
                </DefaultContentContainer >
                <CreateActions formId="form" disabled={loading} />
            </Stack>
        </form>
    );
};

export default CatalogItemCreate;
