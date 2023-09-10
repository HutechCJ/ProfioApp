import 'dart:math';

import 'package:flutter/material.dart';
import 'package:mqtt_client/mqtt_client.dart';
import 'package:mqtt_client/mqtt_server_client.dart';

class MqttProvider {
  late final MqttServerClient client;

  MqttProvider() {
    client = MqttServerClient('f771154e.ala.us-east-1.emqxsl.com',
        'Profio-${Random().nextInt(1000) + 1}');
    client.secure = true;
    client.logging(on: false);
    client.setProtocolV311();
    client.keepAlivePeriod = 20;
    client.connectTimeoutPeriod = 2000; // milliseconds
    client.port = 8883;
    client.onDisconnected = onDisconnected;
    client.onConnected = onConnected;
    client.onSubscribed = onSubscribed;
    client.pongCallback = pong;
    client.websocketProtocols = MqttClientConstants.protocolsSingleDefault;

    final connMess = MqttConnectMessage()
        .withClientIdentifier('Mqtt_MyClientUniqueId')
        .withWillTopic(
            '/location') // If you set this, you must set a will message
        .withWillMessage('My Will message')
        .startClean() // Non-persistent session for testing
        .withWillQos(MqttQos.atLeastOnce)
        .authenticateAs('root', 'Hutech@123');

    client.connectionMessage = connMess;
  }

  Future<void> connect() async {
    WidgetsFlutterBinding.ensureInitialized();

    try {
      await client.connect();
    } on Exception catch (e) {
      print('MqttProvider::client exception - $e');
      client.disconnect();
      return;
    }

    if (client.connectionStatus!.state == MqttConnectionState.connected) {
      print('MqttProvider::Mosquitto client connected');
    } else {
      print(
          'MqttProvider::ERROR Mosquitto client connection failed - disconnecting, status is ${client.connectionStatus}');
      client.disconnect();
    }
  }

  void disconnect() {
    client.disconnect();
  }

  void subscribe(String topic, MqttQos qos) {
    client.subscribe(topic, qos);
  }

  void unsubscribe(String topic) {
    client.unsubscribe(topic);
  }

  void publish(String topic, MqttQos qos, MqttClientPayloadBuilder builder) {
    client.publishMessage(topic, qos, builder.payload!);
  }

  void onSubscribed(String topic) {
    print('MqttProvider::Subscription confirmed for topic $topic');
  }

  void onDisconnected() {
    print(
        'MqttProvider::OnDisconnected client callback - Client disconnection');
    if (client.connectionStatus!.disconnectionOrigin ==
        MqttDisconnectionOrigin.solicited) {
      print(
          'MqttProvider::OnDisconnected callback is solicited, this is correct');
    }
  }

  void onConnected() {
    print(
        'MqttProvider::OnConnected client callback - Client connection was successful');
  }

  void pong() {
    print('MqttProvider::Ping response client callback invoked');
  }
}
