using System.Reflection;

namespace Profio.Application;

public static class AssemblyReference
{
  public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
  public static readonly Assembly[] AppDomainAssembly = AppDomain.CurrentDomain.GetAssemblies();
}
