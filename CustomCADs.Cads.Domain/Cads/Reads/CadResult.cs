using CustomCADs.Cads.Domain.Cads.Entites;

namespace CustomCADs.Cads.Domain.Cads.Reads;

public record CadResult(int Count, ICollection<Cad> Cads);
