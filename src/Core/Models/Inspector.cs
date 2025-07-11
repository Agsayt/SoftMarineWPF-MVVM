namespace SoftMarineWPF_MVVM.Core.Models;

public class Inspector
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public int UniqueNumber { get; set; }
    public bool IsActive { get; set; }

    public virtual List<Inspection> Inspections { get; set; } = new();


    public override string ToString()
    {
        return $"{LastName} {FirstName[0]}.{(MiddleName is null? "": $" {MiddleName[0]}.")}";
    }

}
