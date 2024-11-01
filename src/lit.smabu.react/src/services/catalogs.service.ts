import axiosConfig from "../configs/axiosConfig";
import { CatalogDTO } from "../types/domain";


export const getDefaultCatalog = () => {
  return axiosConfig.get<CatalogDTO[]>(`catalogs/default`);
};
