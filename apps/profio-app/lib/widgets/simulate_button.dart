import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/location_store.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';

class SimulateButton extends StatefulWidget {
  const SimulateButton({
    Key? key,
  }) : super(key: key);

  @override
  State<SimulateButton> createState() => _SimulateButtonState();
}

class _SimulateButtonState extends State<SimulateButton> {
  late LocationStore locationStore;
  late VehicleStore vehicleStore;

  @override
  void initState() {
    locationStore = context.read<LocationStore>();
    vehicleStore = context.read<VehicleStore>();
    locationStore.onInit(context);
    locationStore.getCurrentLocation();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Observer(
      builder: (context) {
        return Column(
          children: [
            Text(
                'Latitude: ${locationStore.hasSelectedPosition ? locationStore.selectedPosition!.latitude.toString() : 'xxx'}'),
            Text(
                'Longtitude: ${locationStore.hasSelectedPosition ? locationStore.selectedPosition!.longitude.toString() : 'xxx'}'),
            ElevatedButton(
                onPressed: () {
                  locationStore.simulateVehicleMovement(
                      vehicleId: vehicleStore.selectedVehicle.id);
                },
                child: const Text('Simulate now')),
            ElevatedButton(
                onPressed: () {
                  locationStore.stopSimulation();
                },
                child: const Text('Stop simulate now'))
          ],
        );
      },
    );
  }
}
