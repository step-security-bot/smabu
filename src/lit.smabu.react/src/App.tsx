import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { AuthProvider, msalInstance } from "./contexts/authContext";
import { navConfig } from "./configs/navConfig";
import { Home } from "./pages/home/Home";
import { Profile } from "./pages/profile/Profile";
import CustomerList from "./pages/customers/customer-list/CustomerList";
import CustomerDetails from "./pages/customers/customer-details/CustomerDetails";
import { TitleProvider } from "./contexts/titleContext";

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
            <Route path="/profile" element={<Profile />} />
            <Route path="/customers" element={<CustomerList />} />
            <Route path="/customers/:id" element={<CustomerDetails />} />
        </Routes>
    );
}

export default App;