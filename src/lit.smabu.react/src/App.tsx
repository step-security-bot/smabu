import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { AuthProvider, msalInstance } from "./contexts/authContext";
import { TitleProvider } from "./contexts/titleContext";
import { getFlatItems } from "./configs/navConfig";
import { Home } from "./pages/home/Home";

function App() {
    return (
        <MsalProvider instance={msalInstance}>
            <AuthProvider>
                <TitleProvider>
                    <PageLayout>
                        <Pages />
                    </PageLayout>
                </TitleProvider>
            </AuthProvider>
        </MsalProvider>
    );
}

function Pages() {
    return (
        <Routes>
            <Route index element={<Home />} />
            { getFlatItems().filter(x => x.element != null).map((item) => <Route path={item.route}  element={item.element!} />)}
        </Routes>
    );
}

export default App;