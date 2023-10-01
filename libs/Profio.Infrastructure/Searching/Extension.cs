using Profio.Infrastructure.Searching.Lucene;
using Profio.Infrastructure.Searching.Lucene.Internal;

namespace Profio.Infrastructure.Searching;

public static class Extension
{
  public static WebApplicationBuilder AddLuceneSearch(this WebApplicationBuilder builder)
  {
    builder.Services.AddSingleton(typeof(ILuceneService<>), typeof(LuceneService<>));
    return builder;
  }
}
