using System;

namespace csv2hist
{
    /// <summary>
    /// I have decided to supply the statistic functions as extensions to arrays of doubles
    /// </summary>
    public static class DoubleArrayExtensions
    {
        /// <summary>
        /// Calculate mean average value from an array of values
        /// </summary>
        /// <param name="values">(Extension to an array of double precision floating point variables)</param>
        /// <returns>Mean</returns>
        public static double CalculateMean(this double[] values)
        {
            double total = 0;

            // Sum the values. Can also be accomplished using Linq: values.Sum()
            foreach (double value in values)
            {
                total += value;
            }

            // Return average
            return total / values.Length;
        }

        /// <summary>
        /// Manually calculate standard deviation from an array of values
        /// </summary>
        /// <param name="values">(Extension to an array of double precision floating point variables)</param>
        /// <returns>Standard deviation</returns>
        public static double CalculateStandardDeviation(this double[] values)
        {
            double total = 0;
            double mean = values.CalculateMean();

            // For each value
            foreach (double value in values)
            {
                // Calculate difference from mean
                double difference = value - mean;

                // Square the difference
                double squaredDifference = difference * difference;

                // Add to the total squared differences
                total += squaredDifference;
            }

            // Calculate the mean of the squared differences
            double meanSquaredDifference = total / values.Length;

            // Return the square root of the mean of the squared differences
            return Math.Sqrt(meanSquaredDifference);
        }
    }
}
