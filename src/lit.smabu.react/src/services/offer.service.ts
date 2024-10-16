import axiosConfig from "../configs/axiosConfig";
import { AddOfferItemCommand, CreateOfferCommand, OfferDTO, UpdateOfferCommand, UpdateOfferItemCommand } from "../types/domain";

export const createOffer = (payload: CreateOfferCommand) => {
    return axiosConfig.post<OfferDTO[]>(`offers`, payload);
};

export const getOffers = () => {
    return axiosConfig.get<OfferDTO[]>(`offers`);
};

export const getOffer = (offerId: string, withItems: boolean = false) => {
    return axiosConfig.get<OfferDTO>(`offers/${offerId}?withItems=${withItems}`);
};

export const getOfferReport = (offerId: string) => {
    return axiosConfig.get<any>(`offers/${offerId}/report`, {
        headers: {
          'Content-Type': 'application/json',
        },
        responseType: 'blob',
      });
};

export const updateOffer = (offerId: string, payload: UpdateOfferCommand) => {
    return axiosConfig.put(`offers/${offerId}`, payload);
};

export const deleteOffer = (offerId: string) => {
    return axiosConfig.delete(`offers/${offerId}`);
};

export const addOfferItem = (offerId: string, payload: AddOfferItemCommand) => {
    return axiosConfig.post(`offers/${offerId}/items`, payload);
};

export const updateOfferItem = (offerId: string, itemId: string, payload: UpdateOfferItemCommand) => {
    return axiosConfig.put(`offers/${offerId}/items/${itemId}`, payload);
};

export const moveOfferItemUp = (offerId: string, itemId: string) => {
    return axiosConfig.put(`offers/${offerId}/items/${itemId}/moveup`);
};

export const moveOfferItemDown = (offerId: string, itemId: string) => {
    return axiosConfig.put(`offers/${offerId}/items/${itemId}/movedown`);
};

export const deleteOfferItem = (offerId: string, itemId: string) => {
    return axiosConfig.delete(`offers/${offerId}/items/${itemId}`);
};