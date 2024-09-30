import axiosConfig from "../configs/axiosConfig";
import { AddInvoiceItemCommand, CreateInvoiceCommand, InvoiceDTO, InvoiceItemId, UpdateInvoiceCommand } from "../types/domain";

export const createInvoice = (payload: CreateInvoiceCommand) => {
    return axiosConfig.post<InvoiceDTO[]>(`invoices`, payload);
};

export const getInvoices = () => {
    return axiosConfig.get<InvoiceDTO[]>(`invoices`);
};

export const getInvoice = (invoiceId: string, withItems: boolean = false) => {
    return axiosConfig.get<InvoiceDTO>(`invoices/${invoiceId}?withItems=${withItems}`);
};

export const updateInvoice = (invoiceId: string, payload: UpdateInvoiceCommand) => {
    return axiosConfig.put(`invoices/${invoiceId}`, payload);
};

export const deleteInvoice = (invoiceId: string) => {
    return axiosConfig.delete(`invoices/${invoiceId}`);
};

export const addInvoiceItem = (invoiceId: string, payload: AddInvoiceItemCommand) => {
    return axiosConfig.post(`invoices/${invoiceId}/items`, payload);
};

export const updateInvoiceItem = (invoiceId: string, itemId: InvoiceItemId, payload: UpdateInvoiceCommand) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${itemId.value}`, payload);
};

export const moveInvoiceItemUp = (invoiceId: string, itemId: InvoiceItemId) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${itemId.value}/moveup`);
};

export const moveInvoiceItemDown = (invoiceId: string, itemId: InvoiceItemId) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${itemId.value}/movedown`);
};

export const deleteInvoiceItem = (invoiceId: string, itemId: InvoiceItemId) => {
    return axiosConfig.delete(`invoices/${invoiceId}/items/${itemId.value}`);
};