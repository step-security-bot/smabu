type HandleAsyncTaskParams<T> = {
    task: () => Promise<T>;
    onSuccess?: (res: T) => void;
    onError?: (err: any) => void;
    onComplete?: () => void;
    onLoading?: (loading: boolean) => void;
    resetError?: boolean
};

export const handleAsyncTask = async <T>({
    task,
    onSuccess,
    onError,
    onComplete,
    onLoading,
    resetError = true
}: HandleAsyncTaskParams<T>): Promise<T> => {
            
    if (onLoading) {
        onLoading(true);
    }

    if (resetError && onError) {
        onError(null);
    }   

    try {
        const result = await task();
        if (onSuccess) {
            onSuccess(result);
        }
        return result;
    } catch (error) {
        if (onError) {
            onError(error); // todo format error
        }
        throw error;
    } finally {
        if (onLoading) {
            onLoading(false);
        }
        if (onComplete) {
            onComplete();
        }
    }
}