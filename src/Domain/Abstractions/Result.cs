using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions;

public class Result<T>
{
    private readonly T? _value;
    public Error? Error { get; }

    private Result(T value)
    {
        IsSuccess = true;
        _value = value;
        Error = null;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        _value = default;
        Error = error;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool IsSuccess { get; }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(Error error) => new(error);

    public T Value =>
        IsSuccess ? _value! : throw new InvalidOperationException("Value can not be accessed when IsSuccess is false");

    public static implicit operator Result<T>(T value) => Success(value);

    public static implicit operator Result<T>(Error error) => Failure(error);
}
