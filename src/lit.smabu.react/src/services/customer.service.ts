import axiosConfig from "../configs/axiosConfig";
import { CreateCustomerCommand, CustomerDTO, UpdateCustomerCommand } from "../types/domain";

export const createCustomer = async (payload: CreateCustomerCommand) => {
  return axiosConfig.post(`customers`, payload);
};

export const getCustomers = () => {
  return axiosConfig.get<CustomerDTO[]>(`customers`);
};

export const getCustomer = (id: string) => {
  return axiosConfig.get<CustomerDTO>(`customers/${id}`);
};

export const updateCustomer = (id: string, payload: UpdateCustomerCommand) => {
  return axiosConfig.put(`customers/${id}`, payload);
};

export const deleteCustomer = (id: string) => {
  return axiosConfig.delete(`customers/${id}`);
};