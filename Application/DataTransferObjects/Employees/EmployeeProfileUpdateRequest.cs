

namespace Application.DataTransferObjects.Employees;

public record EmployeeProfileUpdateRequest
{
    public required Guid EmployeeId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime BirthDateUtc { get; init; }
}
