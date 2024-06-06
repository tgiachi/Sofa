namespace Sofa.Core.Data.Messages;

public record MediaAddedEvent(string FileName, string Hash)
{
    public override string ToString() => $"FileName: {FileName}, Hash: {Hash}";
}
