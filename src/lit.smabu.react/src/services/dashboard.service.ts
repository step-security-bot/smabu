import axiosConfig from "../configs/axiosConfig";
import { GetWelcomeDashboardReadModel } from "../types/domain";

export const getWelcomeDashboard = ()  => {
  return axiosConfig.get<GetWelcomeDashboardReadModel>(`dashboards/welcome`)
    .then(response => response.data);
};
