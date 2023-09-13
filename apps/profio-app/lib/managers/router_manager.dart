import 'package:flutter/material.dart';
import 'package:profio_staff_client/pages/home/home_page.dart';
import 'package:profio_staff_client/pages/map/map_page.dart';

class RouteManager {
  RouteManager._();

  static String splash = 'splash';
  static String home = 'home';
  static String map = 'map';

  static Map<String, Widget Function(BuildContext context)> routes = {
    home: (context) => const HomePage(),
    map: (context) => const MapPage()
  };
}
