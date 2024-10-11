import React, { createContext, ReactNode, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { getItemByRoute } from '../configs/navConfig';


interface TitleContextProps {
    setTitle: (title: string) => void;
}

export const TitleContext = createContext<TitleContextProps>({
    setTitle: () => {},
});

export const TitleProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const location = useLocation();

    useEffect(() => {
        var item = getItemByRoute(location.pathname);
        document.title = `smabu | ${item?.name}`;
    }, [location]);

    const setTitle = (title: string) => {
        document.title = title;
    };

    return (
        <TitleContext.Provider value={{ setTitle }}>
            {children}
        </TitleContext.Provider>
    );
};
