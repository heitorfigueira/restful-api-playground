using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulApiPlayground.Infrastructure.Presentation.Errors;

public class ErrorException : Exception
{
    private Delegate callback { get; init; }

    public string message { get; init; }

    public ErrorException(string message, Delegate callback)
        : base(message)
    {
        this.callback = callback;
        this.message = message;
    }

    public ErrorException(string message, Delegate callback, Exception inner)
    : base(message, inner)
    {
        this.callback = callback;
        this.message = message;
    }

    public IActionResult Callback()
    {
        return (IActionResult) this.callback.DynamicInvoke()!;
    }
}
