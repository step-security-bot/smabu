import React, { createContext, useContext, useEffect, useState, ReactNode } from 'react';
import { PublicClientApplication, AccountInfo, EventMessage, EventType, AuthenticationResult } from '@azure/msal-browser';
import { useMsal } from '@azure/msal-react';
import { msalConfig, loginRequest } from "../configs/authConfig.ts";

// Erstelle eine Instanz der PublicClientApplication
export const msalInstance = new PublicClientApplication(msalConfig);

msalInstance.initialize().then(() => {
    // Account selection logic is app dependent. Adjust as needed for different use cases.
    const accounts = msalInstance.getAllAccounts();
    if (accounts.length > 0) {
        msalInstance.setActiveAccount(accounts[0]);
    }

    msalInstance.addEventCallback((event: EventMessage) => {
        //console.log("auth", "events", event);
        if (event.eventType === EventType.LOGIN_SUCCESS && event.payload) {
            const payload = event.payload as AuthenticationResult;
            const account = payload.account;
            msalInstance.setActiveAccount(account);
        }
    });
});

// Kontext für Authentifizierung
interface AuthContextType {
    account: AccountInfo | null;
    accessToken: string | null;
    isAuthenticated: boolean;
    login: () => Promise<void>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [account, setAccount] = useState<AccountInfo | null>(null);
    const [accessToken, setAccessToken] = useState<string | null>(null);

    const { instance } = useMsal();

    useEffect(() => {        
        const account = instance.getActiveAccount();
        if (account) {
            setAccount(account);
            instance.acquireTokenSilent(loginRequest).then(response => {
                setAccount(response.account);
                setAccessToken(response.accessToken);
                setIsAuthenticated(true);
            });
        }
    }, [instance]);

    const login = async () => {
        try {
            const response = await instance.loginPopup(loginRequest);
            setAccount(response.account);
            setAccessToken(response.accessToken);
            setIsAuthenticated(true);
        } catch (error) {
            console.error(error);
        }
    };

    const logout = () => {
        instance.logoutPopup();
        setAccount(null);
        setAccessToken(null);
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ account, accessToken, isAuthenticated, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

// Hook zum Verwenden des AuthContext
export const useAuth = (): AuthContextType => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};