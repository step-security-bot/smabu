import { useEffect, useState } from "react";
import { InvoiceDTO } from "../../types/domain";
import DefaultContentContainer, { ToolbarItem } from "../../components/contentBlocks/DefaultContentBlock";
import { Add, Delete, Edit } from "@mui/icons-material";
import { DataGrid, GridActionsCellItem, GridColDef } from '@mui/x-data-grid';
import { formatDate } from "../../utils/formatDate";
import { getInvoices } from "../../services/invoice.service";
import { Link } from "react-router-dom";
import { Paper } from "@mui/material";
import { handleAsyncTask } from "../../utils/executeTask";

const columns: GridColDef[] = [
    { field: 'number', headerName: '#', width: 120, valueGetter: (value: any) => value.displayName },
    { field: 'fiscalYear', headerName: 'Jahr', width: 70 },
    { field: 'createdAt', headerName: 'Erstellt am', width: 100, valueFormatter: (value) => formatDate(value) },
    { field: 'customer', headerName: 'Kunde', flex: 1, valueGetter: (value: any) => value.name },
    { field: 'amount', headerName: 'Summe', width: 110, align: 'right', valueFormatter: (value: any, row) => `${value.toFixed(2)} ${row.currency?.isoCode}` },
    { field: 'releasedAt', headerName: 'Freigegeben am', width: 100, valueFormatter: (value) => formatDate(value) },
    {
        field: 'actions',
        type: 'actions',
        headerName: '',
        width: 100,
        getActions: ({ id }) => {
            return [
                <GridActionsCellItem
                    icon={<Edit />}
                    label="Öffnen"
                    className="textPrimary"
                    component={Link}
                    to={`/invoices/${id}`}
                    color="primary"
                />,
                <GridActionsCellItem
                    icon={<Delete />}
                    label="Delete"
                    component={Link}
                    to={`/invoices/${id}/delete`}
                    color="warning"
                />,
            ];
        },
    }
];

const paginationModel = { page: 0, pageSize: 10 };

const InvoiceList = () => {
    const [data, setData] = useState<InvoiceDTO[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const toolbarItems: ToolbarItem[] = [
        {
            text: "Neu",
            route: "/invoices/create",
            icon: <Add />
        }
    ];

    useEffect(() => {
        handleAsyncTask({
            task: getInvoices,
            onLoading: setLoading,
            onSuccess: setData,
            onError: setError
        });
    }, []);

    return (
        <DefaultContentContainer loading={loading} error={error} toolbarItems={toolbarItems}>
            <Paper sx={{ flex: 1, overflow: 'hidden' }}>
                <DataGrid
                    rows={data}
                    columns={columns}
                    getRowId={(row) => row.id.value}
                    isRowSelectable={() => false}
                    initialState={{ pagination: { paginationModel } }}
                    pageSizeOptions={[10, 50, 100]}
                    sx={{ border: 0 }}
                />
            </Paper>
        </DefaultContentContainer>
    );
}

export default InvoiceList;