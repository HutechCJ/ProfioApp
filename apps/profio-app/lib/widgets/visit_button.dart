import 'package:flutter/material.dart';
import 'package:flutter_mobx/flutter_mobx.dart';
import 'package:profio_staff_client/stores/vehicle_store.dart';
import 'package:provider/provider.dart';

class VisitButton extends StatefulWidget {
  const VisitButton({
    Key? key,
  }) : super(key: key);

  @override
  State<VisitButton> createState() => _VisitButtonState();
}

class _VisitButtonState extends State<VisitButton> {
  late VehicleStore vehicleStore;

  @override
  void initState() {
    vehicleStore = context.read<VehicleStore>();
    vehicleStore.onInit(context);
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Observer(builder: (context) {
      return Column(
        children: [
          ElevatedButton(
              onPressed: () {
                vehicleStore.visit();
              },
              child: const Text('Visit hub'))
        ],
      );
    });
  }
}
