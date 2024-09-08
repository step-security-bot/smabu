import { Routes, Route } from "react-router-dom";
import { MsalProvider } from "@azure/msal-react";
import { PageLayout } from "./components/common/PageLayout";
import { Home } from "./pages/home/Home";
import { Profile } from "./pages/profile/Profile";
import { AuthProvider, msalInstance } from "./contexts/authContext";
import CustomerList from "./pages/customers/customer-list/CustomerList";

function App() {
    return (
        <MsalProvider instance={msalInstance}>
            <AuthProvider>
                <PageLayout>
                    <Pages />
                </PageLayout>
            </AuthProvider>
        </MsalProvider>
    );
}

function Pages() {
    return (
        <Routes>
            <Route index element={<Home />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/customers" element={<CustomerList />}>
                <Route path=":id" element={<Home />} />
            </Route>
        </Routes>
    );
}

export default App;