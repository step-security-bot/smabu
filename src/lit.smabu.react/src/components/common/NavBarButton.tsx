import { useIsAuthenticated, useMsal } from "@azure/msal-react";
import { SignInButton } from "../auth/SignInButton";
import { SignOutButton } from "../auth/SignOutButton";
import { InteractionStatus } from "@azure/msal-browser";
import { useState } from "react";
import IconButton from '@mui/material/IconButton';
import AccountCircle from "@mui/icons-material/AccountCircle";
import Menu from '@mui/material/Menu';
import { Divider, ListItemIcon, MenuItem } from "@mui/material";
import { Link } from "react-router-dom";
import { Person } from "@mui/icons-material";

const SignInSignOutButton = () => {

    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);
    const { inProgress } = useMsal();
    const isAuthenticated = useIsAuthenticated();

    const handleClose = () => {
        setAnchorEl(null);
    };

    if (isAuthenticated) {
        return (<div>
            <IconButton
                onClick={(event) => setAnchorEl(event.currentTarget)}
                color="inherit"
            >
                <AccountCircle />
            </IconButton>
            <Menu
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={open}
                onClose={handleClose}
            >
                <MenuItem component={Link} to="/profile" onClick={handleClose}>
                    <ListItemIcon>
                        <Person fontSize="small" />
                    </ListItemIcon>
                    Profil
                </MenuItem>
                <Divider />
                <SignOutButton action={handleClose} />
            </Menu>
        </div>)
    } else if (inProgress !== InteractionStatus.Startup && inProgress !== InteractionStatus.HandleRedirect) {
        return <SignInButton />;
    } else {
        return null;
    }
}

export default SignInSignOutButton;