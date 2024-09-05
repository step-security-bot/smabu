import MenuItem from '@mui/material/MenuItem';
import { useAuth } from '../../contexts/authContext';


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
        <MenuItem onClick={() => handleLogout()}>Abmelden</MenuItem>
    )
};