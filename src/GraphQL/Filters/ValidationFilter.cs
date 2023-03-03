using CA.GraphQL.Application.Common.Exceptions;

namespace GraphQL.Filters;

public class ValidationFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is not null and ValidationException valEx)
        {
            // NOTE: Is there a tidier way to to this?
            var errors = new Dictionary<string, object?>();
            foreach (var key in valEx.Errors.Keys)
                errors.Add(key, valEx.Errors[key]);

            return error
                .WithCode(ErrorCode.ClientError.ToString())
                .WithMessage(valEx.Message)
                .WithExtensions(errors);
        }

        if (error.Exception is not null and NotFoundException nfEx)
        {
            return error
                .RemoveExtensions()
                .WithCode(ErrorCode.NotFound.ToString())
                .WithMessage(nfEx.Message);
        }

        return error;
    }
}

public enum ErrorCode
{
    NoError = 0,
    ClientError = 400,
    NotFound = 404,
    ServerError = 500,
}