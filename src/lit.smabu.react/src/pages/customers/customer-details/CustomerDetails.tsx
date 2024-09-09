import { useState, useEffect } from 'react';
import axiosConfig from "../../../configs/axiosConfig";
import { CustomerDTO } from '../../../types/domain';
import { useParams } from 'react-router-dom';


const CustomerDetails = () => {
    const params = useParams()
    const [data, setData] = useState<CustomerDTO>();
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
        
    useEffect(() => {
        axiosConfig.get<CustomerDTO>('customers/' + params.id)
            .then(response => {
                setData(response.data);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
                setLoading(false);
                console.log(99, error);
            });
    }, []);

    return (
        <div>
            {data ? (
                <div>
                    <h1>Customer Details</h1>
                    <p>ID: {data.id?.value}</p>
                    <p>Name: {data.name}</p>
                </div>
            ) : (
                <p>Loading customer details...</p>
            )}
        </div>
    );
};

export default CustomerDetails;