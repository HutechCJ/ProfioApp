// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'hub_store.dart';

// **************************************************************************
// StoreGenerator
// **************************************************************************

// ignore_for_file: non_constant_identifier_names, unnecessary_brace_in_string_interps, unnecessary_lambdas, prefer_expression_function_bodies, lines_longer_than_80_chars, avoid_as, avoid_annotating_with_dynamic, no_leading_underscores_for_local_identifiers

mixin _$HubStore on HubStoreBase, Store {
  Computed<bool>? _$hasSelectedHubComputed;

  @override
  bool get hasSelectedHub =>
      (_$hasSelectedHubComputed ??= Computed<bool>(() => super.hasSelectedHub,
              name: 'HubStoreBase.hasSelectedHub'))
          .value;

  late final _$hubListAtom =
      Atom(name: 'HubStoreBase.hubList', context: context);

  @override
  List<Hub> get hubList {
    _$hubListAtom.reportRead();
    return super.hubList;
  }

  @override
  set hubList(List<Hub> value) {
    _$hubListAtom.reportWrite(value, super.hubList, () {
      super.hubList = value;
    });
  }

  late final _$selectedHubAtom =
      Atom(name: 'HubStoreBase.selectedHub', context: context);

  @override
  Hub get selectedHub {
    _$selectedHubAtom.reportRead();
    return super.selectedHub;
  }

  @override
  set selectedHub(Hub value) {
    _$selectedHubAtom.reportWrite(value, super.selectedHub, () {
      super.selectedHub = value;
    });
  }

  late final _$getNextHubAsyncAction =
      AsyncAction('HubStoreBase.getNextHub', context: context);

  @override
  Future<void> getNextHub(String vehicleId) {
    return _$getNextHubAsyncAction.run(() => super.getNextHub(vehicleId));
  }

  late final _$fetchHubsAsyncAction =
      AsyncAction('HubStoreBase.fetchHubs', context: context);

  @override
  Future<void> fetchHubs() {
    return _$fetchHubsAsyncAction.run(() => super.fetchHubs());
  }

  late final _$HubStoreBaseActionController =
      ActionController(name: 'HubStoreBase', context: context);

  @override
  void setHub(Hub hub) {
    final _$actionInfo =
        _$HubStoreBaseActionController.startAction(name: 'HubStoreBase.setHub');
    try {
      return super.setHub(hub);
    } finally {
      _$HubStoreBaseActionController.endAction(_$actionInfo);
    }
  }

  @override
  String toString() {
    return '''
hubList: ${hubList},
selectedHub: ${selectedHub},
hasSelectedHub: ${hasSelectedHub}
    ''';
  }
}
