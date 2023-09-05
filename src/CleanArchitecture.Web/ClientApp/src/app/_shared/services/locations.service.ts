import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WeatherLocation } from '../models/location.model';

@Injectable({
    providedIn: 'root'
})
export class LocationsService {

    public constructor(private readonly _http: HttpClient) {

    }

    public get(): Observable<WeatherLocation[]> {
        return this._http.get<WeatherLocation[]>('api/locations');
    }
}