using System.ComponentModel.DataAnnotations;

namespace Sofa.Engine.Requests;

public class AddMediaPathRequest
{
    [Required] public string Name { get; set; }

    [Required] public string Path { get; set; }

    [Required] public bool AutoScan { get; set; }
}
