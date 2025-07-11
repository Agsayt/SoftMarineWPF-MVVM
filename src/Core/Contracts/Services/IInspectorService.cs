using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Contracts.Services;

public interface IInspectorService
{
    Task<Guid> Create(Inspector inspector);
    Task<bool> Update(Guid inspectorId, Inspector updatedInspector);
    Task<bool> Remove(Guid inspectorId);
    Task<IEnumerable<Inspector>> GetAll(bool isActive = true);
    Task<Inspector?> Get(Guid inspectorId);
    Task<bool> Deactivate(Guid inspectorId);
}
