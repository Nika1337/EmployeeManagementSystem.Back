

namespace Application.DataTransferObjects.Employees;

public record EmployeeProfileResponse
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Position { get; init; }
    public required string Department { get; init; }
    public required DateTime BirthDateUtc { get; init; }
    public required DateTime StartDateUtc { get; init; }
}
