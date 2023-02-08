using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsaftware.Shared.Extensions;
using Elsaftware.Shared.Wrapper;

namespace Shared.Wrapper;

public class Result : IResult
{
    public Result() { }

    public List<string> Messages { get; set; } = new List<string>();

    public bool Succeeded { get; set; }

    public static IResult Failure()
    {
        return new Result { Succeeded = false };
    }

    public static IResult Failure(string message)
    {
        return new Result
        {
            Succeeded = false,
            Messages = new List<string> { message }
        };
    }

    public static IResult Failure(List<string> messages)
    {
        return new Result { Succeeded = false, Messages = messages };
    }

    public static Task<IResult> FailureAsync()
    {
        return Task.FromResult(Failure());
    }

    public static Task<IResult> FailureAsync(string message)
    {
        return Task.FromResult(Failure(message));
    }

    public static Task<IResult> FailureAsync(List<string> messages)
    {
        return Task.FromResult(Failure(messages));
    }

    public static IResult Success()
    {
        return new Result { Succeeded = true };
    }

    public static IResult Success(string message)
    {
        return new Result
        {
            Succeeded = true,
            Messages = new List<string> { message }
        };
    }

    public static Task<IResult> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<IResult> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static async Task<IResult> TryCatch(Func<Task<Result>> func)
    {
        try
        {
            return await func.Invoke();
        }
        catch (System.Exception e)
        {
            return Failure(e.GetMessages());
        }
    }

    public static IResult TryCatch(Func<Result> func)
    {
        try
        {
            return func.Invoke();
        }
        catch (System.Exception e)
        {
            if (e.InnerException == null)
                return Failure(e.Message);

            return Failure(new List<string> { e.Message, e.InnerException.Message });
        }
    }
}

public class Result<T> : Result, IResult<T>
{
    public Result() { }

    public T Data { get; set; } = default!;

    public new static Result<T> Failure()
    {
        return new Result<T> { Succeeded = false };
    }

    public new static Result<T> Failure(string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            Messages = new List<string> { message }
        };
    }

    public new static Result<T> Failure(List<string> messages)
    {
        return new Result<T> { Succeeded = false, Messages = messages };
    }

    public new static Task<Result<T>> FailureAsync()
    {
        return Task.FromResult(Failure());
    }

    public new static Task<Result<T>> FailureAsync(string message)
    {
        return Task.FromResult(Failure(message));
    }

    public new static Task<Result<T>> FailureAsync(List<string> messages)
    {
        return Task.FromResult(Failure(messages));
    }

    public new static Result<T> Success()
    {
        return new Result<T> { Succeeded = true };
    }

    public new static Result<T> Success(string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Messages = new List<string> { message }
        };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T> { Succeeded = true, Data = data };
    }

    public static Result<T> Success(T data, string message)
    {
        return new Result<T>
        {
            Succeeded = true,
            Data = data,
            Messages = new List<string> { message }
        };
    }

    public static Result<T> Success(T data, List<string> messages)
    {
        return new Result<T>
        {
            Succeeded = true,
            Data = data,
            Messages = messages
        };
    }

    public new static Task<Result<T>> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public new static Task<Result<T>> SuccessAsync(string message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<Result<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<Result<T>> SuccessAsync(T data, string message)
    {
        return Task.FromResult(Success(data, message));
    }

    public static async Task<IResult<T>> TryCatch(Func<Task<Result<T>>> func)
    {
        try
        {
            return await func.Invoke();
        }
        catch (Exception e)
        {
            return Failure(e.GetMessages().ToList());
        }
    }
    public static IResult<T> TryCatch(Func<Result<T>> func)
    {
        try
        {
            return func.Invoke();
        }
        catch (Exception e)
        {
            return Failure(e.GetMessages().ToList());
        }
    }
}
