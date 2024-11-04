import axiosConfig from "../configs/axiosConfig";
import { AddInvoiceItemCommand, CreateInvoiceCommand, InvoiceDTO, ReleaseInvoiceCommand, UpdateInvoiceCommand, UpdateInvoiceItemCommand, WithdrawReleaseInvoiceCommand } from "../types/domain";

export const createInvoice = (payload: CreateInvoiceCommand) => {
    return axiosConfig.post<InvoiceDTO[]>(`invoices`, payload);
};

export const getInvoices = () => {
    return axiosConfig.get<InvoiceDTO[]>(`invoices`);
};

export const getInvoice = (invoiceId: string, withItems: boolean = false) => {
    return axiosConfig.get<InvoiceDTO>(`invoices/${invoiceId}?withItems=${withItems}`);
};

export const getInvoiceReport = (invoiceId: string) => {
    return axiosConfig.get<any>(`invoices/${invoiceId}/report`, {
        headers: {
          'Content-Type': 'application/json',
        },
        responseType: 'blob',
      });
};

export const updateInvoice = (invoiceId: string, payload: UpdateInvoiceCommand) => {
    return axiosConfig.put(`invoices/${invoiceId}`, payload);
};

export const releaseInvoice = (invoiceId: string, payload: ReleaseInvoiceCommand) => {
    return axiosConfig.put<ReleaseInvoiceCommand>(`invoices/${invoiceId}/release`, payload);
};

export const withdrawReleaseInvoice = (invoiceId: string, payload: WithdrawReleaseInvoiceCommand) => {
    return axiosConfig.put<WithdrawReleaseInvoiceCommand>(`invoices/${invoiceId}/withdrawrelease`, payload);
};

export const deleteInvoice = (invoiceId: string) => {
    return axiosConfig.delete(`invoices/${invoiceId}`);
};

export const addInvoiceItem = (invoiceId: string, payload: AddInvoiceItemCommand) => {
    return axiosConfig.post(`invoices/${invoiceId}/items`, payload);
};

export const updateInvoiceItem = (invoiceId: string, invoiceItemId: string, payload: UpdateInvoiceItemCommand) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${invoiceItemId}`, payload);
};

export const moveInvoiceItemUp = (invoiceId: string, invoiceItemId: string) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${invoiceItemId}/moveup`);
};

export const moveInvoiceItemDown = (invoiceId: string, invoiceItemId: string) => {
    return axiosConfig.put(`invoices/${invoiceId}/items/${invoiceItemId}/movedown`);
};

export const deleteInvoiceItem = (invoiceId: string, invoiceItemId: string) => {
    return axiosConfig.delete(`invoices/${invoiceId}/items/${invoiceItemId}`);
};