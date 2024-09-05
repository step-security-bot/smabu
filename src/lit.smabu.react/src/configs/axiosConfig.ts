
import { useIsAuthenticated } from '@azure/msal-react';
import axios from 'axios';
import { useAuth } from '../contexts/authContext';

// Next we make an 'instance' of it
const instance = axios.create({
    // .. where we make our configurations
    baseURL: 'https://api.example.com'
});

const { account, accessToken } = useAuth();

// Where you would set stuff like your 'Authorization' header, etc ...
instance.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
instance.defaults.headers.common['X-User-Name'] = account?.username;

// Also add/ configure interceptors && all the other cool stuff
instance.interceptors.request.use(
    config => {
        console.log('axios', 1, useIsAuthenticated(), accessToken, account, config);
        if (accessToken && !config.headers.Authorization) {
            if (accessToken) {
                config.headers.Authorization = `Bearer ${accessToken}`;
                config.headers.set("X-User-Name", account?.username);
            }
        }

        return config;
    },
    error => Promise.reject(error)
);

export default instance;