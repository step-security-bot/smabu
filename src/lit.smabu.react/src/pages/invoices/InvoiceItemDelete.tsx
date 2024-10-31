import { useState, useEffect } from 'react';
import { InvoiceDTO, InvoiceItemDTO } from '../../types/domain';
import { Paper, Stack } from '@mui/material';
import DetailPageContainer from '../../components/contentBlocks/DefaultContentBlock';
import { useNavigate, useParams } from 'react-router-dom';
import { useNotification } from '../../contexts/notificationContext';
import { deleteInvoiceItem, getInvoice } from '../../services/invoice.service';
import { DeleteActions } from '../../components/contentBlocks/PageActionsBlock';

const InvoiceDelete = () => {
    const [invoice, setInvoice] = useState<InvoiceDTO>();
    const [data, setData] = useState<InvoiceItemDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const params = useParams();
    const { toast } = useNotification();

    useEffect(() => {
        getInvoice(params.invoiceId!, true)
            .then(response => {
                setInvoice(response.data);
                setData(response.data.items?.find((item: InvoiceItemDTO) => item.id!.value === params.id));
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    }, []);

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        deleteInvoiceItem(params.invoiceId!, params.id!)
            .then((_response) => {
                setLoading(false);
                toast("Position erfolgreich gelöscht", "success");
                navigate(`/invoices/${params.invoiceId}`);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
            });
    };

    return (
        <form id="form" onSubmit={handleSubmit}>
            <Stack spacing={2}>
                <DetailPageContainer title={invoice?.displayName} subtitle={`Pos: ${data?.displayName}`} loading={loading} error={error} >
                    <Paper sx={{ p: 2 }}>
                        Soll die Rechnungsposition "{data?.position}" wirklich gelöscht werden?
                        <br />
                        Details: {data?.details}
                    </Paper>
                </DetailPageContainer>
                <DeleteActions formId="form" />
            </Stack>
        </form>
    );
};

export default InvoiceDelete;
