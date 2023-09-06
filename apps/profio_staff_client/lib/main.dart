import 'dart:async';
import 'dart:convert';
import 'dart:math';

import 'package:flutter/material.dart';
import 'package:geocoding/geocoding.dart';
import 'package:geolocator/geolocator.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:mqtt_client/mqtt_server_client.dart';
import 'package:profio_staff_client/generators/random_location_generator.dart';
import 'package:profio_staff_client/managers/location_manager.dart';
import 'package:profio_staff_client/models/vehicle_location.dart';
import 'package:profio_staff_client/providers/mqtt_provider.dart';

final client = MqttServerClient('f771154e.ala.us-east-1.emqxsl.com',
    'Profio-${Random().nextInt(1000) + 1}');
Future<int> main() async {
  final mqttProvider = MqttProvider();

  WidgetsFlutterBinding.ensureInitialized();

  await mqttProvider.connect();

  if (mqttProvider.client.connectionStatus!.state ==
      MqttConnectionState.connected) {
    print('MqttProvider::Mosquitto client connected');
  } else {
    print('MqttProvider::ERROR Mosquitto client connection failed');
    return -1;
  }

  const topic = '/location';
  mqttProvider.subscribe(topic, MqttQos.atMostOnce);

  mqttProvider.client.updates!
      .listen((List<MqttReceivedMessage<MqttMessage?>>? c) {
    final recMess = c![0].payload as MqttPublishMessage;
    final pt =
        MqttPublishPayload.bytesToStringAsString(recMess.payload.message);

    print(
        'MqttProvider::Change notification:: topic is <${c[0].topic}>, payload is <-- $pt -->');
    print('');
  });

  mqttProvider.client.published!.listen((MqttPublishMessage message) {
    print(
        'MqttProvider::Published notification:: topic is ${message.variableHeader!.topicName}, with Qos ${message.header!.qos}');
  });

  // const pubTopic = '/location';
  // final builder = MqttClientPayloadBuilder();
  // final position = await LocationManager.getPosition();
  // final location = VehicleLocation(
  //     id: "1234567890",
  //     latitude: position.latitude,
  //     longitude: position.longitude);
  // builder.addString(jsonEncode(location.toJson()));
  final Position currentPosition = await LocationManager.getPosition();

  LocationManager.simulateCarMovement(
      mqttProvider,
      currentPosition,
      RandomLocationGenerator.generatePositionNearby(currentPosition, 1000),
      20);

  // void obtainAndPublishLocation() async {
  //   try {
  //     final position = await LocationManager.getPosition();
  //     await LocationManager.publishLocation(mqttProvider, position);
  //   } catch (e) {
  //     print('Error obtaining location: $e');
  //   }
  // }
  // // Start a periodic timer to call the function every 1 second
  // final locationUpdateTimer = Timer.periodic(Duration(seconds: 1), (_) {
  //   obtainAndPublishLocation();
  // });

  // print('MqttProvider::Publishing our topic');
  // mqttProvider.publish(pubTopic, MqttQos.exactlyOnce, builder);

  // print('MqttProvider::Sleeping....');
  // await MqttUtilities.asyncSleep(60);

  // print('MqttProvider::Unsubscribing');
  // mqttProvider.unsubscribe(topic);

  // await MqttUtilities.asyncSleep(2);
  // print('MqttProvider::Disconnecting');
  // mqttProvider.disconnect();

  // runApp(const MyApp());
  return 0;
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a blue toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
        useMaterial3: true,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;
  Position? _position;
  String _address = 'Address';

  void _incrementCounter() {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      _counter++;
    });
  }

  Future<Position> _determinePosition() async {
    bool serviceEnabled;
    LocationPermission permission;

    // Test if location services are enabled.
    serviceEnabled = await Geolocator.isLocationServiceEnabled();
    if (!serviceEnabled) {
      // Location services are not enabled don't continue
      // accessing the position and request users of the
      // App to enable the location services.
      return Future.error('Location services are disabled.');
    }

    permission = await Geolocator.checkPermission();
    if (permission == LocationPermission.denied) {
      permission = await Geolocator.requestPermission();
      if (permission == LocationPermission.denied) {
        // Permissions are denied, next time you could try
        // requesting permissions again (this is also where
        // Android's shouldShowRequestPermissionRationale
        // returned true. According to Android guidelines
        // your App should show an explanatory UI now.
        return Future.error('Location permissions are denied');
      }
    }

    if (permission == LocationPermission.deniedForever) {
      // Permissions are denied forever, handle appropriately.
      return Future.error(
          'Location permissions are permanently denied, we cannot request permissions.');
    }

    // When we reach here, permissions are granted and we can
    // continue accessing the position of the device.
    return await Geolocator.getCurrentPosition();
  }

  _getAddressFromCoordinates() async {
    try {
      List<Placemark> placemarks = await placemarkFromCoordinates(
          _position!.latitude, _position!.longitude);

      Placemark place = placemarks[0];

      setState(() {
        _address = '${place.locality}, ${place.country}';
      });
    } catch (e) {
      print(e);
    }
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((timeStamp) async {
      setState(() async {
        _position = await _determinePosition();
        // await _getAddressFromCoordinates();
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // TRY THIS: Try changing the color here to a specific color (to
        // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
        // change color while the other colors stay the same.
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: Center(
        // Center is a layout widget. It takes a single child and positions it
        // in the middle of the parent.
        child: Column(
          // Column is also a layout widget. It takes a list of children and
          // arranges them vertically. By default, it sizes itself to fit its
          // children horizontally, and tries to be as tall as its parent.
          //
          // Column has various properties to control how it sizes itself and
          // how it positions its children. Here we use mainAxisAlignment to
          // center the children vertically; the main axis here is the vertical
          // axis because Columns are vertical (the cross axis would be
          // horizontal).
          //
          // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
          // action in the IDE, or press "p" in the console), to see the
          // wireframe for each widget.
          mainAxisAlignment: MainAxisAlignment.center,
          children: <Widget>[
            const Text(
              'You have pushed the button this many times:',
            ),
            Text(
              '$_counter',
              style: Theme.of(context).textTheme.headlineMedium,
            ),
            Text('Address: $_address'),
            Text(
                'Current Position: ${_position?.latitude} - ${_position?.longitude}'),
            ElevatedButton(
                onPressed: () async {
                  await _getAddressFromCoordinates();
                },
                child: const Text('Get location'))
          ],
        ),
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }
}
