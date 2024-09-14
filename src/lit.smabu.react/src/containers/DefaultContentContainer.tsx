import { Alert, AlertTitle, Box, Button, Grid2 as Grid, Skeleton, Toolbar, Typography } from '@mui/material';
import React from 'react';
import { getItemByCurrentLocation } from '../configs/navConfig';
import { blueGrey, grey } from '@mui/material/colors';
import { AxiosError } from 'axios';

interface DefaultContentContainerProps {
    title?: string | null;
    subtitle?: string | null;
    children?: React.ReactNode;
    loading?: boolean;
    error?: AxiosError | string | null;
    toolbarItems?: ToolbarItem[];
}

export interface ToolbarItem {
    text: string;
    icon?: React.ReactNode;
    route?: string,
    action?: () => void;
    showMode?: "onlyText" | "onlyIcon" | "both";
}

const DefaultContentContainer: React.FC<DefaultContentContainerProps> = ({ title, subtitle, children, loading, error, toolbarItems }) => {
    if (title === null || title === undefined) {
        const item = getItemByCurrentLocation();
        title = item?.name;
    }

    return (
        <Box component="section">
            <Toolbar
                variant="dense"
                sx={[
                    {
                        pl: { sm: 0 },
                        pr: { xs: 1, sm: 1 },
                        py: 0
                    },
                ]}>
                <Typography component="h1">
                    <Typography variant="h6" component="span" sx={{ color: (subtitle ? grey[300] : blueGrey[500]), fontWeight: "bold" }}>{title}</Typography>
                    {subtitle && <Typography variant="h6" component="span" sx={{ color: blueGrey[500], ml: 0.5, fontWeight: "bold" }}>{subtitle}</Typography>}
                </Typography>
                <Box sx={{ flex: 1 }}>

                </Box>
                <div>
                    {toolbarItems && toolbarItems.map((item, index) => {
                        item.showMode === undefined ? item.showMode = "both" : item.showMode;
                        return (
                            <Button key={index} size='medium' variant="text" startIcon={item.icon}
                                disabled={loading}
                                onClick={item.action} component="a" href={item.route}
                                title={item.text}>
                                {item.showMode == "onlyText" || item.showMode == "both" && item.text}
                            </Button>
                        );
                    })}
                </div>
            </Toolbar>
            {loading && loadingComponent()}
            {error && errorComponent(error)}
            {!loading && !error && <>{children}</>}
        </Box>
    );
};

const errorComponent = (error: AxiosError | string) => {
    const title = error instanceof AxiosError
        ? `Vorgang nicht möglich (${error.response?.status}-${error.response?.statusText})`
        : `Hoppla`;

    const message = error instanceof AxiosError
        ? `${error.response?.data}`
        : error.toString();

    const severity = error instanceof AxiosError
        ? error.response?.status.toString().startsWith("4") ? "warning" : "warning"
        : "error";

    return <Alert severity={severity}>
        <AlertTitle>{title}</AlertTitle>
        {message}
    </Alert>
}

const loadingComponent = () => <>
    <Grid container spacing={2} size={{ xs: 12 }}>
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 4 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 6, md: 6 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 12, md: 6 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 8 }}>
            <Skeleton variant="rounded" />
        </Grid>
        <Grid size={{ xs: 12, sm: 4 }}>
            <Skeleton variant="rounded" />
        </Grid>
    </Grid>
</>

export default DefaultContentContainer;