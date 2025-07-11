using Microsoft.EntityFrameworkCore;
using SoftMarineWPF_MVVM.Core.Context;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Services;

public class InspectorService : DbService, IInspectorService
{
    public InspectorService(CoreContext context) : base(context)
    {
    }

    public async Task<Guid> Create(Inspector inspector)
    {
        await Context.AddAsync(inspector);

        try
        {
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            // TODO: Логгер сюда
            return Guid.Empty;
        }

        return inspector.Id;
    }

    public async Task<Inspector?> Get(Guid inspectorId)
    {
        var inspector = await Context.Set<Inspector>()
            .Where(x => x.Id == inspectorId)
            .FirstOrDefaultAsync();

        if (inspector is null) return null;

        return inspector;
    }

    public async Task<IEnumerable<Inspector>> GetAll(bool isActive = true)
    {
        var inspectors = await Context.Set<Inspector>()
            .Where(x => x.IsActive == isActive)
            .ToListAsync();

        return inspectors;
    }

    public async Task<bool> Remove(Guid inspectorId)
    {
        // Какой-то прикол/баг SQlite, из-за которого при фильтрации PK поля вида Guid он некорректно отрабатывает.
        // В другом проекте не имел таких проблем...
        var inspectorToRemove = await Context.Set<Inspector>()
                .Where(x => x.Id.ToString().Equals(inspectorId.ToString()))
                .FirstOrDefaultAsync();

        if (inspectorToRemove is null) return false;

        Context.Remove(inspectorToRemove);

        var changes = Context.ChangeTracker.Entries().ToList();

        try
        {
            Context.SaveChanges();

        }
        catch (Exception e)
        {
            // TODO: добавить сюда логгер
            return false;
        }

        return true;
    }

    public async Task<bool> Deactivate(Guid inspectorId)
    {
        // Какой-то прикол/баг SQlite, из-за которого при фильтрации PK поля вида Guid он некорректно отрабатывает.
        // В другом проекте не имел таких проблем...
        var inspectorToDeactivate = await Context.Set<Inspector>()
                .Where(x => x.Id.ToString().Equals(inspectorId.ToString()))
                .FirstOrDefaultAsync();

        if (inspectorToDeactivate is null) return false;

        inspectorToDeactivate.IsActive = false;

        try
        {
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            //TODO: Добавить сюда логгер
            return false;
        }

        return true;
    }

    public async Task<bool> Update(Guid inspectorId, Inspector updatedInspector)
    {
        var inspectorToUpdate = await Context.Set<Inspector>()
               .Where(x => x.Id.ToString().Equals(inspectorId.ToString()))
               .FirstOrDefaultAsync();

        if (inspectorToUpdate is null) return false;

        inspectorToUpdate.UniqueNumber = updatedInspector.UniqueNumber;
        inspectorToUpdate.FirstName = inspectorToUpdate.FirstName;
        inspectorToUpdate.LastName = inspectorToUpdate.LastName;
        inspectorToUpdate.MiddleName = inspectorToUpdate.MiddleName;
        // Не лучший, но сейчас быстрый вариант
        try
        {
            Context.Update(inspectorToUpdate);
            await Context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return false;
            //TODO: подключить сюда логгер
        }


        return true;
    }
}
