
import axios from 'axios';

// Next we make an 'instance' of it
const axiosConfig = axios.create({
    // .. where we make our configurations
    baseURL: 'http://localhost:5035/'
});

axiosConfig.defaults.headers.common['Authorization'] = `Bearer ${sessionStorage.getItem('authIdToken')}`;

axiosConfig.interceptors.request.use(
    config => {
        const idToken = sessionStorage.getItem('authIdToken');
        config.headers.Authorization = `Bearer ${idToken}`;
        return config;
    },
    error => Promise.reject(error)
);

axiosConfig.interceptors.response.use(async (response) => {
    if (process.env.NODE_ENV === 'development') {
        await new Promise((resolve) => setTimeout(resolve, 50));
    }
    return response;
});

axiosConfig.interceptors.request.use(async (request) => {
    if (process.env.NODE_ENV === 'development') {
        await new Promise((resolve) => setTimeout(resolve, 50));
    }
    return request;
});

export default axiosConfig;