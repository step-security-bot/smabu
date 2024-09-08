

import * as React from 'react';
import Box from '@mui/material/Box';
import NavBar from './NavBar';
import CssBaseline from '@mui/material/CssBaseline';
import Toolbar from '@mui/material/Toolbar';
import NavDrawer from './NavDrawer';
import { useAuth } from '../../contexts/authContext';

type Props = {
    children?: React.ReactNode;
};

export function PageLayout(props: Props) {
    const { children } = props;
    const { isAuthenticated } = useAuth();

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <NavBar />
            { isAuthenticated && <NavDrawer />}
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <Toolbar />
                {children}
            </Box>
        </Box>
    );
}
export default PageLayout;