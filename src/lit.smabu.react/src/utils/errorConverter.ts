import axios, { AxiosError } from 'axios';
import { ModelError } from '../types/domain';

export interface AppError {
    message: string;
    code: string;
    status?: number;
    details?: any;
    severity?: 'error' | 'warning';
    name: 'AppError';
}

export const convertToAppError = (error: any): AppError => {
    if (isAppError(error)) {
        console.log(1, error, isAppError(error));
        return error as AppError;
    } else if (axios.isAxiosError(error)) {
        console.log(2, error);
        const axiosError = error as AxiosError;
        const status = axiosError.response?.status;
        const modelError = error.response?.data as ModelError;
        if (modelError?.code) {         
            console.log(3, modelError);   
            return {
                code: axiosError.code ?? 'UNKNOWN',
                status: status,
                message: `Vorgang nicht möglich`,
                details: `${modelError.description}`,
                severity: 'warning',
                name: 'AppError',
            };
        } else {            
            return {
                code: axiosError.code ?? 'UNKNOWN',
                status: status,
                message: `${error.response?.status}-${error.response?.statusText}`,
                details:  `${error.response?.data}`,
                severity: status && status >= 400 && status < 500 ? 'warning' : 'error',
                name: 'AppError'
            };
        }
    } else if (error instanceof Error) {
        return {
            code: 'UNKNOWN',
            message: error.message,
            details: error.stack,
            severity: 'error',
            name: 'AppError'
        };
    } else {
        return {
            code: 'UNKNOWN',
            message: 'An unknown error occurred',
            details: error,
            severity: 'error',
            name: 'AppError'
        };
    }
};


const isAppError = (error: any): error is AppError => {
    return error?.name === 'AppError';
};