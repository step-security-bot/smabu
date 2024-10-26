import axiosConfig from "../configs/axiosConfig";
import { CreateOrderCommand, OrderDTO, UpdateOrderCommand } from "../types/domain";

export const createOrder = (payload: CreateOrderCommand) => {
    return axiosConfig.post<OrderDTO[]>(`orders`, payload);
};

export const getOrders = () => {
    return axiosConfig.get<OrderDTO[]>(`orders`);
};

export const getOrder= (orderId: string) => {
    return axiosConfig.get<OrderDTO>(`orders/${orderId}`);
};

export const updateOrder = (orderId: string, payload: UpdateOrderCommand) => {
    return axiosConfig.put(`orders/${orderId}`, payload);
};

export const deleteOffer = (orderId: string) => {
    return axiosConfig.delete(`orders/${orderId}`);
};
