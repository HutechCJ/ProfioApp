using Profio.Infrastructure.Persistence;
using System.Reflection;

namespace Profio.Infrastructure;

public static class AssemblyReference
{
  public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
  public static readonly string? AssemblyName = typeof(ApplicationDbContext).Assembly.FullName;
}
