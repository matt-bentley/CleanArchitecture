export interface WeatherForecast {
    id: string;
    date: Date;
    temperatureC: number;
    temperatureF: number;
    summary: string;
    locationId: string;
}

export interface CreateWeatherForecast {
    date: Date;
    temperatureC: number;
    summary: string;
    locationId: string;
}