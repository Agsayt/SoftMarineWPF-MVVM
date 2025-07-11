using Microsoft.EntityFrameworkCore;
using SoftMarineWPF_MVVM.Core.Context;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Services;

public class RemarkService : DbService, IRemarkService
{
    public RemarkService(CoreContext context) : base(context)
    {
    }

    public async Task<Guid> Create(Remark remark)
    {
        remark.Inspection.RemarkValue = ++remark.Inspection.RemarkValue;

        await Context.AddAsync(remark);

        await Context.SaveChangesAsync();

        return remark.Id;
    }

    public async Task<IEnumerable<Remark>> GetAllRemarks()
    {
        var remarks = await Context.Set<Remark>()
            .Include(x => x.Inspection)
            .ToListAsync();

        return remarks;
    }

    public async Task<IEnumerable<Remark>> GetRemarks(Guid inspectionId)
    {
        var remarks = await Context.Set<Remark>()
            .Include(x => x.Inspection)
            .Where(x => x.Inspection.Id == inspectionId)
            .ToListAsync();

        return remarks;
    }

    public async Task<bool> Remove(Guid id)
    {
        var remarkToRemove = await Context.Set<Remark>()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (remarkToRemove is null) return false;



        var inspection = await Context.Set<Inspection>()
            .Where(x => x.Id == remarkToRemove.Inspection.Id)
            .FirstOrDefaultAsync();

        inspection.RemarkValue = --inspection.RemarkValue;

        Context.Remove(remarkToRemove);
        await Context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(Guid remarkId, Remark remark)
    {
        var remarkToUpdate = await Context.Set<Remark>()
              .Where(x => x.Id == remarkId)
              .FirstOrDefaultAsync();

        if (remarkToUpdate is null) return false;

        // Не лучший, но сейчас быстрый вариант
        try
        {
            remarkToUpdate.Title = remark.Title;
            remarkToUpdate.Commentary = remark.Commentary;
            remarkToUpdate.DateOfElimination = remark.DateOfElimination;
            remarkToUpdate.Type = remark.Type;
        }
        catch (Exception e)
        {
            return false;
            //TODO: подключить сюда логгер
        }

        await Context.SaveChangesAsync();

        return true;
    }
}
