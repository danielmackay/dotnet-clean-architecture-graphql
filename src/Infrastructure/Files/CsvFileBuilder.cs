using System.Globalization;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Application.TodoLists.Queries.ExportTodos;
using CA.GraphQL.Infrastructure.Files.Maps;
using CsvHelper;

namespace CA.GraphQL.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
