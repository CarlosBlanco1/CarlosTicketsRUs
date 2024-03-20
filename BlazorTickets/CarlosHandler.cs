partial class CarlosHandler(ILogger<CarlosHandler> logger)
{
    public string HandleRequest1()
    {
        LogHandleRequest1(logger, "This is a log");
        return "Hello World";
    }

    public string HandleRequest2()
    {
        LogHandleRequest2(logger, "This is another log");
        return "Hello World";
    }

    public string HandleRequest3()
    {
        LogHandleRequest3(logger, "xd");
        return "Hello World";
    }

    public string HandleRequest4()
    {
        LogHandleRequest4(logger, "yy");
        return "Hello World";
    }

    public string HandleRequest5()
    {
        LogHandleRequest5(logger, "xx");
        return "Hello World";
    }

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest1 was called with {Description}")]
    public static partial void LogHandleRequest1(ILogger logger, string description);

    [LoggerMessage(LogLevel.Warning, "ExampleHandler.HandleRequest2 was called with {Description}")]
    public static partial void LogHandleRequest2(ILogger logger, string description);

    [LoggerMessage(LogLevel.Debug, "ExampleHandler.HandleRequest3 was called with {Description}")]
    public static partial void LogHandleRequest3(ILogger logger, string description);

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest4 was called with {Description}")]
    public static partial void LogHandleRequest4(ILogger logger, string description);

    [LoggerMessage(LogLevel.Information, "ExampleHandler.HandleRequest5 was called with {Description}")]
    public static partial void LogHandleRequest5(ILogger logger, string description);
}
