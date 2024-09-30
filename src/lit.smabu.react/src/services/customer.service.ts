import axiosConfig from "../configs/axiosConfig";
import { CreateCustomerCommand, CustomerDTO, UpdateCustomerCommand } from "../types/domain";

export const createCustomer = async (payload: CreateCustomerCommand): Promise<CustomerDTO> => {
  return axiosConfig.post(`customers`, payload);
};

export const getCustomers = () => {
  return axiosConfig.get(`customers`);
};

export const getCustomerById = (id: string) => {
  return axiosConfig.get(`customers/${id}`);
};

export const updateCustomer = (id: string, payload: UpdateCustomerCommand): Promise<CustomerDTO> => {
  return axiosConfig.put(`customers/${id}`, payload);
};

export const deleteCustomer = (id: string): Promise<void> => {
  return axiosConfig.delete(`customers/${id}`);
};