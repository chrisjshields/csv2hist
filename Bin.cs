using System.Collections.Generic;

namespace csv2hist
{
    /// <summary>
    /// Bin for storing sorted values
    /// </summary>
    public class Bin
    {
        public int Interval { get; }
        public int Width { get; }
        public List<double> Values { get; }
        public int Count => Values.Count;

        /// <summary>
        /// Create bin with specified interval and width
        /// </summary>
        /// <param name="interval">Upper boundary of bin</param>
        /// <param name="width">Width of bin</param>
        public Bin(int interval, int width)
        {
            Interval = interval;
            Width = width;
            Values = new List<double>();
        }

        /// <summary>
        /// Allow bin of unspecified interval and width for items which don't fit in any other bin
        /// </summary>
        public Bin()
        {
            Interval = -1;
            Width = -1;
            Values = new List<double>();
        }
    }
}
