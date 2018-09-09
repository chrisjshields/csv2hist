using System;
using System.Linq;

namespace csv2hist
{
    public class Program
    {
        private const int DEFAULT_BIN_WIDTH = 10;
        private const string DEFAULT_CSV_PATH = "SampleData.csv";

        /// <summary>
        /// Program entry point. From the command line, optionally specify the path to the CSV file and the bin width as shown below
        /// </summary>
        /// <param name="args">csv2hist path binWidth</param>
        private static void Main(string[] args)
        {
            string csvPath = DEFAULT_CSV_PATH;
            int binWidth = DEFAULT_BIN_WIDTH;

            // Use command line arguments if they are specified
            if (args.Length > 0)
                csvPath = args[0];

            if (args.Length > 1)
                int.TryParse(args[1], out binWidth);

            // Open and parse file as CSV
            CsvFile csvFile = new CsvFile(csvPath);

            // Display mean and SD rounded to 3 d.p.
            Console.WriteLine($"Parsed {csvFile.Values.Length} values.");
            Console.WriteLine($"Mean: {csvFile.Values.CalculateMean():##.###}");
            Console.WriteLine($"SD:   {csvFile.Values.CalculateStandardDeviation():##.###}");

            // Calculate the first bin and the number of bins required for the data given the supplied bin width
            int binStart = (int) Math.Floor(csvFile.Values.Min() / binWidth) * binWidth;
            int binCount = (int) Math.Ceiling(csvFile.Values.Max() - binStart) / binWidth;

            // Generate histogram
            Histogram histogram = new Histogram(binStart, binWidth, binCount);
            bool success = histogram.Scatter(csvFile.Values);

            Console.WriteLine("Histogram bins:");
            foreach (Bin bin in histogram.Bins)
            {
                // Display each bin alongside the number of values it contains
                // Note: As per the specification, the bin contains values less than itself e.g. < 10, 10 to < 20 etc. This is different to how Excel calculates bins - the bin contains values up to and including itself e.g. <= 10
                Console.WriteLine($"{bin.Interval,4}: {bin.Count,4}");
            }

            // Display warning if some items were larger than the largest bin. Could be simply listed as More: {histogram.LostAndFound.Count}
            // This should never happen under this usage of the Histogram class because we have calculated the number of bins programatically
            if (!success)
                Console.WriteLine($"Warning: {histogram.LostAndFound.Count} values did not fit into the allocated bins (e.g. they were larger than {histogram.Bins.Last().Interval}).");

            // Pause before exiting in case program is run from a GUI
            Console.ReadLine();
        }
    }
}
