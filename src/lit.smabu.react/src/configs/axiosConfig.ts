
import axios from 'axios';

// Next we make an 'instance' of it
const axiosConfig = axios.create({
    // .. where we make our configurations
    baseURL: 'http://localhost:5035/'
});

axiosConfig.defaults.headers.common['Authorization'] = `Bearer ${sessionStorage.getItem('authIdToken')}` ;

axiosConfig.interceptors.request.use(
    config => {
        const idToken = sessionStorage.getItem('authIdToken');
        config.headers.Authorization = `Bearer ${idToken}`;
        return config;
    },
    error => Promise.reject(error)
);

export default axiosConfig;