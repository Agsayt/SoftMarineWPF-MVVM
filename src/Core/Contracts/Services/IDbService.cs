using SoftMarineWPF_MVVM.Core.Context;

namespace SoftMarineWPF_MVVM.Core.Contracts.Services;

public interface IDbService
{
    CoreContext GetContext();
}
