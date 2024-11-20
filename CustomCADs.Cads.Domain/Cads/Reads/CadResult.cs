namespace CustomCADs.Cads.Domain.Cads.Reads;

public record CadResult(int Count, ICollection<Cad> Cads);
