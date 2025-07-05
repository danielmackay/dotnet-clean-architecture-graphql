using CA.GraphQL.Domain.Entities;
using CA.GraphQL.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MigrationService.Initialisers;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.AddRange(
                new TodoList
                {
                    Title = "Todo List",
                    Items =
                    {
                        new TodoItem { Title = "Make a todo list 📃" },
                        new TodoItem { Title = "Check off the first item ✅" },
                        new TodoItem { Title = "Realise you've already done two things on the list! 🤯" },
                        new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                    }
                },
                new TodoList
                {
                    Title = "Shopping List",
                    Items =
                    {
                        new TodoItem { Title = "Buy milk 🥛" },
                        new TodoItem { Title = "Get bread 🍞" },
                        new TodoItem { Title = "Pick up eggs 🥚" },
                        new TodoItem { Title = "Grab some fruit 🍎" },
                    }
                },
                new TodoList
                {
                    Title = "Work Tasks",
                    Items =
                    {
                        new TodoItem { Title = "Check emails 📧" },
                        new TodoItem { Title = "Attend standup meeting 👥" },
                        new TodoItem { Title = "Review pull requests 🔍" },
                        new TodoItem { Title = "Plan next sprint 🏃‍♂️" },
                    }
                }
            );

            await _context.SaveChangesAsync();
        }
    }
}