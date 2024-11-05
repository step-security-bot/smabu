import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { AuthProvider, msalInstance } from "./contexts/authContext";
import { TitleProvider } from "./contexts/titleContext";
import { getFlatItems } from "./configs/navConfig";
import { NotificationProvider } from "./contexts/notificationContext";
import Welcome from "./pages/welcome/Welcome";

function App() {
    return (
        <MsalProvider instance={msalInstance}>
            <AuthProvider>
                <TitleProvider>
                    <NotificationProvider>
                        <PageLayout>
                            <Pages />
                        </PageLayout>
                    </NotificationProvider>
                </TitleProvider>
            </AuthProvider>
        </MsalProvider>
    );
}

function Pages() {
    return (
        <Routes>
            <Route index element={<Welcome />} />
            {getFlatItems().filter(x => x.element != null).map((item) => <Route path={item.route} element={item.element!} key={item.name + "_" + item.route} />)}
        </Routes>
    );
}

export default App;