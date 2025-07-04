using System.Reflection;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CA.GraphQL.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions options,
        IMediator mediator)
        : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}

// public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
// {
//     public ApplicationDbContext CreateDbContext(string[] args)
//     {
//         // Manually configure the options here
//         var configuration = new ConfigurationBuilder()
//             .SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("appsettings.json", optional: true)
//             .Build();
//
//         var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//
//         var connectionString = configuration.GetConnectionString("graphql-db");
//         optionsBuilder.UseSqlServer(connectionString); // or UseNpgsql, etc.
//
//         return new ApplicationDbContext(
//             optionsBuilder.Options,
//             mediator: null!);
//     }
// }