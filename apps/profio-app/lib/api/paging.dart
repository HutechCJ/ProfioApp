import 'package:json_annotation/json_annotation.dart';

part 'paging.g.dart';

@JsonSerializable()
class Paging {
  int pageIndex;
  int pageSize;
  int count;
  int totalCount;
  int totalPages;
  bool hasPreviousPage;
  bool hasNextPage;
  List<dynamic> items;
  Paging(
      {this.pageIndex = 0,
      this.pageSize = 0,
      this.count = 0,
      this.totalCount = 0,
      this.totalPages = 0,
      this.hasPreviousPage = false,
      this.hasNextPage = false,
      this.items = const []});
  factory Paging.fromJson(Map<String, dynamic> json) => _$PagingFromJson(json);

  Map<String, dynamic> toJson() => _$PagingToJson(this);
}
