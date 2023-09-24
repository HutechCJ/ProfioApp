namespace Profio.Website.Data.Common;

public record ResultModel<T>(
  T? Data,
  bool IsError = false,
  string? ErrorMessage = default);
