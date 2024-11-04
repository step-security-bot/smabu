import { useState, useEffect } from 'react';
import { CatalogItemDTO } from '../../types/domain';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/contentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { DeleteActions } from '../../components/contentBlocks/PageActionsBlock';
import { getCatalogItem, removeCatalogItem } from '../../services/catalogs.service';

const CatalogItemDelete = () => {
    const [data, setData] = useState<CatalogItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getCatalogItem(params.catalogId!, params.catalogItemId!)
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        removeCatalogItem(params.catalogId!, params.catalogItemId!)
            .then((_response) => {
                setLoading(false);
                toast("Erfolgreich gelöscht", "success");
                navigate(`/catalogs/${params.catalogId!}`);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DetailPageContainer subtitle={data?.name} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        Soll der Artikel "{data?.name}" wirklich gelöscht werden?
                    </Paper>
                </DetailPageContainer >
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default CatalogItemDelete;
