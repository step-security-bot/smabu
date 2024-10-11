import React, { createContext, useContext, useState, ReactNode } from 'react';
import { Snackbar, SnackbarOrigin, Alert, AlertColor, Slide, SlideProps } from '@mui/material';
import { JSX } from 'react/jsx-runtime';

interface ToastNotification {
    message: string;
    severity: AlertColor;
}

interface NotificationContextType {
    toast: (message: string, severity: AlertColor) => void;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const useNotification = (): NotificationContextType => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error('useNotification must be used within a NotificationProvider');
    }
    return context;
};

interface NotificationProviderProps {
    children: ReactNode;
}

const TransitionUp = (props: JSX.IntrinsicAttributes & SlideProps) => {
    return <Slide {...props} direction="up" />;
};

export const NotificationProvider: React.FC<NotificationProviderProps> = ({ children }) => {
    const [notifications, setNotifications] = useState<ToastNotification[]>([]);

    const toast = (message: string, severity: AlertColor) => {
        setNotifications([...notifications, { message, severity }]);
    };

    const handleCloseToast = (notification: ToastNotification) => {
        setNotifications(notifications.filter(x => x !== notification));
    };

    return (
        <NotificationContext.Provider value={{ toast: toast }}>
            {children && React.isValidElement(children) ? children : null}
            {notifications.map((notification, index) => (<Snackbar
                key={index + "_" + Math.random()}
                open={true}
                TransitionComponent={TransitionUp}
                autoHideDuration={6000}
                onClose={() => handleCloseToast(notification)}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' } as SnackbarOrigin}
            >
                <Alert onClose={() => handleCloseToast(notification)} severity={notification.severity} sx={{ width: '100%' }}>
                    {notification.message}
                </Alert>
            </Snackbar>))}
        </NotificationContext.Provider>
    );
};