using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Contracts.Services;

public interface IRemarkService
{
    Task<Guid> Create(Remark remark);
    Task<bool> Update(Guid remarkId, Remark remark);
    // Я бы лучше не удалял первично, а архивировал запись с флагом удаления
    Task<bool> Remove(Guid id);

    Task<IEnumerable<Remark>> GetAllRemarks();
    Task<IEnumerable<Remark>> GetRemarks(Guid inspectionId);
}
