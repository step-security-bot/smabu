import {
    DashboardOutlined as DashboardIcon, GroupsOutlined as GroupsIcon, PointOfSaleOutlined as PointOfSaleIcon,
    GavelOutlined as GavelIcon, PendingActionsOutlined as PendingActionsOutlinedIcon, CurrencyExchangeOutlined as CurrencyExchangeIcon,
    ReceiptLongOutlined as ReceiptLongIcon, ShoppingBagOutlined as ShoppingBagIcon, DesignServicesOutlined as DesignServicesIcon,
    CreditScoreOutlined as CreditScoreIcon, PersonOutline as PersonIcon,
    Navigation,
    SvgIconComponent,
    FormatListNumbered
} from '@mui/icons-material';

import React from 'react';
import CustomerList from '../pages/customers/CustomerList';
import CustomerDetails from '../pages/customers/CustomerDetails';
import { Welcome } from '../pages/welcome/Welcome';
import { matchPath } from 'react-router-dom';
import CustomerCreate from '../pages/customers/CustomerCreate';
import CustomerDelete from '../pages/customers/CustomerDelete';
import { Profile } from '../pages/profile/Profile';
import InvoiceList from '../pages/invoices/InvoiceList';
import InvoiceCreate from '../pages/invoices/InvoiceCreate';
import InvoiceDetails from '../pages/invoices/InvoiceDetails';
import InvoiceDelete from '../pages/invoices/InvoiceDelete';
import InvoiceItemDetails from '../pages/invoices/InvoiceItemDetails';
import InvoiceItemCreate from '../pages/invoices/InvoiceItemCreate';
import InvoiceItemDelete from '../pages/invoices/InvoiceItemDelete';
import OfferCreate from '../pages/offers/OfferCreate';
import OfferDelete from '../pages/offers/OfferDelete';
import OfferDetails from '../pages/offers/OfferDetails';
import OfferItemCreate from '../pages/offers/OfferItemCreate';
import OfferItemDelete from '../pages/offers/OfferItemDelete';
import OfferItemDetails from '../pages/offers/OfferItemDetails';
import OfferList from '../pages/offers/OfferList';
import { SalesDashboard } from '../pages/salesDashboard/SalesDashboard';
import OrderList from '../pages/orders/OrderList';
import OrderCreate from '../pages/orders/OrderCreate';
import OrderDetails from '../pages/orders/OrderDetails';
import OrderDelete from '../pages/orders/OrderDelete';
import CatalogDetails from '../pages/catalogs/CatalogDetails';
import CatalogItemDetails from '../pages/catalogs/CatalogItemDetails';

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

export interface Navigation {
    groups: NavigationGroup[];
}

export const navConfig: Navigation = {
    groups: [
        {
            name: "Dashboards",
            children: [
                {
                    name: "Willkommen",
                    icon: DashboardIcon,
                    route: "/",
                    element: React.createElement(Welcome),
                    showInNav: true,
                },
                {
                    name: "Umsatzübersicht",
                    icon: ReceiptLongIcon,
                    route: "/salesdashboard",
                    element: React.createElement(SalesDashboard),
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
                    name: "Katalog",
                    icon: DesignServicesIcon,
                    route: "/catalogs/default",
                    showInNav: true,
                    element: React.createElement(CatalogDetails),
                    children: [
                        {
                            name: "Katalog-Gruppe",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId/groups/:id",
                            showInNav: false,
                            element: React.createElement(CatalogDetails),
                        },
                        {
                            name: "Katalog-Gruppe löschen",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId/groups/:id/delete",
                            showInNav: false,
                            element: React.createElement(CatalogDetails),
                        },
                        {
                            name: "Katalog-Gruppe erstellen",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId/groups/create",
                            showInNav: false,
                            element: React.createElement(CatalogDetails),
                        },
                        {
                            name: "Katalog-Artikel",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId/items/:id",
                            showInNav: false,
                            element: React.createElement(CatalogItemDetails),
                        },
                        {
                            name: "Katalog-Artikel erstellen",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId//items/create",
                            showInNav: false,
                            element: React.createElement(CatalogDetails),
                        },
                        {
                            name: "Katalog-Artikel löschen",
                            icon: DesignServicesIcon,
                            route: "/catalogs/:catalogId/groups/:groupId/items/:id/delete",
                            showInNav: false,
                            element: React.createElement(CatalogDetails),
                        },
                    ]
                },
                {
                    name: "Angebote",
                    icon: PendingActionsOutlinedIcon,
                    route: "/offers",
                    showInNav: true,
                    element: React.createElement(OfferList),
                    children: [
                        {
                            name: "Angebot erstellen",
                            icon: PendingActionsOutlinedIcon,
                            route: "/offers/create",
                            showInNav: false,
                            element: React.createElement(OfferCreate),
                        },
                        {
                            name: "Angebot",
                            icon: PendingActionsOutlinedIcon,
                            route: "/offers/:id",
                            showInNav: false,
                            element: React.createElement(OfferDetails),
                        },
                        {
                            name: "Angebot löschen",
                            icon: PendingActionsOutlinedIcon,
                            route: "/offers/:id/delete",
                            showInNav: false,
                            element: React.createElement(OfferDelete),
                        },
                        {
                            name: "Angebotsposition erstellen",
                            icon: FormatListNumbered,
                            route: "/offers/:offerId/items/create",
                            showInNav: false,
                            element: React.createElement(OfferItemCreate),
                        },
                        {
                            name: "Angebotsposition",
                            icon: FormatListNumbered,
                            route: "/offers/:offerId/items/:id/",
                            showInNav: false,
                            element: React.createElement(OfferItemDetails),
                        },
                        {
                            name: "Angebotsposition löschen",
                            icon: FormatListNumbered,
                            route: "/offers/:offerId/items/:id/delete",
                            showInNav: false,
                            element: React.createElement(OfferItemDelete),
                        }
                    ]
                },
                {
                    name: "Aufträge",
                    icon: GavelIcon,
                    route: "/orders",
                    showInNav: true,
                    element: React.createElement(OrderList),
                    children: [
                        {
                            name: "Auftrag erstellen",
                            icon: PersonIcon,
                            route: "/orders/create",
                            showInNav: false,
                            element: React.createElement(OrderCreate),
                        },
                        {
                            name: "Auftrag Details",
                            icon: PersonIcon,
                            route: "/orders/:id",
                            showInNav: false,
                            element: React.createElement(OrderDetails),
                        },
                        {
                            name: "Auftrag löschen",
                            icon: PersonIcon,
                            route: "/orders/:id/delete",
                            showInNav: false,
                            element: React.createElement(OrderDelete),
                        }
                    ]
                },
                {
                    name: "Rechnungen",
                    icon: PointOfSaleIcon,
                    route: "/invoices",
                    showInNav: true,
                    element: React.createElement(InvoiceList),
                    children: [
                        {
                            name: "Rechnung erstellen",
                            icon: PointOfSaleIcon,
                            route: "/invoices/create",
                            showInNav: false,
                            element: React.createElement(InvoiceCreate),
                        },
                        {
                            name: "Rechnung",
                            icon: PointOfSaleIcon,
                            route: "/invoices/:id",
                            showInNav: false,
                            element: React.createElement(InvoiceDetails),
                        },
                        {
                            name: "Rechnung löschen",
                            icon: PointOfSaleIcon,
                            route: "/invoices/:id/delete",
                            showInNav: false,
                            element: React.createElement(InvoiceDelete),
                        },
                        {
                            name: "Position erstellen",
                            icon: FormatListNumbered,
                            route: "/invoices/:invoiceId/items/create",
                            showInNav: false,
                            element: React.createElement(InvoiceItemCreate),
                        },
                        {
                            name: "Position",
                            icon: FormatListNumbered,
                            route: "/invoices/:invoiceId/items/:id/",
                            showInNav: false,
                            element: React.createElement(InvoiceItemDetails),
                        },
                        {
                            name: "Position löschen",
                            icon: FormatListNumbered,
                            route: "/invoices/:invoiceId/items/:id/delete",
                            showInNav: false,
                            element: React.createElement(InvoiceItemDelete),
                        }
                    ]
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
