import { Alert, AlertTitle, Box, Grid2 as Grid, Skeleton, Toolbar, Typography } from '@mui/material';
import React from 'react';
import { getItemByCurrentLocation } from '../configs/navConfig';
import { blueGrey, grey } from '@mui/material/colors';

interface DefaultPageContainerProps {
    title?: string | null;
    subtitle?: string | null;
    children?: React.ReactNode;
    loading?: boolean;
    error?: string | null;
}

const DefaultPageContainer: React.FC<DefaultPageContainerProps> = ({ title, subtitle, children, loading, error }) => {
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
                <Typography component="h1" sx={{ flex: 1 }}>
                    <Typography variant="h6" component="span" sx={{ color: (subtitle ? grey[300] : blueGrey[500]), fontWeight: "bold" }}>{title}</Typography>
                    {subtitle && <Typography variant="h6" component="span" sx={{ color: blueGrey[500], ml: 0.5, fontWeight: "bold" }}>{subtitle}</Typography>}
                </Typography>
                <div>
                </div>
            </Toolbar>

            {loading && loadingComponent()}
            {error && errorComponent(error)}
            {!loading && !error && <Box sx={{ mt: 0 }}>
                {children}
            </Box>}

        </Box>
    );
};

const errorComponent = (error: string) =>
    <Alert severity="error">
        <AlertTitle>Hoppla</AlertTitle>
        {error?.toString()}
    </Alert>

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

export default DefaultPageContainer;