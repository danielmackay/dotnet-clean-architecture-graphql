using CA.GraphQL.Application.Common.Exceptions;

namespace GraphQL.Filters;

public class ValidationFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not null and ValidationException ex)
        {
            var errors = new Dictionary<string, object?>();
            foreach (var key in ex.Errors.Keys)
                foreach (var item in ex.Errors[key])
                    errors.Add(key, item);

            return error
                .WithMessage(ex.Message)
                .WithExtensions(errors);
        }

        return error;
    }
}