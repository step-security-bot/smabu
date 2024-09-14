import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';

import { Link, useLocation } from 'react-router-dom';
import { Box, Divider, ListSubheader, styled, SvgIcon, Theme, Toolbar, useMediaQuery } from '@mui/material';
import { grey } from "@mui/material/colors";
import { navConfig } from '../../configs/navConfig';

const drawerWidth = 240;

interface NavDrawerProps {
    drawerOpen: boolean;
}

const CustomDrawer = styled(Drawer)(() => ({
    width: drawerWidth,
    backgroundColor: "transparent",
    color: grey[800],
    flexShrink: 0,
    '& .MuiDrawer-paper': {
        width: drawerWidth,
        backgroundColor: grey[100],
        color: grey[800]
    }
}));

const NavDrawer = (props: NavDrawerProps) => {
    const location = useLocation();
    const isMobile = useMediaQuery((theme: Theme) => theme.breakpoints.down('md'));
    return <CustomDrawer variant={isMobile ? "temporary" : "permanent"} open={props.drawerOpen}>
        <Toolbar />
        <Box sx={{ overflow: 'auto' }}>
            {navConfig.groups.filter(x => x.children.filter(y => y.showInNav).length > 0).map((group) =>
                <>
                    <List dense={false} sx={{ p: 0 }}>
                        {false && <ListSubheader key={group.name} sx={{ fontSize: '0.800rem', fontWeight: '600', color: grey[400] }}>{group.name}</ListSubheader>}
                        {group.children.filter(x => x.showInNav === true).map((item) =>
                            <ListItem key={item.name} component={Link} to={item.route} disablePadding>
                                <ListItemButton selected={CheckSelected(item.route)}>
                                    <ListItemIcon sx={{ minWidth: '45px' }}>
                                        <SvgIcon component={item.icon} inheritViewBox />
                                    </ListItemIcon>
                                    <ListItemText
                                        primary={item.name}
                                        primaryTypographyProps={{ fontSize: '0.900rem' }}
                                        sx={{ color: grey[600] }} />
                                </ListItemButton>
                            </ListItem>
                        )}
                        <Divider />
                    </List>
                </>

            )}
        </Box>
    </CustomDrawer>;

    function CheckSelected(path: string): boolean | undefined {
        if (path === "/") {
            return location.pathname === path;
        } else {
            return location.pathname.startsWith(path);
        }
    }
}

export default NavDrawer;