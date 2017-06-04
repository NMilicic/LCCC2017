import { Component, OnInit } from '@angular/core';
import { AgmMap, AgmMarker } from '@agm/core';
import { User } from '../models/user'

@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit {
  lat: number = 51.678418;
  lng: number = 7.809007;
  markers: marker[] = [];
  zoom: number = 8;
  gameStarted: boolean;
  constructor() {

  }


  ngOnInit() {
  }
  mapClicked($event: MapMouseEvent) {
    this.markers = [];
    this.markers.push({
      lat: $event.coords.lat,
      lng: $event.coords.lng,
      draggable: true
    });

  }

  onNotify(message: boolean): void {
    this.gameStarted = true;
  }

}
interface marker {
  lat: number;
  lng: number;
  label?: string;
  draggable: boolean;
}
interface MapMouseEvent extends MouseEvent {
  coords: marker
}
