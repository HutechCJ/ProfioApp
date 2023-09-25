import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:profio_staff_client/models/vehicle.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';

class VehicleList extends StatefulWidget {
  const VehicleList({
    Key? key,
  }) : super(key: key);

  @override
  State<VehicleList> createState() => _VehicleListState();
}

class _VehicleListState extends State<VehicleList> {
  late VehicleStore vehicleStore;

  @override
  void initState() {
    vehicleStore = context.read<VehicleStore>();
    vehicleStore.fetchVehicles();
    super.initState();
  }

  @override
  void dispose() {
    vehicleStore.onDispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Observer(builder: (context) {
        return DropdownButton<Vehicle>(
          value: vehicleStore.hasSelectedVehicle
              ? vehicleStore.selectedVehicle
              : null, // The currently selected vehicle
          onChanged: (newValue) async {
            vehicleStore.setVehicle(newValue!);
            await vehicleStore.fetchHubPath();
          },
          items: vehicleStore.vehicleList.map((Vehicle vehicle) {
            return DropdownMenuItem<Vehicle>(
              value: vehicle,
              child: Text(vehicle.id), // Display a vehicle property here
            );
          }).toList(),
        );
      }),
    );
  }
}
