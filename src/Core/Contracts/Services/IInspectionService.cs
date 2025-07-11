using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Contracts.Services;

public interface IInspectionService
{
    Task<Guid> Create(Inspection inspection);
    Task<bool> Update(Guid inspectionId, Inspection inspection);
    // Я бы лучше не удалял первично, а архивировал запись с флагом удаления
    Task<bool> Remove(Guid id);



    Task<IEnumerable<Inspection>> GetInspections();
    Task<IEnumerable<Inspection>> GetInspectionsWithRemarks();
    Task<IEnumerable<Inspection>> GetInspectionsByTitle(string title);

}
