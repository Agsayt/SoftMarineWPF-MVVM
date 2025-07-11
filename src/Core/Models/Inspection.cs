
using SoftMarineWPF_MVVM.Core.Enums;

namespace SoftMarineWPF_MVVM.Core.Models;

public class Inspection
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public InspectionType Type { get; set; }
    public DateTime Date { get; set; }
    public Inspector Inspector { get; set; }
    public string? Commentary { get; set; }
    public int RemarkValue { get; set; }
    public List<Remark> Remarks { get; set; } = new();
}
