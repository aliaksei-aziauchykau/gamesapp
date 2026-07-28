import { CommonModule } from '@angular/common';
import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { GameXSState } from '@itdl/game-portal/features/game/ng-xs';
import { GpllGeoGuesserGameSocketActions } from '@itdl/game-portal/features/games/geo-guesser-game/socket';
import { ShrlUiActionsService } from '@itdl/shared/common/ui-actions';
import { Store } from '@ngxs/store';
import { Icon, latLng, LeafletEvent, LeafletMouseEvent, Map, Marker, Polyline, tileLayer } from 'leaflet';

@Component({
    selector: 'gpll-geo-guesser-game-board',
    standalone: true,
    templateUrl: './geo-guesser-game-board.component.html',
    styleUrls: ['./geo-guesser-game-board.component.scss'],
    imports: [CommonModule, LeafletModule],
})
export class GpllGeoGuesserGameBoardComponent implements OnInit, OnDestroy {
    private readonly uiActions = inject(ShrlUiActionsService);
    private readonly store = inject(Store);
    private readonly gameCode = this.store.selectSnapshot(GameXSState.gameCode);

    public map!: Map;
    public markers: Marker[] = [];
    public options = {
        layers: [
            tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                opacity: 1,
                maxZoom: 19,
                minZoom: 7,
                // detectRetina: true,
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
            }),
        ],
        zoom: 7,
        center: latLng(53.558869, 28.248644),
    };

    private zoom!: number;
    private polyline!: Polyline;
    private marker!: Marker;

    public ngOnInit() {
        this.uiActions.dispatch(new GpllGeoGuesserGameSocketActions.StartGame(this.gameCode));
    }

    public ngOnDestroy() {
        this.map.clearAllEventListeners();
    }

    public onMapReady(map: Map) {
        this.map = map;
        this.zoom = map.getZoom();
        const greenIcon = new Icon({
            iconUrl:
                'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
            shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
            iconSize: [25, 41],
            iconAnchor: [12, 41],
            popupAnchor: [1, -34],
            shadowSize: [41, 41],
        });
        const marker = new Marker(latLng(53.893009, 27.567444), { draggable: false, icon: greenIcon });
        marker.addTo(this.map);
    }

    public onMapZoomEnd(event: LeafletEvent) {
        this.zoom = event.target.getZoom();
    }

    public mapClicked($event: LeafletMouseEvent) {
        if (this.marker) this.marker.remove();
        const marker = new Marker($event.latlng, { draggable: true });
        this.marker = marker;
        marker.on('dragend', ($dragendEvent) => {
            const marker = $dragendEvent.target;
            const position = marker.getLatLng();
            marker.setLatLng(latLng(position.lat, position.lng), { draggable: true });
            this.map.panTo(latLng(position.lat, position.lng));
            this.polyline
                .setLatLngs([latLng(53.893009, 27.567444), latLng(position.lat, position.lng)])
                .addTo(this.map);
        });
        marker.addTo(this.map);
        if (this.polyline) this.polyline.remove();
        const polyline = new Polyline([latLng(53.893009, 27.567444), $event.latlng], { color: 'red' }).addTo(this.map);
        this.map.fitBounds(polyline.getBounds());
        this.polyline = polyline;
    }
}
