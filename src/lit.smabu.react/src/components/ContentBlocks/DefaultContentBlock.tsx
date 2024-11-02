import { Alert, AlertTitle, Box, Button, ButtonGroup, Collapse, IconButton, LinearProgress, Toolbar, Typography } from '@mui/material';
import React, { useState } from 'react';
import { getItemByCurrentLocation } from '../../configs/navConfig';
import { blueGrey, grey } from '@mui/material/colors';
import { AxiosError } from 'axios';
import { ModelError } from '../../types/domain';
import { Close } from '@mui/icons-material';

interface DefaultContentContainerProps {
    title?: string | null;
    subtitle?: string | null;
    children?: React.ReactNode;
    loading?: boolean;
    error?: AxiosError | string | undefined | null;
    toolbarItems?: ToolbarItem[] | undefined;
}

export interface ToolbarItem {
    text: string;
    icon?: React.ReactNode;
    route?: string,
    action?: () => void;
    showMode?: "onlyText" | "onlyIcon" | "both";
    color?: 'inherit' | 'primary' | 'secondary' | 'success' | 'error' | 'info' | 'warning';
    title?: string;
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
                    {title && title !== undefined && <Typography variant="h6" component="span" sx={{ color: (subtitle ? grey[300] : blueGrey[500]), fontWeight: "bold" }}>{title}</Typography>}
                    {subtitle && subtitle !== undefined && <Typography variant="h6" component="span" sx={{ color: blueGrey[500], ml: 0.5, fontWeight: "bold" }}>{subtitle}</Typography>}
                </Typography>
                <Box sx={{ flex: 1 }}>

                </Box>
                <ButtonGroup>
                    {toolbarItems && toolbarItems.map((item, index) => {
                        item.showMode === undefined ? item.showMode = "both" : item.showMode;
                        return (
                            <Button key={index} size='small' variant="text" startIcon={item.icon}
                                disabled={loading}
                                color={item.color}
                                onClick={ item.action ? () => item.action!() : undefined } 
                                component={item.route ? "a" : "button"}
                                href={item.route ? item.route : undefined}
                                title={item.title ?? item.text}>
                                {item.showMode == "onlyText" || item.showMode == "both" && item.text}
                            </Button>
                        );
                    })}
                </ButtonGroup>
            </Toolbar>
            {error && errorComponent(error)}
            {!loading && children}
            {loading && !error && <Box sx={{ opacity: 0.2 }}>
                {children}
                <LinearProgress sx={{ mt: -0.5 }} />
            </Box>}
        </Box>
    );
};

const errorComponent = (error: AxiosError | string) => {
    const [isOpen, setIsOpen] = useState<boolean>(false);
    let title = "Hoppla";
    let message = error?.toString();
    if (error instanceof AxiosError) {

        const modelError = error.response?.data as ModelError;
        if (modelError?.code) {
            title = `Vorgang nicht möglich`
            message = `${modelError.description}`
        } else {
            title = `${error.response?.status}-${error.response?.statusText}`
            message = `${error.response?.data}`
        }
    }

    const severity = error instanceof AxiosError
        ? error.response?.status.toString().startsWith("4") ? "warning" : "warning"
        : "error";

    React.useEffect(() => {
        setIsOpen(true);
    }, [error]);

    return <Collapse in={isOpen}><Alert severity={severity} variant='standard'
        action={
            <IconButton
                aria-label="close"
                color="inherit"
                size="small"
                onClick={() => setIsOpen(false)}
            >
                <Close fontSize="inherit" />
            </IconButton>
        }>
        <AlertTitle>{title}</AlertTitle>
        {message}
    </Alert></Collapse>
}

export default DefaultContentContainer;