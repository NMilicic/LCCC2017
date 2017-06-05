export class Marker {
    lat: number;
    lng: number;
    label?: string;
    draggable: boolean;
    title: string;
    openInfoWindow? :boolean;
}

export interface MapMouseEvent extends MouseEvent {
    coords: Marker
}
