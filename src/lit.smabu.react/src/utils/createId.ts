import { v4 as uuidv4 } from 'uuid';

export default function createId<T>(): T {
    const guid = uuidv4();
    const result = { value: guid } as unknown as T;
    return result
}