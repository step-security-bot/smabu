import axiosConfig from "../configs/axiosConfig";
import { Currency, TaxRate, Unit } from "../types/domain";

export const getCurrencies = async (): Promise<Currency[]> => {
    const response = await axiosConfig.get<Currency[]>(`common/currencies`);
    return response?.data;
};

export const getQuantityUnits = async (): Promise<Unit[]> => {
    const response = await axiosConfig.get<Unit[]>(`common/units`);
    return response?.data;
};

export const getTaxRates = async (): Promise<TaxRate[]> => {
    const response = await  axiosConfig.get<TaxRate[]>(`common/taxrates`);
    return response?.data;
};