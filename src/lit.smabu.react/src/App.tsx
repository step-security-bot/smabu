import { Routes, Route } from "react-router-dom";
import { Grid2 } from "@mui/material";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { Home } from "./pages/home/Home";
import { Profile } from "./pages/profile/Profile";
import { AuthProvider, msalInstance } from "./contexts/authContext";

function App() {
    return (
        <MsalProvider instance={msalInstance}>
            <AuthProvider>
                <PageLayout>
                    <Grid2 container justifyContent="center">
                        <Pages />
                    </Grid2>
                </PageLayout>
            </AuthProvider>
        </MsalProvider>
    );
}

function Pages() {
    return (
        <Routes>
            <Route path="/profile" element={<Profile />} />
            <Route path="/" element={<Home />} />
        </Routes>
    );
}

export default App;