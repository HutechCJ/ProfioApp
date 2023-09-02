using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Profio.Infrastructure.Searching.Lucene.Internal;
using Profio.Infrastructure.Searching.Lucene;

namespace Profio.Infrastructure.Searching;

public static class Extension
{
  public static WebApplicationBuilder AddLuceneSearch(this WebApplicationBuilder builder)
  {
    builder.Services.AddSingleton(typeof(ILuceneService<>), typeof(LuceneService<>));
    return builder;
  }
}
