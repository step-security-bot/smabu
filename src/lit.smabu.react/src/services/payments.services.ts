import axiosConfig from "../configs/axiosConfig";
import { CompletePaymentCommand, CreatePaymentCommand, PaymentDTO, PaymentId, UpdatePaymentCommand } from "../types/domain";

export const createPayment = async (payload: CreatePaymentCommand): Promise<PaymentId> => {
  const response = await axiosConfig.post<PaymentId>(`payments`, payload);
  return response.data;
};

export const getPayments = async (): Promise<PaymentDTO[]> => {
  const response = await  axiosConfig.get<PaymentDTO[]>(`payments`);
  return response.data;
};

export const getPayment = async (paymentId: string): Promise<PaymentDTO> => {
  const response = await  axiosConfig.get<PaymentDTO>(`payments/${paymentId}`);
  return response.data;
};

export const updatePayment = async (paymentId: string, payload: UpdatePaymentCommand): Promise<void> => {
  const response = await axiosConfig.put(`payments/${paymentId}`, payload);
  return response.data;
};

export const completePayment = async (paymentId: string, payload: CompletePaymentCommand): Promise<void> => {
  const response = await axiosConfig.put(`payments/${paymentId}/complete`, payload);
  return response.data;
};

export const deletePayment = async (paymentId: string): Promise<void> => {
  await axiosConfig.delete(`payments/${paymentId}`);
};