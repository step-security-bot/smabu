export const formatDate = (date: Date | null | undefined, fallback: string = ''): string => {
    if (!date) return fallback;
    date = new Date(date.toString().substring(0, 10));
    const options: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    return new Intl.DateTimeFormat('de-DE', options).format(date);
};

export const formatTime = (date: Date | null | undefined, fallback: string = ''): string => {
    if (!date) return fallback;
    date = new Date(date);
    const options: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit' };
    return new Intl.DateTimeFormat('de-DE', options).format(date);
};

export const formatDateTime = (date: Date | null | undefined, fallback: string = ''): string => {
    if (!date) return fallback;
    date = new Date(date);
    const dateOptions: Intl.DateTimeFormatOptions = { year: 'numeric', month: '2-digit', day: '2-digit' };
    const timeOptions: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit' };
    const formattedDate = new Intl.DateTimeFormat('de-DE', dateOptions).format(date);
    const formattedTime = new Intl.DateTimeFormat('de-DE', timeOptions).format(date);
    return `${formattedDate} ${formattedTime}`;
};

/**
 * Formats a date string to the C# DateOnly format (yyyy-MM-dd).
 * @param dateStr - The date string to format.
 * @returns The formatted date string in yyyy-MM-dd format.
 */
export const formatToDateOnly = (dateStr: string): string => {
    const date = new Date(dateStr);
    if (isNaN(date.getTime())) {
        throw new Error('Invalid date string');
    }

    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
}