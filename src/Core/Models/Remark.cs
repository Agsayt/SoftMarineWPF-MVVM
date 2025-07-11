using SoftMarineWPF_MVVM.Core.Enums;

namespace SoftMarineWPF_MVVM.Core.Models;

public class Remark
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    // Возможно, лучше стандартизировать типы замечаний для единообразия
    public RemarkType Type { get; set; }
    public string? Commentary { get; set; }
    public DateTime? DateOfElimination { get; set; }
    public Inspection Inspection { get; set; }
}
