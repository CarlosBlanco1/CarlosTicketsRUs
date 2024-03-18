using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using OpenTelemetry.Metrics;
public static class CarlosMetric
{
    public static string MetricName = "Carlos Metric xd";
    public static Meter greeterMeter = new Meter("OtPrGrYa.Example", "1.0.0");
    static Counter<int> count = greeterMeter.CreateCounter<int>(MetricName, description: "Counts the number of greetings");
}