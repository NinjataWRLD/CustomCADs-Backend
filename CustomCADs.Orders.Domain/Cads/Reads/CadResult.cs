using CustomCADs.Orders.Domain.Cads.Entites;

namespace CustomCADs.Orders.Domain.Cads.Reads;

public record CadResult(int Count, ICollection<Cad> Cads);
