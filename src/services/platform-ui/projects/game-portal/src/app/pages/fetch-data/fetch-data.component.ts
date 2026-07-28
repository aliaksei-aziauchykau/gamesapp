import { APP_BASE_HREF } from '@angular/common';
import { Component, Inject, Injector } from '@angular/core';
import { ServerDataToken } from '@itdl/game-portal/common';
import { IInjectorProvider, ServerSideDataByToken } from '@itdl/shared/common';
import { HttpService } from '@itdl/shared/http';
import { of } from 'rxjs';

import { WeatherForecast } from '../../interfaces/weather-forecast';

@Component({
    selector: 'gpl-fetch-data',
    templateUrl: './fetch-data.component.html',
    styleUrls: ['./fetch-data.component.scss'],
})
export class FetchDataComponent implements IInjectorProvider {
    forecasts: WeatherForecast[] = [];

    constructor(
        private readonly httpService: HttpService,
        @Inject(APP_BASE_HREF) private baseUrl: string,
        public readonly injector: Injector,
    ) {
        this.getWeatherForecasts().subscribe({
            next: (result) => {
                this.forecasts = result;
            },
            error: (error) => {
                console.error(error);
            },
        });
    }

    @ServerSideDataByToken(ServerDataToken, (dataProvider) => dataProvider.weatherForecasts, (data) => of(data))
    private getWeatherForecasts() {
        return this.httpService.get<WeatherForecast[]>(`${this.baseUrl}/WeatherForecast`);
    }
}
