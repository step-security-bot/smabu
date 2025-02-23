import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";
import WelcomeName from "./WelcomeName";
import SignInSignOutButton from "./NavBarButton";
import { Link as RouterLink } from "react-router-dom";
import { IconButton } from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu';

interface NavBarProps {
    handleDrawerOpen: () => void;
}

const NavBar = (props: NavBarProps) => {
    return (
        <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
            <Toolbar>
                <IconButton
                    color="inherit"
                    aria-label="open drawer"
                    edge="start"
                    onClick={props.handleDrawerOpen}
                    sx={{ mr: 1, display: { lg: 'none' } }}
                >
                    <MenuIcon />
                </IconButton>
                <Typography style={{ flexGrow: 1 }}>
                    <Link
                        component={RouterLink}
                        to="/"
                        color="inherit"
                        fontWeight="bold"
                        underline="none"
                        variant="h6">
                        smabu
                    </Link>
                </Typography>
                <WelcomeName />
                <SignInSignOutButton />
            </Toolbar>
        </AppBar >
    );
};

export default NavBar;