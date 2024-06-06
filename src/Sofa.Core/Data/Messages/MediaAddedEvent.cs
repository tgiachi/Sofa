namespace Sofa.Core.Data.Messages;

public record MediaAddedEvent(string FileName, string Hash, string Extension)
{
    public override string ToString() => $"FileName: {FileName}, Hash: {Hash}";
}
