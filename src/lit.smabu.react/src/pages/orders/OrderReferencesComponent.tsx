import React, { useEffect, useState } from 'react';
import { GetOrderReferencesReadModel, InvoiceId, InvoiceIdOrderReferenceDTO, OfferId, OfferIdOrderReferenceDTO } from '../../types/domain';
import { Chip, Grid2 as Grid, Stack } from '@mui/material';
import { Cancel, Edit, Save } from '@mui/icons-material';
import { useNotification } from '../../contexts/notificationContext';
import { ToolbarItem } from '../../containers/DefaultContentContainer';
import { getOrdersReferences, updateOrderReferences } from '../../services/order.service';

interface OrderReferencesComponentProps {
    orderId: string;
    setError: (error: any) => void | undefined;
    setToolbar?: (items: ToolbarItem[]) => void | undefined;
    setLoading?: (loading: boolean) => void | undefined;
}

const OrderReferencesComponent: React.FC<OrderReferencesComponentProps> = ({ orderId, setError, setToolbar, setLoading }) => {
    const [data, setData] = useState<GetOrderReferencesReadModel>();
    const [addMode, setAddMode] = useState(false);
    const { toast } = useNotification();

    const toolbar: ToolbarItem[] = []
    if (!addMode) {
        toolbar.push(
            {
                text: "Bearbeiten",
                icon: <Edit />,
                action: () => { setAddMode(true) },
            },
        )
    } else {
        toolbar.push(
            {
                text: "Abbrechen",
                icon: <Cancel />,
                action: () => { setAddMode(false) },
            });
        toolbar.push(
            {
                text: "Speichern",
                icon: <Save />,
                action: () => submit(),
            });
    }

    const submit = () => {
        setLoading && setLoading(true);
        updateOrderReferences(orderId!, {
            orderId: { value: orderId! },
            references: {
                offerIds: data!.offers?.filter(x => x.isSelected).map(x => x.id!),
                invoiceIds: data!.invoices?.filter(x => x.isSelected).map(x => x.id!)
            }
        })
            .then(() => {
                setLoading && setLoading(false);
                setAddMode(false)
                toast("Erledigt", "success");
            })
            .catch(error => {
                setError && setError(error);
                setLoading && setLoading(false);
            });
    };

    const loadData = () => getOrdersReferences(orderId!)
        .then(response => {
            setError && setError(null);
            setData(response.data);
            setLoading && setLoading(false);
        })
        .catch(error => {
            setError && setError(error);
            setLoading && setLoading(false);
        });

    useEffect(() => {
        setLoading && setLoading(true);
        loadData();
        setToolbar && setToolbar(toolbar);
    }, [addMode]);

    const addReference = (id: InvoiceId | OfferId) => {
        const updatedData = { ...data! };
        if (data?.offers?.find(x => x.id?.value === id.value)) {
            updatedData.offers = updatedData.offers?.map(x => {
                if (x.id === id) {
                    x.isSelected = !x.isSelected;
                }
                return x;
            });
        } else {
            updatedData.invoices = updatedData.invoices?.map(x => {
                if (x.id === id) {
                    x.isSelected = !x.isSelected;
                }
                return x;
            });
        }
        setData(updatedData);
        setToolbar && setToolbar(toolbar);
    }

    return (
        <Grid container spacing={2}>
            {data?.offers && (
                <Grid size={{ xs: 12 }}>
                    {data.offers && renderReferenceType(data.offers!, "offer", "Angebote", "/offers", addMode, addReference)}
                </Grid>
            )}
            {data?.invoices && (
                <Grid size={{ xs: 12 }}>
                    {data.invoices && renderReferenceType(data.invoices!, "invoice", "Rechnungen", "/invoices", addMode, addReference)}
                </Grid>
            )}
        </Grid>
    );
};

const renderReferenceType = (
    data: OfferIdOrderReferenceDTO[] | InvoiceIdOrderReferenceDTO[], key: string, label: string, url: string, addMode: boolean,
    addReference: (id: InvoiceId | OfferId) => void) => {
    return (
        <Stack direction="row" spacing={1} flexWrap={"wrap"}
            sx={{
                p: 0,
                justifyContent: "flex-start",
            }}>
            <Chip key={key} clickable label={label}
                variant='outlined' component="a" href={url}
                sx={{ width: "100px", fontWeight: 600 }} />

            {!addMode && data.filter(x => x.isSelected).length === 0 && (
                <Chip key={"NoItems" + key} variant='outlined' label={"Keine Verknüpfungen"} />
            )}
            {data?.filter(x => addMode ? true : x.isSelected).map((reference) => {
                return addMode ? (
                    <Chip clickable key={reference.id?.value}
                        label={reference.name}
                        variant={reference.isSelected ? "filled" : "outlined"}
                        onClick={() => addReference(reference.id!)} />
                ) : (
                    <Chip key={reference.id?.value}
                        clickable
                        component={"a"} 
                        href={url + "/" + reference.id?.value}
                        label={reference.name} />
                );
            })}
        </Stack>
    );;
}

export default OrderReferencesComponent;