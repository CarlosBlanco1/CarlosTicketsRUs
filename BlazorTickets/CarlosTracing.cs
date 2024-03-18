using System.Diagnostics;

public static class CarlosTracing
{
    public static string traceName = "This is a Carlos Trace";
    public static string traceName2 = "This is a Carlos Trace 2";
    public static readonly ActivitySource MyActivitySource = new(traceName);
    public static readonly ActivitySource MyActivitySource2 = new(traceName2);

}
