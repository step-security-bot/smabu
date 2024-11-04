import { useState, useEffect } from 'react';
import { CatalogDTO } from '../../types/domain';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, Divider, Paper, Stack, TextField, Toolbar, Typography } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { getDefaultCatalog, updateCatalog } from '../../services/catalogs.service';
import { Add, Edit } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';

const CatalogDetails = () => {
    const [data, setData] = useState<CatalogDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);
    const { toast } = useNotification();

    const loadData = () => getDefaultCatalog()
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
        setData(deepValueChange(data, name, value));
    };

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        setLoading(true);
        updateCatalog(data?.id?.value!, {
            catalogId: data?.id!,
            name: data?.name!,
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

    const toolbarDetails: ToolbarItem[] = [];

    const toolbarGroupsAndItems: ToolbarItem[] = [
        {
            text: "Gruppe",
            icon: <Add />,
            route: `/catalogs/${data?.id?.value}/groups/create`
        }
    ];

    return (
        <Stack spacing={2}>
            <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarDetails} >
                <form id="form" onSubmit={handleSubmit} >
                    <Paper sx={{ p: 2 }}>
                        <Stack spacing={2}>
                            <TextField
                                fullWidth
                                id="name"
                                name="name"
                                label="Name"
                                value={data?.name}
                                onChange={handleChange}
                            />
                        </Stack>
                    </Paper>
                </form>
            </DefaultContentContainer >
            <DetailsActions formId="form" deleteUrl={`/catalogs/${data?.id?.value}/delete`} disabled={loading} />

            <DefaultContentContainer title="Gruppen und Artikel" loading={loading} error={error} toolbarItems={toolbarGroupsAndItems}>
                <Stack spacing={1} direction="column">
                    {data && data.groups?.map(group => (
                        <Box key={group.id?.value}>
                            <Toolbar variant='regular'
                                sx={[
                                    {
                                        pl: { sm: 0 },
                                        pr: { xs: 1, sm: 1 }
                                    },
                                ]}> 
                                <Typography variant="h6" component="div">{group.displayName}</Typography>

                                <Box sx={{ flex: 1 }}></Box>
                                <ButtonGroup>
                                    <Button startIcon={<Edit />} href={`/catalogs/${data.id?.value}/groups/${group.id?.value}`}  >Öffnen</Button>
                                    <Button startIcon={<Add />} href={`/catalogs/${data.id?.value}/items/create?catalogGroupId=${group.id?.value}`}>Artikel</Button>
                                </ButtonGroup>
                            </Toolbar>
                            <Stack spacing={{ xs: 1, sm: 2 }}
                                direction="row"
                                useFlexGap
                                sx={{ flexWrap: 'wrap' }}
                            >
                                {group.items?.map(item => (
                                    <Card key={item.id?.value} sx={{ minWidth: 300, maxWidth: 400, flex: '1 1 300px' }}>
                                        <CardContent>
                                            <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                                                {item.number?.long}
                                            </Typography>
                                            <Typography variant="h5" component="div">
                                                {item.name}
                                            </Typography>
                                            <Typography sx={{ color: 'text.secondary', mb: 1.5 }}>
                                                {item.currentPrice?.price?.toFixed(2)} {item.currentPrice?.currency?.isoCode}
                                            </Typography>
                                            <Typography variant="body2">
                                                {item.description}
                                            </Typography>
                                        </CardContent>
                                        <CardActions>
                                            <Button size="small" href={`${data?.id?.value}/items/${item.id?.value}`}>Öffnen</Button>
                                        </CardActions>
                                    </Card>))}

                            </Stack>
                            <Divider sx={{ mt: 3}} />
                        </Box>
                    ))}
                </Stack>
            </DefaultContentContainer>
        </Stack>
    );
};

export default CatalogDetails;