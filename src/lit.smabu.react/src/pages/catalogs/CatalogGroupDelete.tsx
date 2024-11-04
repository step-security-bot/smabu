import { useState, useEffect } from 'react';
import { CatalogGroupDTO } from '../../types/domain';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/contentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { DeleteActions } from '../../components/contentBlocks/PageActionsBlock';
import { getCatalogGroup, removeCatalogGroup } from '../../services/catalogs.service';

const CatalogGroupDelete = () => {
    const [data, setData] = useState<CatalogGroupDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getCatalogGroup(params.catalogId!, params.catalogGroupId!)
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
        removeCatalogGroup(params.catalogId!, params.catalogGroupId!)
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
                        Soll die Gruppe "{data?.name}" wirklich gelöscht werden?
                    </Paper>
                </DetailPageContainer >
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default CatalogGroupDelete;
