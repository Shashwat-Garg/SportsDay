function convertStringToDate(dateTimeString: string): Date {
    const date: Date = new Date(Date.parse(dateTimeString));
    return date;
}

function getFormattedTime(date: Date): string {
    const timeString: string = `${date.getHours() % 12}${date.getMinutes() > 0 ? (":" + date.getMinutes()) : ""} ${date.getHours() >= 12 ? "pm" : "am"}`;
    return timeString;
}

function areDatesEqual(date1: Date, date2: Date): boolean {
    return (!(date1 < date2) && !(date1 > date2));
}

export {
    convertStringToDate,
    getFormattedTime,
    areDatesEqual
};