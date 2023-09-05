import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateWeatherForecast, WeatherForecast } from '../models/weather.model';
import { CreatedResult } from '../models/results.model';

@Injectable({
    providedIn: 'root'
})
export class WeatherService {

    public constructor(private readonly _http: HttpClient) {

    }

    public get(locationId: string): Observable<WeatherForecast[]> {
        return this._http.get<WeatherForecast[]>(`api/weatherforecasts?locationId=${locationId}`);
    }

    public create(forecast: CreateWeatherForecast) : Observable<CreatedResult>{
        return this._http.post<CreatedResult>('api/weatherforecasts', forecast);
    }

    public delete(id: string): Observable<void> {
        return this._http.delete<void>(`api/weatherforecasts/${id}`);
    }

    public getTemperatureSummary(temperature: number): string {
        if (temperature > 40) {
            return "Scorching";
        }
        else if (temperature > 20) {
            return "Hot";
        }
        else if (temperature > 10) {
            return "Mild";
        }
        else if (temperature > 0) {
            return "Cold";
        }
        else if (temperature === null) {
            return "";
        }
        else {
            return "Freezing";
        }
    }
}