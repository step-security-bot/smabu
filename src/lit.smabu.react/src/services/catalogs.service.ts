import axiosConfig from "../configs/axiosConfig";
import { AddCatalogGroupCommand, AddCatalogItemCommand, CatalogDTO, CatalogGroupDTO, CatalogItemDTO, UpdateCatalogCommand, UpdateCatalogGroupCommand, UpdateCatalogItemCommand } from "../types/domain";


export const getDefaultCatalog = () => {
  return axiosConfig.get<CatalogDTO>(`catalogs/default`);
};

export const updateCatalog = (catalogId: string, payload: UpdateCatalogCommand) => {
  return axiosConfig.put(`catalogs/${catalogId}`, payload);
};


export const getCatalogGroup = (catalogId: string, catalogGroupId: string) => {
  return axiosConfig.get<CatalogGroupDTO>(`catalogs/${catalogId}/groups/${catalogGroupId}`);
};

export const addCatalogGroup = (catalogId: string, payload: AddCatalogGroupCommand) => {
  return axiosConfig.post<AddCatalogGroupCommand>(`catalogs/${catalogId}/groups`, payload);
};

export const updateCatalogGroup = (catalogId: string, catalogGroupId: string, payload: UpdateCatalogGroupCommand) => {
  return axiosConfig.put(`catalogs/${catalogId}/groups/${catalogGroupId}`, payload);
};

export const removeCatalogGroup = (catalogId: string, catalogGroupId: string) => {
  return axiosConfig.delete(`catalogs/${catalogId}/groups/${catalogGroupId}`);
};


export const getCatalogItems = (): Promise<CatalogItemDTO[]> => {
  return axiosConfig.get<CatalogDTO>(`catalogs/default`)
    .then(response => {
      return (response?.data?.groups?.flatMap(group => group.items!) ?? [])
    });
};

export const getCatalogItem = (catalogId: string, catalogItemId: string) => {
  return axiosConfig.get<CatalogItemDTO>(`catalogs/${catalogId}/items/${catalogItemId}`);
};

export const addCatalogItem = (catalogId: string, payload: AddCatalogItemCommand) => {
  return axiosConfig.post<AddCatalogItemCommand>(`catalogs/${catalogId}/items`, payload);
};

export const updateCatalogItem = (catalogId: string, catalogItemId: string, payload: UpdateCatalogItemCommand) => {
  return axiosConfig.put(`catalogs/${catalogId}/items/${catalogItemId}`, payload);
};

export const removeCatalogItem = (catalogId: string, catalogItemId: string) => {
  return axiosConfig.delete(`catalogs/${catalogId}/items/${catalogItemId}`);
};
