namespace Profio.Website.Data.Common;

public record PagedListDto<TItem>(IList<TItem> Items);
