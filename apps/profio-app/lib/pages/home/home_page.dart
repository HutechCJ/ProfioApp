import 'package:flutter/material.dart';
import 'package:profio_staff_client/widgets/hub_info.dart';
import 'package:profio_staff_client/widgets/mqtt_connection_button.dart';
import 'package:profio_staff_client/widgets/simulate_button.dart';
import 'package:profio_staff_client/widgets/vehicle_list.dart';
import 'package:profio_staff_client/widgets/visit_button.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: const Column(children: [
        Expanded(
          child: MQTTConnectionButton(),
        ),
        VehicleList(),
        HubInfo(),
        VisitButton(),
        SizedBox(
          height: 100,
        ),
        Expanded(child: SimulateButton()),
      ]),
    );
  }
}
