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
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return FutureBuilder(
        future: vehicleStore.fetchVehicles(),
        builder: (context, snappshot) {
          return Observer(builder: (context) {
            if (snappshot.connectionState == ConnectionState.waiting) {
              return const CircularProgressIndicator();
            }
            return Center(
                child: DropdownButton<Vehicle>(
              value: vehicleStore.selectedVehicle.id == ''
                  ? null
                  : vehicleStore
                      .selectedVehicle, // The currently selected vehicle
              onChanged: (newValue) {
                vehicleStore.setVehicle(newValue!);
              },
              items: vehicleStore.vehicleList.map((Vehicle vehicle) {
                return DropdownMenuItem<Vehicle>(
                  value: vehicle,
                  child: Text(vehicle.id), // Display a vehicle property here
                );
              }).toList(),
            ));
          });
        });
  }
}
