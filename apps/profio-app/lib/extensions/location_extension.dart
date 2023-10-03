import 'package:geolocator/geolocator.dart';
import 'package:profio_staff_client/models/location.dart';

extension LocationExtension on Location {
  Position toPosition() {
    return Position(
        latitude: latitude,
        longitude: longitude,
        accuracy: 0,
        altitude: 0,
        heading: 0,
        speed: 0,
        speedAccuracy: 0,
        timestamp: null,
        floor: 0,
        isMocked: false,
        altitudeAccuracy: 0,
        headingAccuracy: 0);
  }
}
