import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { AuthProvider, msalInstance } from "./contexts/authContext";
import { Home } from "./pages/home/Home";
import { Profile } from "./pages/profile/Profile";
import CustomerList from "./pages/customers/CustomerList";
import CustomerDetails from "./pages/customers/CustomerDetails";
import { TitleProvider } from "./contexts/titleContext";
import CustomerCreate from "./pages/customers/CustomerCreate";
import CustomerDelete from "./pages/customers/CustomerDelete";

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
            <Route path="/customers/create" element={<CustomerCreate />} />
            <Route path="/customers/:id" element={<CustomerDetails />} />
            <Route path="/customers/:id/delete" element={<CustomerDelete />} />
        </Routes>
    );
}

export default App;