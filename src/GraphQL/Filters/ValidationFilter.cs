using CA.GraphQL.Application.Common.Exceptions;

namespace GraphQL.Filters;

public class ValidationFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not null and ValidationException ex)
        {
            // NOTE: Is there a tidier way to to this?
            var errors = new Dictionary<string, object?>();
            foreach (var key in ex.Errors.Keys)
                errors.Add(key, ex.Errors[key]);

            return error
                .WithMessage(ex.Message)
                .WithExtensions(errors);
        }

        return error;
    }
}