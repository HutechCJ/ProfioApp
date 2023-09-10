import 'dart:math';

import 'package:geolocator/geolocator.dart';

class RandomLocationGenerator {
  static const double minLatitude = -90.0; // Minimum latitude
  static const double maxLatitude = 90.0; // Maximum latitude
  static const double minLongitude = -180.0; // Minimum longitude
  static const double maxLongitude = 180.0; // Maximum longitude

  static final Random _random = Random();

  // Generate a random latitude within the specified range
  static double generateRandomLatitude() {
    return minLatitude + _random.nextDouble() * (maxLatitude - minLatitude);
  }

  // Generate a random longitude within the specified range
  static double generateRandomLongitude() {
    return minLongitude + _random.nextDouble() * (maxLongitude - minLongitude);
  }

  // Generate a random Position with random latitude and longitude
  static Position generateRandomPosition() {
    final latitude = generateRandomLatitude();
    final longitude = generateRandomLongitude();
    return Position(
      latitude: latitude,
      longitude: longitude,
      timestamp: DateTime.now(),
      altitude: 0.0,
      accuracy: 0.0,
      heading: 0.0,
      speed: 0.0,
      speedAccuracy: 0.0,
      floor: null,
      isMocked: false,
    );
  }

  static Position generatePositionNearby(
      Position basePosition, double maxDistanceMeters) {
    final maxDistanceDegrees =
        maxDistanceMeters / 111300.0; // Approximate degrees per meter

    final randomLatitude = basePosition.latitude +
        (_random.nextDouble() * 2 - 1) * maxDistanceDegrees;
    final randomLongitude = basePosition.longitude +
        (_random.nextDouble() * 2 - 1) * maxDistanceDegrees;

    return Position(
      latitude: randomLatitude,
      longitude: randomLongitude,
      timestamp: DateTime.now(),
      altitude: 0.0,
      accuracy: 0.0,
      heading: 0.0,
      speed: 0.0,
      speedAccuracy: 0.0,
      floor: null,
      isMocked: false,
    );
  }
}
