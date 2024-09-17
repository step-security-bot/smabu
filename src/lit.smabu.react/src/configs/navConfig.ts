import {
    DashboardOutlined as DashboardIcon, GroupsOutlined as GroupsIcon, PointOfSaleOutlined as PointOfSaleIcon,
    GavelOutlined as GavelIcon, PendingActionsOutlined as PendingActionsOutlinedIcon, CurrencyExchangeOutlined as CurrencyExchangeIcon,
    ReceiptLongOutlined as ReceiptLongIcon, ShoppingBagOutlined as ShoppingBagIcon, DesignServicesOutlined as DesignServicesIcon,
    CreditScoreOutlined as CreditScoreIcon, PersonOutline as PersonIcon,
    Navigation,
    SvgIconComponent
} from '@mui/icons-material';

import CustomerList from '../pages/customers/CustomerList';
import CustomerDetails from '../pages/customers/CustomerDetails';
import { Home } from '../pages/home/Home';
import { matchPath } from 'react-router-dom';
import CustomerCreate from '../pages/customers/CustomerCreate';
import CustomerDelete from '../pages/customers/CustomerDelete';
import React from 'react';
import { Profile } from '../pages/profile/Profile';

interface NavigationGroup {
    name: string;
    children: NavigationItem[];
}

export interface NavigationItem {
    name: string;
    icon: SvgIconComponent;
    route: string;
    showInNav?: boolean;
    children?: NavigationItem[];
    element?: React.ReactNode | null;
}

interface Navigation {
    groups: NavigationGroup[];
}

export const navConfig: Navigation = {
    groups: [
        {
            name: "Willkommen",
            children: [
                {
                    name: "Dashboard",
                    icon: DashboardIcon,
                    route: "/",
                    element: React.createElement(Home),
                    showInNav: true,
                }
            ]
        },
        {
            name: "Verwaltung",
            children: [
                {
                    name: "Kunden",
                    icon: GroupsIcon,
                    route: "/customers",
                    showInNav: true,
                    element: React.createElement(CustomerList),
                    children: [
                        {
                            name: "Kunde erstellen",
                            icon: PersonIcon,
                            route: "/customers/create",
                            showInNav: false,
                            element: React.createElement(CustomerCreate),
                        },
                        {
                            name: "Kunden Details",
                            icon: PersonIcon,
                            route: "/customers/:id",
                            showInNav: false,
                            element: React.createElement(CustomerDetails),
                        },
                        {
                            name: "Kunde löschen",
                            icon: PersonIcon,
                            route: "/customers/:id/delete",
                            showInNav: false,
                            element: React.createElement(CustomerDelete),
                        }
                    ]
                },
                {
                    name: "Produkte",
                    icon: DesignServicesIcon,
                    route: "/products",
                    showInNav: true
                },
                {
                    name: "Angebote",
                    icon: PendingActionsOutlinedIcon,
                    route: "/offers",
                    showInNav: true
                },
                {
                    name: "Aufträge",
                    icon: GavelIcon,
                    route: "/orders",
                    showInNav: true
                },
                {
                    name: "Rechnungen",
                    icon: PointOfSaleIcon,
                    route: "/invoices",
                    showInNav: true
                },
                {
                    name: "Zahlungen",
                    icon: CreditScoreIcon,
                    route: "/payments",
                    showInNav: true
                },
                {
                    name: "Ausgaben",
                    icon: ShoppingBagIcon,
                    route: "/expenses",
                    showInNav: true
                },
            ]
        },
        {
            name: "Finanzen",
            children: [
                {
                    name: "EÜR-Berechnung",
                    icon: CurrencyExchangeIcon,
                    route: "/incomesurpluscalculation",
                    showInNav: true
                },
                {
                    name: "Umsatzauswertung",
                    icon: ReceiptLongIcon,
                    route: "/salesanalysis",
                    showInNav: true
                }
            ]
        },
        {
            name: "Account",
            children: [
                {
                    name: "Profil",
                    icon: PersonIcon,
                    route: "/profile",
                    showInNav: false,
                    element: React.createElement(Profile),
                }
            ]
        },
    ],
};

export const getItemByCurrentLocation = (): NavigationItem | undefined => {
    const currentPath = window.location.pathname;
    return getItemByRoute(currentPath);
};

export const getItemByRoute = (path: string): NavigationItem | undefined => {
    const flattenItems = getFlatItems();
    const detectedItems = flattenItems.filter(item => matchPath(item.route, path));
    if (detectedItems.length > 1) {
        var itemWithSamePath = detectedItems.find(item => item.route === path);
        if (itemWithSamePath) {
            return itemWithSamePath;
        } else {
            console.warn(`Multiple items found for path ${path}`, detectedItems.map(item => item.route));
            return detectedItems[0];
        }
    } else {
        return detectedItems[0];
    }
};

export const getFlatItems = (): NavigationItem[] => {
    const rootItems = navConfig.groups.flatMap((group) => group.children);
    const flattenItems = (items: NavigationItem[]): NavigationItem[] => {
        return items.reduce((acc, item) => {
            if (item.children) {
                acc.push(item);
                acc.push(...flattenItems(item.children));
            } else {
                acc.push(item);
            }
            return acc;
        }, [] as NavigationItem[]);
    };
    return flattenItems(rootItems);
}
