import { Info, Link, SmartButton } from '@mui/icons-material';
import { Divider, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, styled, Theme, Toolbar, useMediaQuery } from '@mui/material';
import { grey } from '@mui/material/colors';
import React from 'react';

const sidebarWidth = 220;

const SidebarRootBox = styled(Drawer)(() => ({
    maxWidth: sidebarWidth,
    backgroundColor: "transparent",
    color: grey[800],
    flexShrink: 0,
    '& .MuiDrawer-paper': {
        width: sidebarWidth,
        backgroundColor: grey[100],
        color: grey[800]
    }
}));

const Sidebar: React.FC = () => {
    const isMobile = useMediaQuery((theme: Theme) => theme.breakpoints.down('md'));
    const hasContent = false;
    const isOpen = !isMobile && hasContent;

    return (
        <SidebarRootBox open={isOpen} variant='persistent' anchor='right'
            component="aside"
            sx={{ width: isOpen ? sidebarWidth : 0 }}>
            <Toolbar />
            <List dense={true} disablePadding sx={{ mt: 1}}>
                <ListItem disablePadding disableGutters>
                    <ListItemButton>
                        <ListItemIcon>
                            <Info />
                        </ListItemIcon>
                        <ListItemText primary="Informationen" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Erstellt am" secondary="xx.xx.xxxx" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Erstellt durch" secondary="Benutzer" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Zuletz verändert" secondary="xx.xx.xxxx" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Verändert durch" secondary="Benutzer" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Version" secondary="1" />
                    </ListItemButton>
                </ListItem>
            </List>
            <Divider />
            <List dense={true}>
                <ListItem disableGutters>
                    <ListItemButton>
                        <ListItemIcon>
                            <SmartButton />
                        </ListItemIcon>
                        <ListItemText primary="Aktionen" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="Rechnung erstellen" secondary="" />
                    </ListItemButton>
                </ListItem>
            </List>
            <Divider />
            <List dense={true}>
                <ListItem disablePadding disableGutters>
                    <ListItemButton>
                        <ListItemIcon>
                            <Link />
                        </ListItemIcon>
                        <ListItemText primary="Verknüpfungen" />
                    </ListItemButton>
                </ListItem>
                <ListItem disablePadding>
                    <ListItemButton>
                        <ListItemText primary="INV-20240000" secondary="Rechnung" />
                    </ListItemButton>
                </ListItem>
            </List>
        </SidebarRootBox>
    );
};

export default Sidebar;