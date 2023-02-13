using CA.GraphQL.Application.Common.Interfaces;

namespace CA.GraphQL.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
