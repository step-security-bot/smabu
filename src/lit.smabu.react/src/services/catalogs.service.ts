import axiosConfig from "../configs/axiosConfig";
import { CatalogDTO, CatalogItemDTO, UpdateCatalogItemCommand } from "../types/domain";


export const getDefaultCatalog = () => {
  return axiosConfig.get<CatalogDTO>(`catalogs/default`);
};

export const getCatalogItem = (catalogId: string, id: string) => {
  return axiosConfig.get<CatalogItemDTO>(`catalogs/${catalogId}/items/${id}`);
};

export const updateCatalogItem = (catalogId: string, id: string, payload: UpdateCatalogItemCommand) => {
  return axiosConfig.put(`catalogs/${catalogId}/items/${id}`, payload);
};
