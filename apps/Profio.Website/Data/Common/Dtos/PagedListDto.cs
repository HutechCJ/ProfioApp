namespace Profio.Website.Data.Common.Dtos;

public record PagedListDto<TItem>(IList<TItem> Items);
