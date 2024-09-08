import Drawer from '@mui/material/Drawer';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import {
    DashboardOutlined as DashboardIcon, GroupsOutlined as GroupsIcon, PointOfSaleOutlined as PointOfSaleIcon,
    GavelOutlined as GavelIcon, PendingActionsOutlined as PendingActionsOutlinedIcon, CurrencyExchangeOutlined as CurrencyExchangeIcon,
    ReceiptLongOutlined as ReceiptLongIcon, ShoppingBagOutlined as ShoppingBagIcon, DesignServicesOutlined as DesignServicesIcon,
    CreditScoreOutlined as CreditScoreIcon
} from '@mui/icons-material';
import { Link, useLocation } from 'react-router-dom';
import { Box, Divider, ListSubheader, styled, Toolbar } from '@mui/material';
import { grey } from "@mui/material/colors";

const drawerWidth = 240;
const navigationItems = [
    {
        name: null,
        children: [

            {
                name: "Dashboard",
                to: "/",
                icon: <DashboardIcon />,
            }
        ]
    },
    {
        name: null,
        children: [
            {
                name: "Kunden",
                to: "/customers",
                icon: <GroupsIcon />,
            },
            {
                name: "Produkte",
                to: "/products",
                icon: <DesignServicesIcon />,
            },
            {
                name: "Angebote",
                to: "/offers",
                icon: <PendingActionsOutlinedIcon />,
            },
            {
                name: "Aufträge",
                to: "/orders",
                icon: <GavelIcon />,
            },
            {
                name: "Rechnungen",
                to: "/invoices",
                icon: <PointOfSaleIcon />,
            },
            {
                name: "Zahlungen",
                to: "/payments",
                icon: <CreditScoreIcon />,
            },
            {
                name: "Ausgaben",
                to: "/expenses",
                icon: <ShoppingBagIcon />,
            },
        ]
    },
    {
        name: null,
        children: [
            {
                name: "EÜR-Berechnung",
                to: "/incomesurpluscalculation",
                icon: <CurrencyExchangeIcon />,
            },
            {
                name: "Umsatzauswertung",
                to: "/umsatzauswertung",
                icon: <ReceiptLongIcon />,
            }
        ]
    },
];

const CustomDrawer = styled(Drawer)(({ theme }) => ({
    width: drawerWidth,
    backgroundColor: grey[100],
    color: grey[800],
    flexShrink: 0,
    '& .MuiDrawer-paper': {
        width: drawerWidth,
        backgroundColor: grey[100],
        color: grey[800],
    },
}));

const NavDrawer = () => {
    const location = useLocation();

    return <CustomDrawer variant="permanent">
        <Toolbar />
        <Box sx={{ overflow: 'auto' }}>
            {navigationItems.map((group) =>
                <>
                    <List dense={false} sx={{ p: 0 }}>
                        {group.name && <ListSubheader sx={{ fontSize: '0.800rem', fontWeight: '600', color: grey[400] }}>{group.name}</ListSubheader>}
                        {group.children.map((item) =>
                            <ListItem key={item.name} component={Link} to={item.to} disablePadding>
                                <ListItemButton selected={location.pathname === item.to}>
                                    <ListItemIcon sx={{ minWidth: '45px' }}>
                                        {item.icon}
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
}

export default NavDrawer;