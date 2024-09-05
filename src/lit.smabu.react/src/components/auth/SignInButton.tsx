import Button from "@mui/material/Button";
import { useAuth } from "../../contexts/authContext";

export const SignInButton = () => {
    const { login } = useAuth();

    const handleLogin = () => {
        login();
    }

    return (
        <div>
            <Button
                onClick={() => handleLogin()} key="loginPopup"
                color="inherit">
                Anmelden
            </Button>
        </div>
    )
};