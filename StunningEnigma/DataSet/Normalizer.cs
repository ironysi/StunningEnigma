using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSet
{
    class Normalizer
    {
        public double[][] NormalizedData { get; set; }

        public Normalizer(double[][] data)
        {
            NormalizedData = new double[data.Length][];
            double[] col = new double[data.Length];


            for (int i = 0; i < data[0].Length; i++) // number of columns
            {
                for (int j = 0; j < data.Length; j++) // number of rows
                {
                    col[j] = data[j][i];
                }

                NormalizedData[i] = NormalizeDataBetween(col, 1, 0);
            }
        }

        private double[] NormalizeDataBetween(double[] column, double max, double min)
        {
            double maxValueInCol = column.Max();
            double minValueInCol = column.Min();
            double range = maxValueInCol - minValueInCol;

            double[] newColumn = new double[column.Length];

            newColumn = column.Select(d => (d - minValueInCol) / range)
                              .Select(n => ((1 - n) * min + n * max))
                              .ToArray();

            return newColumn;
        }

    }
}
