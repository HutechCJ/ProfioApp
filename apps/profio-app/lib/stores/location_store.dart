import 'package:flutter/material.dart';
import 'package:geolocator/geolocator.dart';
import 'package:mobx/mobx.dart';
import 'package:profio_staff_client/enums/vehicle_type.dart';
import 'package:profio_staff_client/managers/location_manager.dart';
import 'package:profio_staff_client/models/location.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';

part 'location_store.g.dart';

class LocationStore = LocationStoreBase with _$LocationStore;

abstract class LocationStoreBase with Store {
  late VehicleStore vehicleStore;
  late HubStore hubStore;
  late MqttProvider mqttProvider;
  @observable
  Position? selectedPosition;

  @computed
  bool get hasSelectedPosition => selectedPosition != null;

  @action
  Future<void> getCurrentLocation() async {
    selectedPosition = await LocationManager.getPosition();
  }

  @action
  Future<void> setCurrentLocation(Position position) async {
    selectedPosition = position;
  }

  @action
  void simulateVehicleMovement() {
    if (!vehicleStore.hasSelectedVehicle) return;
    var vehicleSpeed = getVehicleSpeed(vehicleStore.selectedVehicle.type);

    var hubPosition = Position(
        latitude: hubStore.selectedHub.location.latitude,
        longitude: hubStore.selectedHub.location.longitude,
        accuracy: 0,
        altitude: 0,
        heading: 0,
        speed: 0,
        speedAccuracy: 0,
        timestamp: null,
        floor: 0,
        isMocked: false);

    if (!hasSelectedPosition) return;

    LocationManager.simulateCarMovement(
        mqttProvider, selectedPosition!, hubPosition, vehicleSpeed,
        onIntermediatePosition: (p) => {setCurrentLocation(p)});
  }

  double getVehicleSpeed(VehicleType type) {
    switch (type) {
      case VehicleType.motorcycle:
        return 40;
      case VehicleType.truck:
      case VehicleType.trailer:
      case VehicleType.van:
        return 60;
    }
  }

  void stopSimulation() {
    LocationManager.stopCarSimulation();
  }

  void onInit(BuildContext context) {
    vehicleStore = context.read<VehicleStore>();
    hubStore = context.read<HubStore>();
    mqttProvider = context.read<MqttProvider>();
    mqttProvider.connect();
  }
}
