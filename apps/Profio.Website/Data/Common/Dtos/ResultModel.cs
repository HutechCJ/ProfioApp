namespace Profio.Website.Data.Common.Dtos;

public record ResultModel<T>(
  T? Data,
  bool IsError = false,
  string? ErrorMessage = default);
