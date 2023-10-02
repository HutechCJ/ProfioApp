using System.Reflection;
using Profio.Infrastructure.Persistence;

namespace Profio.Infrastructure;

public static class AssemblyReference
{
  public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
  public static readonly string? AssemblyName = typeof(ApplicationDbContext).Assembly.FullName;
  public static readonly Assembly CallingAssembly = Assembly.GetCallingAssembly();
}
