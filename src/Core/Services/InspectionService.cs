using Microsoft.EntityFrameworkCore;
using SoftMarineWPF_MVVM.Core.Context;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.Core.Services
{
    public class InspectionService : DbService, IInspectionService
    {
        public InspectionService(CoreContext context) : base(context)
        {
        }

        public async Task<Guid> Create(Inspection inspection)
        {
            await Context.AddAsync(inspection);

            await Context.SaveChangesAsync();

            return inspection.Id;
        }

        public async Task<IEnumerable<Inspection>> GetInspections()
        {
            var inspections = await Context.Set<Inspection>().ToListAsync();

            return inspections;
        }

        public async Task<IEnumerable<Inspection>> GetInspectionsByTitle(string title)
        {
            var inspections = await Context.Set<Inspection>()
                .Where(x => x.Name.Contains(title))
                .ToListAsync();

            return inspections;
        }

        public async Task<IEnumerable<Inspection>> GetInspectionsWithRemarks()
        {
            var inspections = await Context.Set<Inspection>()
                .Include(x => x.Remarks)
                .ToListAsync();

            return inspections;
        }

        public async Task<bool> Remove(Guid id)
        {
            var inspectionToRemove = await Context.Set<Inspection>()
                .Where(x => x.Id == id)
                .Include(x => x.Remarks)
                .FirstOrDefaultAsync();

            if (inspectionToRemove is null) return false;

            Context.Remove(inspectionToRemove);
            await Context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Guid inspectionId, Inspection inspection)
        {
            var inspectionToUpdate = await Context.Set<Inspection>()
                .Where(x => x.Id == inspectionId)
                .FirstOrDefaultAsync();

            if(inspectionToUpdate is null) return false;

            // Не лучший, но сейчас быстрый вариант
            try
            {
                inspectionToUpdate.RemarkValue = inspection.RemarkValue;
                inspectionToUpdate.Commentary = inspection.Commentary;
                inspectionToUpdate.Name = inspection.Name;
                inspectionToUpdate.Date = inspection.Date;
                inspectionToUpdate.Type = inspection.Type;

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
}
