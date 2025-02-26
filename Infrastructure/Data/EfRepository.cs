using Ardalis.Specification.EntityFrameworkCore;
using Domain.Abstractions;

namespace Infrastructure.Data;

internal class EfRepository<T>(DomainDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T> where T : class
{

}

