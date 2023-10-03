import 'dart:io';
import 'dart:developer' as developer;

import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:dio/dio.dart';

/// ## Api Status
/// * [succeeded] - The request was successful.
/// * [failed] - The request failed.
/// * [internetUnavailable] - The internet is unavailable.
enum ApiStatus { succeeded, failed, internetUnavailable }

/// ## Api Method
/// * [get] - Get method.
/// * [post] - Post method.
/// * [put] - Put method.
/// * [delete] - Delete method.
enum ApiMethod { get, post, put, delete }

/// ## api Method with Map of [ApiMethod, String]
/// * ApiMethod.get - Get method = 'get'
/// * ApiMethod.post - Post method = 'post'
/// * ApiMethod.put - Put method = 'put'
/// * ApiMethod.delete - Delete method = 'delete'
Map<ApiMethod, String> apiMethod = {
  ApiMethod.get: 'get',
  ApiMethod.post: 'post',
  ApiMethod.put: 'put',
  ApiMethod.delete: 'delete'
};

/// ## [BaseDataAPI] - Base Class for handling API
class BaseDataAPI {
  dynamic object;
  dynamic apiStatus;
  BaseDataAPI({this.object, this.apiStatus});
}

void printLogYellow(String message) {
  developer.log('\x1B[33m$message\x1B[0m');
}

void printLogError(String message) {
  developer.log('\x1B[31m$message\x1B[0m');
}

void printLogSusscess(String message) {
  developer.log('\x1B[32m$message\x1B[0m');
}

class BaseAPI {
  /// ## [domain] is domain of API
  static String domain = '';

  /// _dio is instance of dio
  final Dio _dio = Dio();

  /// BaseAPI is instance of BaseAPI
  BaseAPI();

  /// # [fetchData] is fetch data from API
  /// * Param [url] is url of API without domain
  /// * Param [params] is params of API with key and value
  /// * Param [body] is body of API with key and value
  /// * Param [headers] is headers of API with key and value
  /// * Return [BaseDataAPI] is object of BaseDataAPI with object and apiStatus
  /// * Example:
  ///  ```dart
  ///  return BaseDataAPI(object: response.data, apiStatus:ApiStatus.succeeded);
  /// ```
  Future<BaseDataAPI> fetchData(
    url, {
    dynamic body,
    Map<String, dynamic>? params,
    Map<String, dynamic>? headers,
    ApiMethod method = ApiMethod.get,
  }) async {
    /// Check internet connection is available
    /// * If internet connection is not available,
    ///  return [ApiStatus.internetUnavailable]
    /// * If internet connection is available,
    /// continue to fetch data

    if (!(await checkConnection())) {
      return BaseDataAPI(
        apiStatus: ApiStatus.internetUnavailable,
      );
    }

    /// Continue to fetch data
    /// response is response of API
    Response response;
    printLogYellow('API:${apiMethod[method]}|================--------------->');
    developer.log('url: $domain$url');
    developer.log('header: $headers');
    developer.log('params: $params');
    developer.log('body: $body');
    try {
      Options options = Options();
      options.method = apiMethod[method];
      options.headers = headers;
      response = await _dio.request(domain + url,
          data: body, queryParameters: params, options: options);
    } on DioException catch (e) {
      /// If error is DioException, return [ApiStatus.failed]
      printLogError('Error [${apiMethod[method]} API]: $e');
      printLogYellow(
          'END API ${apiMethod[method]}<---------------================|');
      return BaseDataAPI(apiStatus: ApiStatus.failed);
    }
    // If response.data is DioException, return [ApiStatus.failed]
    if (response.data is DioException) {
      printLogError('Error [${apiMethod[method]} API]: ${response.data}');
      printLogYellow('END API get<---------------================|');
      return BaseDataAPI(apiStatus: ApiStatus.failed);
    }
    // If response.data is not null, return [response.data ,ApiStatus.succeeded]
    printLogSusscess('Success [${apiMethod[method]} API]: ${response.data}');
    printLogYellow(
        'END API ${apiMethod[method]}<---------------================|');
    return BaseDataAPI(object: response.data, apiStatus: ApiStatus.succeeded);
  }

  Future<BaseDataAPI> fileUpload(url,
      {dynamic body,
      Map<String, dynamic>? headers,
      ApiMethod method = ApiMethod.post,
      required File file}) async {
    /// Check internet connection is available
    /// * If internet connection is not available,
    ///  return [ApiStatus.internetUnavailable]
    /// * If internet connection is available,
    /// continue to fetch data

    if (!(await checkConnection())) {
      return BaseDataAPI(
        apiStatus: ApiStatus.internetUnavailable,
      );
    }

    /// Continue to fetch data
    /// response is response of API
    Response response;
    printLogYellow('API:${apiMethod[method]}|================--------------->');
    developer.log('url: $domain$url');
    developer.log('header: $headers');
    developer.log('body: $body');
    try {
      Options options = Options();
      options.method = apiMethod[method];
      options.headers = headers;
      String fileName = file.path.split('/').last;
      FormData formData = FormData.fromMap({
        'file': await MultipartFile.fromFile(file.path, filename: fileName),
      });
      response =
          await _dio.request(domain + url, data: formData, options: options);
    } on DioException catch (e) {
      /// If error is DioException, return [ApiStatus.failed]
      printLogError('Error [${apiMethod[method]} API]: $e');
      printLogYellow(
          'END API ${apiMethod[method]}<---------------================|');
      return BaseDataAPI(apiStatus: ApiStatus.failed);
    }
    // If response.data is DioException, return [ApiStatus.failed]
    if (response.data is DioException) {
      printLogError('Error [${apiMethod[method]} API]: ${response.data}');
      printLogYellow('END API get<---------------================|');
      return BaseDataAPI(apiStatus: ApiStatus.failed);
    }
    // If response.data is not null, return [response.data ,ApiStatus.succeeded]
    printLogSusscess('Success [${apiMethod[method]} API]: ${response.data}');
    printLogYellow(
        'END API ${apiMethod[method]}<---------------================|');
    return BaseDataAPI(object: response.data, apiStatus: ApiStatus.succeeded);
  }

  /// # [checkConnection] Check connection internet
  /// * Return true if connection internet
  /// * Return false if not connection internet
  static Future<bool> checkConnection() async {
    final connectivityResult = await Connectivity().checkConnectivity();
    return connectivityResult != ConnectivityResult.none;
  }
}
