import { Link, useLocation } from 'react-router-dom';
import Breadcrumbs from '@mui/material/Breadcrumbs';
import Typography from '@mui/material/Typography';
import { Button, Divider, SvgIcon } from '@mui/material';
import { Home as HomeIcon } from '@mui/icons-material';
import { getItemByRoute } from '../../configs/navConfig';


const BreadcrumbsComponent: React.FC = () => {
    const location = useLocation();
    const pathnames = location.pathname.split('/').filter((x) => x);
    if (pathnames.length > 0) {
        let backTo: string | null = null;
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
                        const navItem = getItemByRoute(to);
                        if (navItem) {
                            if (isLast) {
                                return <>
                                    <Typography key={to} color="text.primary" component='span' sx={{ fontSize: "0.85rem" }}>
                                        <SvgIcon component={navItem!.icon} sx={{ mr: 0.5, fontSize: "1rem", mb: -0.3 }} />
                                        {navItem ? navItem.name : value.toUpperCase()}
                                    </Typography>
                                    {backTo && <Button key="back" size='small' component="a" href={backTo} variant='outlined'
                                        color='info' 
                                        sx={{ ml: 1, my: -1, fontSize: "0.85rem" }}
                                        title="Zurück">
                                            Zurück
                                    </Button>}
                                </>

                            } else {
                                backTo = to;
                                return <Link key={to} to={to}>
                                    {navItem ? navItem.name : value.toUpperCase()}
                                </Link>
                            }
                        }
                    })}
                </Breadcrumbs>
                <Divider />
            </>


        );
    } else {
        return (
            <><Breadcrumbs sx={{ py: 1 }}></Breadcrumbs></>
        );
    }
};

export default BreadcrumbsComponent;