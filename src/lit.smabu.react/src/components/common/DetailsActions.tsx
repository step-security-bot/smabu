import { ArrowBack, Delete as DeleteIcon, Refresh as RefreshIcon, Save as SaveIcon } from '@mui/icons-material';
import { Button, ButtonGroup, Grid2 as Grid, IconButton } from '@mui/material';
import React from 'react';

interface DetailsActionsProps {
    formId: string | undefined;
    deleteUrl?: string;
    disabled?: boolean;
}

const DetailsActions: React.FC<DetailsActionsProps> = ({ formId, deleteUrl, disabled }) => {
    return (
        <Grid container>
            <Grid size="auto">
                <ButtonGroup>
                    <IconButton color="default" onClick={() => window.history.back()}
                        title='Zurück'>
                        <ArrowBack />
                    </IconButton>
                    {deleteUrl && <IconButton color="error" href={deleteUrl}
                        title='Löschen'>
                        <DeleteIcon />
                    </IconButton>}
                </ButtonGroup>
            </Grid>
            <Grid size="grow">

            </Grid>
            <Grid size="auto">
                <ButtonGroup variant="contained" >
                    <IconButton color="info"
                        href={location.href}
                        title='Zurücksetzen'
                    >
                        <RefreshIcon />
                    </IconButton>
                    {formId && <Button type="submit" form={formId} color="info"
                        title='Speichern'
                        disabled={disabled}
                        endIcon={<SaveIcon />}
                    >
                        Speichern
                    </Button>}
                </ButtonGroup>
            </Grid>
        </Grid>
    );
};

export default DetailsActions;