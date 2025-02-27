

using Application.Abstractions;
using Application.DataTransferObjects.Employees;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Domain.Specifications.Employees;

namespace Infrastructure.Services;

internal class EmployeeService : IEmployeeService
{
    private readonly IRepository<Employee> _repository;

    public EmployeeService(IRepository<Employee> repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeProfileResponse> GetEmployeeProfileAsync(Guid employeeId)
    {
        var specification = new EmployeeProfileByIdSpecification(employeeId);

        var employee = await _repository.SingleOrDefaultAsync(specification) ?? throw new NotFoundException($"Employee with '{employeeId}' not found.");

        var response = new EmployeeProfileResponse
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Department = employee.Department,
            Position = employee.Position,
            StartDateUtc = employee.StartDateUtc,
            BirthDateUtc = employee.BirthDateUtc
        };

        return response;
    }

    public async Task UpdateEmployeeProfileAsync(EmployeeProfileUpdateRequest request)
    {
        var employee = await _repository.GetByIdAsync(request.EmployeeId) ?? throw new NotFoundException($"Employee with '{request.EmployeeId}' not found.");

        employee.UpdateBirthDate(request.BirthDateUtc);
        employee.UpdateFirstName(request.FirstName);
        employee.UpdateLastName(request.LastName);

        await _repository.UpdateAsync(employee);
    }
}
