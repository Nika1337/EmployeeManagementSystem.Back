
using Ardalis.Specification;
using Domain.Models;
using Domain.Specifications.Employees.Results;

namespace Domain.Specifications.Employees;

public class EmployeeProfileByIdSpecification : SingleResultSpecification<Employee, EmployeeProfileResult>
{
    public EmployeeProfileByIdSpecification(Guid id)
    {
        Query.Where(empl => empl.Id == id);

        Query.Select(empl => new EmployeeProfileResult
        {
            FirstName = empl.FirstName,
            LastName = empl.LastName,
            Position = empl.Position,
            Department = empl.Department,
            BirthDateUtc = empl.BirthDateUtc,
            StartDateUtc = empl.StartDateUtc
        });

        Query.AsNoTracking();
    }
}
