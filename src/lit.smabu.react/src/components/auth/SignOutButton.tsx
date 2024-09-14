import MenuItem from '@mui/material/MenuItem';
import { useAuth } from '../../contexts/authContext';
import { ListItemIcon } from '@mui/material';
import { Logout } from '@mui/icons-material';


interface SignOutButtonProps {
    action: () => void;
}

export const SignOutButton = (props: SignOutButtonProps) => {

    const { logout } = useAuth();

    const handleLogout = () => {
        props.action();
        logout();
    }

    return (
        <MenuItem onClick={() => handleLogout()}>
            <ListItemIcon>
                <Logout fontSize="small" />
            </ListItemIcon>
            Abmelden
        </MenuItem>
    )
};