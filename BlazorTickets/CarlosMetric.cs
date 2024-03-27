using System.Diagnostics.Metrics;
using Microsoft.VisualBasic;
using OpenTelemetry.Metrics;
public class CarlosMetric
{
    public static string MetricName = "Carlos.Meter";
    static Random carlosRandom = new Random();
    int valueToObserve = 0;
    int valueForGaugeToObserve = 0;
    int valueForObservableUpDownCounterToObserve = 0;
    Counter<int>? count;
    UpDownCounter<int>? upDownCounter;
    ObservableCounter<int>? observableCounter;
    ObservableGauge<int>? observableGauge;
    ObservableUpDownCounter<int>? observableUpDownCounter;
    Histogram<int>? histogram;

    public CarlosMetric(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MetricName);
        count = meter.CreateCounter<int>("carlos.randomcounter");
        upDownCounter = meter.CreateUpDownCounter<int>("carlos.randomupdowncounter");
        observableCounter = meter.CreateObservableCounter<int>("carlos.randomobservablecounter", () => valueToObserve);
        observableGauge = meter.CreateObservableGauge<int>("carlos.randomobservablegauge", () => valueForGaugeToObserve);
        observableUpDownCounter = meter.CreateObservableUpDownCounter<int>("carlos.randomobservableupdowncounter", () => valueForObservableUpDownCounterToObserve);
        histogram = meter.CreateHistogram<int>("carlos.randomhistogram");
    }
    public string triggerMetrics()
    {
        count?.Add(4);
        upDownCounter?.Add(-5);
        valueToObserve += 7;
        valueForGaugeToObserve += 10;
        valueForObservableUpDownCounterToObserve += 13;
        histogram?.Record(carlosRandom.Next(1, 11));

        return @$"
        Current counter value: {count}
        Current updowncounter value: {upDownCounter}
        Current value being observed: {observableCounter?.Name}
        Current value being observed by Gauge: {observableGauge?.Name}
        Current value being observed by UpDownCOunter: {observableUpDownCounter?.Name}
        Current value histogram: {histogram?.Name}
        ";
    }
}
