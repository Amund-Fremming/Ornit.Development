namespace Ornit.Backend.src.Shared.ResultPattern;

/// <summary>
/// Used for expressing a error result.
/// </summary>
/// <param name="Exception">That was thrown.</param>
/// <param name="Message">To display to end user.</param>
public record Error(string Message, Exception? Exception = null);