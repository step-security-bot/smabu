import {
    DashboardOutlined as DashboardIcon, GroupsOutlined as GroupsIcon, PointOfSaleOutlined as PointOfSaleIcon,
    GavelOutlined as GavelIcon, PendingActionsOutlined as PendingActionsOutlinedIcon, CurrencyExchangeOutlined as CurrencyExchangeIcon,
    ReceiptLongOutlined as ReceiptLongIcon, ShoppingBagOutlined as ShoppingBagIcon, DesignServicesOutlined as DesignServicesIcon,
    CreditScoreOutlined as CreditScoreIcon, PersonOutline as PersonIcon,
    Navigation,
    SvgIconComponent
} from '@mui/icons-material';

import CustomerList from '../pages/customers/customer-list/CustomerList';
import { Home } from '../pages/home/Home';
import CustomerDetails from '../pages/customers/customer-details/CustomerDetails';
import { matchPath } from 'react-router-dom';


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
    element?: () => JSX.Element;
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
                    showInNav: true,
                    element: Home,
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
                    element: CustomerList,
                    children: [
                        {
                            name: "Kunden Details",
                            icon: PersonIcon,
                            route: "/customers/:id",
                            showInNav: false,
                            element: CustomerDetails,
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
                    icon: CurrencyExchangeIcon,
                    route: "/profile",
                    showInNav: false
                }
            ]
        },
    ],
};

export const getItemByRoute = (path: string): NavigationItem | undefined => {
    const flattenItems = getFlatItems();
    return flattenItems.find(item => matchPath(item.route, path));
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
