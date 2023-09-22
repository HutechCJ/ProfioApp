namespace Profio.Infrastructure.Abstractions.Hateoas.Hypermedia;

public record Link(string? Href, string? Rel, string? Method);
