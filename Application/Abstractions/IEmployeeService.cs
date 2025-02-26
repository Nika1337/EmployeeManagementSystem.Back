

using Application.DataTransferObjects.Employees;

namespace Application.Abstractions;

public interface IEmployeeService
{
    Task<EmployeeProfileResponse> GetEmployeeProfileAsync(Guid employeeId);
    Task UpdateEmployeeProfileAsync(EmployeeProfileUpdateRequest request);
}
