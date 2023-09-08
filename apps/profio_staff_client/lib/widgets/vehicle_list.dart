import 'package:flutter/material.dart';
import 'package:profio_staff_client/api/base_api.dart';
import 'package:profio_staff_client/api/paging.dart';
import 'package:profio_staff_client/api/result_model.dart';
import 'package:profio_staff_client/models/vehicle.dart';

class VehicleList extends StatefulWidget {
  const VehicleList({
    Key? key,
  }) : super(key: key);

  @override
  State<VehicleList> createState() => _VehicleListState();
}

class _VehicleListState extends State<VehicleList> {
  List<Vehicle> _vehicles = [];
  Vehicle? _selectedVehicle;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();

    var baseAPI = BaseAPI();
    baseAPI
        .fetchData('https://profio-sv1.azurewebsites.net/api/v1/vehicles')
        .then((data) {
      var result = ResultModel.fromJson(data.object);
      var paging = Paging.fromJson(result.data);
      var vehicles =
          paging.items.map((item) => Vehicle.fromJson(item)).toList();
      setState(() {
        _vehicles = vehicles;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Center(
        child: DropdownButton<Vehicle>(
      value: _selectedVehicle, // The currently selected vehicle
      onChanged: (newValue) {
        setState(() {
          _selectedVehicle = newValue; // Update the selected vehicle
        });
      },
      items: _vehicles.map((Vehicle vehicle) {
        return DropdownMenuItem<Vehicle>(
          value: vehicle,
          child: Text(vehicle.id), // Display a vehicle property here
        );
      }).toList(),
    ));
  }
}
