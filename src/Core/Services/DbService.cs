using SoftMarineWPF_MVVM.Core.Context;
using SoftMarineWPF_MVVM.Core.Contracts.Services;

namespace SoftMarineWPF_MVVM.Core.Services;

public class DbService: IDbService
{
    public DbService(CoreContext context)
    {
        Context = context;
        Context.Database.EnsureCreated();
    }

    protected CoreContext Context { get; }

    public CoreContext GetContext()
    {
        return Context;
    }
}
