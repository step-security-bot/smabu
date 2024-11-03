import { useState, useEffect } from 'react';
import { CatalogDTO } from '../../types/domain';
import { Box, Button, ButtonGroup, Card, CardActions, CardContent, Divider, Paper, Stack, Toolbar, Typography } from '@mui/material';
import DefaultContentContainer, { ToolbarItem } from '../../components/contentBlocks/DefaultContentBlock';
import { deepValueChange } from '../../utils/deepValueChange';
import { DetailsActions } from '../../components/contentBlocks/PageActionsBlock';
import { getDefaultCatalog } from '../../services/catalogs.service';
import { Add, Edit } from '@mui/icons-material';

const CatalogDetails = () => {
    const [data, setData] = useState<CatalogDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);

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
        // setLoading(true);
        // updateInvoice(params.id!, {
        //     id: data?.id!,
        //     performancePeriod: data?.performancePeriod!,
        //     taxRate: data?.taxRate!,
        // })
        //     .then(() => {
        //         setLoading(false);
        //         toast("Rechnung erfolgreich gespeichert", "success");
        //     })
        //     .catch(error => {
        //         setError(error);
        //         setLoading(false);
        //     });
    };

    const toolbarDetails: ToolbarItem[] = [];

    const toolbarGroupsAndItems: ToolbarItem[] = [
        {
            text: "Gruppe",
            icon: <Add />
        },
        {
            text: "Artikel",
            icon: <Add />,
        }
    ];

    return (
        <Stack spacing={2}>
            <DefaultContentContainer subtitle={data?.displayName} loading={loading} error={error} toolbarItems={toolbarDetails} >
                <form id="form" onSubmit={handleSubmit} >
                    <Paper sx={{ p: 2 }}>
                    </Paper>
                </form>
            </DefaultContentContainer >
            <DetailsActions formId="form" deleteUrl={`/catalogs/${data?.id?.value}/delete`} disabled={loading} />

            <DefaultContentContainer title="Gruppen und Artikel" loading={loading} error={error} toolbarItems={toolbarGroupsAndItems}>
                <Stack spacing={1} direction="column">
                    {data && data.groups?.map(group => (
                        <Box>
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
                                    <Button startIcon={<Edit />}>Öffnen</Button>
                                    <Button startIcon={<Add />}>Artikel</Button>
                                </ButtonGroup>
                            </Toolbar>
                            <Stack spacing={{ xs: 1, sm: 2 }}
                                direction="row"
                                useFlexGap
                                sx={{ flexWrap: 'wrap' }}
                            >
                                {group.items?.map(item => (
                                    <Card sx={{ minWidth: 300, maxWidth: 400, flex: '1 1 300px' }}>
                                        <CardContent>
                                            <Typography gutterBottom sx={{ color: 'text.secondary', fontSize: 14 }}>
                                                {item.number?.long}
                                            </Typography>
                                            <Typography variant="h5" component="div">
                                                {item.name}
                                            </Typography>
                                            <Typography sx={{ color: 'text.secondary', mb: 1.5 }}>
                                                {item.currentPrice?.price} {item.currentPrice?.currency?.isoCode}
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


