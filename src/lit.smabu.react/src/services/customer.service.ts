import axiosConfig from "../configs/axiosConfig";
import { CreateCustomerCommand, CustomerDTO, UpdateCustomerCommand } from "../types/domain";

export const createCustomer = async (payload: CreateCustomerCommand) => {
  return axiosConfig.post(`customers`, payload);
};

export const getCustomers = () => {
  return axiosConfig.get<CustomerDTO[]>(`customers`);
};

export const getCustomer = (customerId: string) => {
  return axiosConfig.get<CustomerDTO>(`customers/${customerId}`);
};

export const updateCustomer = (customerId: string, payload: UpdateCustomerCommand) => {
  return axiosConfig.put(`customers/${customerId}`, payload);
};

export const deleteCustomer = (customerId: string) => {
  return axiosConfig.delete(`customers/${customerId}`);
};