using System;

namespace BlazorDialogService
{
    public record DialogResult
    {
        public object Result { get; init; }
        public bool Success { get; init; }
        public Exception Exception { get; init; }
    }
}