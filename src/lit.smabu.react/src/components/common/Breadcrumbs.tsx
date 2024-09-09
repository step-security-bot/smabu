import { Link, useLocation } from 'react-router-dom';
import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import { Divider, SvgIcon } from '@mui/material';
import { Home as HomeIcon } from '@mui/icons-material';
import { getItemByRoute } from '../../configs/navConfig';


const BreadcrumbsComponent: React.FC = () => {
    const location = useLocation();
    const pathnames = location.pathname.split('/').filter((x) => x);
    if (pathnames.length > 0) {
        return (
            <>
                <Breadcrumbs aria-label="breadcrumb"
                    sx={{ py: 1.5, fontSize: "0.85rem" }}
                    separator="/" >
                    <Link to="/">
                        <HomeIcon sx={{ fontSize: "1rem", mb: -0.2 }} />
                    </Link>
                    {pathnames.map((value, index) => {
                        const isLast = index === pathnames.length - 1;
                        const to = `/${pathnames.slice(0, index + 1).join('/')}`;
                        var navItem = getItemByRoute(to);
                        return isLast ? (
                            <Typography key={to} color="text.primary" sx={{ fontSize: "0.85rem" }}>
                                <SvgIcon component={navItem!.icon} sx={{ mr: 0.5, fontSize: "1rem", mb: -0.2 }} />
                                {navItem ? navItem.name : value.toUpperCase()}
                            </Typography>
                        ) : (
                            <Link key={to} to={to}>
                                <SvgIcon component={navItem!.icon} sx={{ mr: 0.5, fontSize: "1rem", mb: -0.2 }} />
                                {navItem ? navItem.name : value.toUpperCase()}
                            </Link>
                        );
                    })}
                </Breadcrumbs>
                <Divider />
            </>
        );
    } else {
        return (
            <><Breadcrumbs sx={{ py: 1  }}></Breadcrumbs></>
        );
    }
};

export default BreadcrumbsComponent;