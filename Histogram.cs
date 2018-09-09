using System.Diagnostics;

namespace csv2hist
{
    /// <summary>
    /// Provides functionality for generating bins and for sorting values into them
    /// </summary>
    public class Histogram
    {
        public Bin[] Bins { get; }
        public Bin LostAndFound { get; }

        /// <summary>
        /// Generate bins
        /// </summary>
        /// <param name="binStart">Lower boundary of first bin</param>
        /// <param name="binCount">Number of bins to generate</param>
        /// <param name="binWidth">Width of each bin</param>
        public Histogram(int binStart, int binWidth, int binCount)
        {
            Bins = new Bin[binCount];
            LostAndFound = new Bin();

            int interval = binStart;
            for (int i = 0; i < binCount; i++)
            {
                interval += binWidth;
                Bins[i] = new Bin(interval, binWidth);
            }
        }

        /// <summary>
        /// Sort values in to their appropriate bins
        /// </summary>
        /// <param name="values"></param>
        /// <returns>True on success, false if there are items in LostAndFound</returns>
        public bool Scatter(double[] values)
        {
            bool success = true;

            foreach (double value in values)
            {
                if (!Put(value))
                    success = false; // At least one value does not fit into the defined bins
            }

            return success;
        }

        /// <summary>
        /// Place value in the appropriate bin
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>True on success, false if item placed in LostAndFound</returns>
        public bool Put(double value)
        {
            foreach (Bin bin in Bins)
            {
                if (value < bin.Interval) // Or value <= bin.Interval - this is how Excel places the values into bins
                {
                    bin.Values.Add(value);
                    return true;
                }
            }

            LostAndFound.Values.Add(value);

            return false; // Does not fit into any of the defined bins
        }
    }
}
