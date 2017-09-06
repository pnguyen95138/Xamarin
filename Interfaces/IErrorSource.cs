using System;

namespace QuickSilver.Core.Interfaces
{
    public interface IErrorSource
    {
        event EventHandler<ErrorEventArgs> ErrorReported;
    }
}