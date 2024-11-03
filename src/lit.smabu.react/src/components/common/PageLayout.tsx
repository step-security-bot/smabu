import * as React from 'react';
import Box from '@mui/material/Box';
import NavBar from './NavBar';
import CssBaseline from '@mui/material/CssBaseline';
import Toolbar from '@mui/material/Toolbar';
import NavDrawer from './NavDrawer';
import { useAuth } from '../../contexts/authContext';
import BreadcrumbsComponent from './Breadcrumbs';
import Sidebar from './Sidebar';

type Props = {
    children?: React.ReactNode;
};

export function PageLayout(props: Props) {
    const { children } = props;
    const { isAuthenticated } = useAuth();
    const [drawerOpen, setDrawerOpen] = React.useState(false);

    const handlerDrawerOpen = () => {
        setDrawerOpen(!drawerOpen);
    }

    return (
        <Box sx={{ display: 'flex' }}>
            <CssBaseline />
            <NavBar handleDrawerOpen={handlerDrawerOpen} />
            {isAuthenticated && <NavDrawer drawerOpen={drawerOpen} handleDrawerOpen={handlerDrawerOpen} />}
            <Box component="main" 
                sx={{ flexGrow: 1, p: 3, pt: 0, flex: 1, overflow: 'hidden', maxWidth: 1200 }}>
                <Toolbar />
                <BreadcrumbsComponent /> 
                {children}
            </Box>
            <Sidebar />
        </Box>
    );
}
export default PageLayout;