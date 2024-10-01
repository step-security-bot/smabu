import axiosConfig from "../configs/axiosConfig";
import { Currency, TaxRate } from "../types/domain";

export const getCurrencies = () => {
    return axiosConfig.get<Currency[]>(`common/currencies`);
};

export const getQuantityUnits = () => {
    return axiosConfig.get<string[]>(`common/quantityunits`);
};

export const getTaxRates = () => {
    return axiosConfig.get<TaxRate[]>(`common/taxrates`);
};