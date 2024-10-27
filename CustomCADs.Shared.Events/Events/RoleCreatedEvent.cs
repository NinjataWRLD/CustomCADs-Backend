namespace CustomCADs.Shared.Events.Events;

public class RoleCreatedEvent
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}
