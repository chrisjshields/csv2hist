using System;
using System.IO;

namespace csv2hist
{
    /// <summary>
    /// Provides functionality to open and parse files in CSV format. Values are accessible as an array of doubles
    /// </summary>
    public class CsvFile
    {
        public double[] Values { get; }

        /// <summary>
        /// Open CSV file and seperate its values
        /// </summary>
        /// <param name="path">Path to CSV file</param>
        public CsvFile(string path)
        {
            string text;

            // Attempt to read all file text as one long string
            try
            {
                text = File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                throw new CsvFileParseException($"Could not open file '{path}': {ex.Message}", ex);
            }

            // Split the string. Do not remove empty entries (e.g. be strict - instead generate an error)
            string[] csv = text.Split(new char[] {','}, StringSplitOptions.None);

            // Create simple array of doubles (no need for List as we already know the array length and it will not change)
            Values = new double[csv.Length];

            // For each value, parse as a double and add to the array. Throw exception on parse failure
            for (int i = 0; i < csv.Length; i++)
            {
                bool result = double.TryParse(csv[i], out double value);

                if (!result)
                    throw new CsvFileParseException($"Could not parse value '{csv[i]}' in " + path);

                Values[i] = value;
            }
        }
    }

    /// <summary>
    /// Exception generated when a parsing error occurs
    /// </summary>
    public class CsvFileParseException : Exception
    {
        public CsvFileParseException(string message)
            : base(message)
        {
        }

        public CsvFileParseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
