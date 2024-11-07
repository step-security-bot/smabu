import axiosConfig from "../configs/axiosConfig";
import { AddCatalogGroupCommand, AddCatalogItemCommand, CatalogDTO, CatalogGroupDTO, CatalogItemDTO, UpdateCatalogCommand, UpdateCatalogGroupCommand, UpdateCatalogItemCommand } from "../types/domain";

export const getDefaultCatalog = async (): Promise<CatalogDTO> => {
  const response = await  axiosConfig.get<CatalogDTO>(`catalogs/default`);
  return response.data;
};

export const updateCatalog = async (catalogId: string, payload: UpdateCatalogCommand): Promise<CatalogDTO> => {
  const response = await axiosConfig.put<CatalogDTO>(`catalogs/${catalogId}`, payload);
  return response.data;
};

export const deleteCatalog = async (catalogId: string): Promise<void> => {
  await axiosConfig.delete(`catalogs/${catalogId}`);
};

export const getCatalogGroup = async (catalogId: string, catalogGroupId: string) => {
  const response = await axiosConfig.get<CatalogGroupDTO>(`catalogs/${catalogId}/groups/${catalogGroupId}`);
  return response.data;
};

export const addCatalogGroup = async (catalogId: string, payload: AddCatalogGroupCommand): Promise<CatalogGroupDTO> => {
  const response = await axiosConfig.post<CatalogGroupDTO>(`catalogs/${catalogId}/groups`, payload);
  return response.data;
};

export const updateCatalogGroup = async (catalogId: string, catalogGroupId: string, payload: UpdateCatalogGroupCommand): Promise<CatalogGroupDTO> => {
  const response = await axiosConfig.put<CatalogGroupDTO>(`catalogs/${catalogId}/groups/${catalogGroupId}`, payload);
  return response.data;
};

export const removeCatalogGroup = async (catalogId: string, catalogGroupId: string): Promise<void> => {
  await axiosConfig.delete(`catalogs/${catalogId}/groups/${catalogGroupId}`);
};

export const getCatalogItems = async (): Promise<CatalogItemDTO[]> => {
  const response = await axiosConfig.get<CatalogDTO>(`catalogs/default`);
  return (response?.data?.groups?.flatMap(group => group.items!) ?? []);
};

export const getCatalogItem = async (catalogId: string, catalogItemId: string): Promise<CatalogItemDTO> => {
  const response = await axiosConfig.get<CatalogItemDTO>(`catalogs/${catalogId}/items/${catalogItemId}`);
  return response.data;
};

export const addCatalogItem = async (catalogId: string, payload: AddCatalogItemCommand): Promise<CatalogItemDTO> => {
  const response = await axiosConfig.post<CatalogItemDTO>(`catalogs/${catalogId}/items`, payload);
  return response.data;
};

export const updateCatalogItem = async (catalogId: string, catalogItemId: string, payload: UpdateCatalogItemCommand): Promise<CatalogItemDTO> => {
  const response = await axiosConfig.put<CatalogItemDTO>(`catalogs/${catalogId}/items/${catalogItemId}`, payload);
  return response.data;
};

export const removeCatalogItem = async (catalogId: string, catalogItemId: string): Promise<void> => {
  await axiosConfig.delete(`catalogs/${catalogId}/items/${catalogItemId}`);
};
