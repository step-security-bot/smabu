import { TextField } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { getUnits } from '../../services/common.service';
import { Unit } from '../../types/domain';

interface SelectFieldProps {
    label: string;
    name: string;
    items: any[];
    value: string | null | undefined;
    required: boolean;
    onChange: (event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => void;
    onGetValue: (item: any) => string;
    onGetLabel: (item: any) => string;
}

interface TypedSelectFieldProps {
    label: string;
    name: string;
    value: string | null | undefined;
    required: boolean;
    onChange: (event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>) => void;
}

export const UnitSelectField: React.FC<TypedSelectFieldProps> = ({ name, value, required, onChange }) => {
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(undefined);
    const [units, setUnits] = useState<Unit[]>([]);

    useEffect(() => {
        getUnits()
            .then(response => {
                setUnits(response);
                setLoading(false);
            })
            .catch(error => {
                setError(error);
            });
    }, []);

    return (
        <SelectField items={units} label={"Einheit"} name={name} value={value} 
            required={required} onChange={onChange} 
            onGetLabel={(item) => item.name }
            onGetValue={(item) => item.value } />
    );
}

const SelectField: React.FC<SelectFieldProps> = ({ items, label, name, value, required, onChange, onGetLabel, onGetValue }) => {
    const onPrepareChange = (e: any) => {
        let { name: targetName, value: targetValue } = e.target;       
        if (targetName === name) {  
            targetValue = items.find(i => i.value === targetValue);
        }  
        onChange({ target: { name: targetName, value: targetValue } } as any);
    }

    return (
        <TextField select fullWidth label={label} name={name}
            value={value} onChange={onPrepareChange} required={required}
            slotProps={{
                select: {
                    native: true,
                }
            }}>
            {items.map((item) => (
                <option key={onGetValue(item)} value={onGetValue(item)}>
                    {onGetLabel(item)}
                </option>
            ))}
        </TextField>
    );
};

export default SelectField;