import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:profio_staff_client/stores/location_store.dart';
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

  @override
  void initState() {
    locationStore = context.read<LocationStore>();
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
                  locationStore.simulateVehicleMovement();
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
