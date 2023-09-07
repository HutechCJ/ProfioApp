import 'package:flutter/material.dart';
import 'package:profio_staff_client/widgets/mqtt_connection_button.dart';

class HomePage extends StatelessWidget {
  const HomePage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: const Row(children: [
        Expanded(
          child: MQTTConnectionButton(),
        ),
      ]),
    );
  }
}
