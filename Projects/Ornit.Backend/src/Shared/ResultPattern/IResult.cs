namespace NucleusResults.Core
{
    public interface IResult
    {
        bool IsError { get; }
        Error? Error { get; }
    }

    public interface IResult<T> : IResult
    {
        T Data { get; }
    }
}