import axiosConfig from "../configs/axiosConfig";
import { CreateCustomerCommand, CustomerDTO, UpdateCustomerCommand } from "../types/domain";

export const createCustomer = async (payload: CreateCustomerCommand): Promise<CustomerDTO> => {
  const response = await axiosConfig.post<CustomerDTO>(`customers`, payload);
  return response.data;
};

export const getCustomers = async (): Promise<CustomerDTO[]> => {
  const response = await axiosConfig.get<CustomerDTO[]>(`customers`);
  return response.data;
};

export const getCustomer = async (customerId: string): Promise<CustomerDTO> => {
  const response = await axiosConfig.get<CustomerDTO>(`customers/${customerId}`);
  return response.data;
};

export const updateCustomer = async (customerId: string, payload: UpdateCustomerCommand): Promise<CustomerDTO> => {
  const response = await axiosConfig.put<CustomerDTO>(`customers/${customerId}`, payload);
  return response.data;
};

export const deleteCustomer = async (customerId: string): Promise<void> => {
  await axiosConfig.delete(`customers/${customerId}`);
};