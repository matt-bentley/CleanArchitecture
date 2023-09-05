import { Component, OnInit } from '@angular/core';
import { WeatherService } from '../_shared/services/weather.service';
import { CreateWeatherForecast, WeatherForecast } from '../_shared/models/weather.model';
import { LocationsService } from '../_shared/services/locations.service';
import { WeatherLocation } from '../_shared/models/location.model';

@Component({
  selector: 'app-weather-forecasts',
  templateUrl: './weather-forecasts.component.html'
})
export class WeatherForecastsComponent implements OnInit {

  public locations: WeatherLocation[] = [];
  public forecasts: WeatherForecast[] = [];
  public selectedLocationId?: string;

  public constructor(private readonly _weatherService: WeatherService,
    private readonly _locationsService: LocationsService) {

  }

  public generate(): void {
    function getRandom(min: number, max: number) {
      const floatRandom = Math.random()

      const difference = max - min

      // random between 0 and the difference
      const random = Math.round(difference * floatRandom)

      const randomWithinRange = random + min

      return randomWithinRange
    }
    const temperature = getRandom(-50, 50);
    const forecast: CreateWeatherForecast = {
      date: new Date(),
      temperatureC: temperature,
      summary: this._weatherService.getTemperatureSummary(temperature),
      locationId: this.selectedLocationId!
    };
    this._weatherService.create(forecast)
      .subscribe(() => {
        this.loadForecasts();
      });
  }

  public delete(id: string): void {
    this._weatherService.delete(id)
      .subscribe(() => {
        this.loadForecasts();
      });
  }

  public loadForecasts(): void {
    if (this.selectedLocationId) {
      this._weatherService.get(this.selectedLocationId)
        .subscribe(forecasts => {
          this.forecasts = forecasts;
        });
    }
  }

  public ngOnInit(): void {
    this.loadLocations();
  }

  private loadLocations(): void {
    this._locationsService.get()
      .subscribe(locations => {
        this.locations = locations;
      });
  }
}
