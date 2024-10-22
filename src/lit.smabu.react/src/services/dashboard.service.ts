import axiosConfig from "../configs/axiosConfig";
import { GetSalesDashboardReadModel, GetWelcomeDashboardReadModel } from "../types/domain";

export const getWelcomeDashboard = () => {
    return axiosConfig.get<GetWelcomeDashboardReadModel>(`dashboards/welcome`)
        .then(response => response.data);
};

export const getSalesDashboard = () => {
    return axiosConfig.get<GetSalesDashboardReadModel>(`dashboards/sales`)
        .then(response => response.data);
};
