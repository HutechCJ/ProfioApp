import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:profio_staff_client/stores/hub_store.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';

class HubInfo extends StatefulWidget {
  const HubInfo({
    Key? key,
  }) : super(key: key);

  @override
  State<HubInfo> createState() => _HubInfoState();
}

class _HubInfoState extends State<HubInfo> {
  late HubStore hubStore;
  late VehicleStore vehicleStore;

  @override
  void initState() {
    hubStore = context.read<HubStore>();
    vehicleStore = context.read<VehicleStore>();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Observer(builder: (context) {
      return Column(
        children: [
          Text(hubStore.hasSelectedHub
              ? hubStore.selectedHub.zipCode
              : 'No Current Hub is selected'),
          ElevatedButton(
              onPressed: () {
                hubStore.getNextHub(vehicleStore.selectedVehicle.id);
              },
              child: const Text('Get next hub'))
        ],
      );
    });
  }
}
