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