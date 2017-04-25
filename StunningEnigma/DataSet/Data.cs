﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;

namespace DataSet
{
    public class Data
    {
        private Matrix<double> _allData;

        private double _percentage;

        private Matrix<double> _outputs;
        private Matrix<double> LearningOutputs { get; set; }
        private Matrix<double> TestingOutputs { get; set; }

        private Matrix<double> _inputs;
        private Matrix<double> LearningInputs { get; set; }
        private Matrix<double> TestingInputs { get; set; }

        public Standardizer standardizer;
        public double[][] GetLearningOutputs()
        {
            return LearningOutputs.ToRowArrays();
        }

        public double[][] GetLearningInputs()
        {
            return LearningInputs.ToRowArrays();
        }

        public double[][] GetTestingOutputs()
        {
            return TestingOutputs.ToRowArrays();
        }

        public double[][] GetTestingInputs()
        {
            return TestingInputs.ToRowArrays();
        }


        public Data(string fileName, char splitOn, int inputColumnsCount, int outputColumnsCount,
            double percentage, string[] columnTypes, int[] outputColumns = null, int[] ignoredColumns = null)
        {
            _allData = _fillData(columnTypes, fileName, splitOn, outputColumns, ignoredColumns, inputColumnsCount);
            _percentage = percentage;

            _inputs = _allData.SubMatrix(0, _allData.RowCount, 0, inputColumnsCount);

            if (outputColumnsCount > 0)
            {
                _outputs = _allData.SubMatrix(0, _allData.RowCount, inputColumnsCount, outputColumnsCount);
            }
            else
            {
                _outputs = _allData.SubMatrix(0, _allData.RowCount, inputColumnsCount, outputColumnsCount);
            }


            DivideIntoTestingAndTrainingSet();
        }
        /// <summary>
        /// Under construction
        /// </summary>
        private double[][] getRawOutputs(string fileName,int numberOfInputsColumns, int numberOfOutputColumns)
        {
            double[,] rawOuts = new double[_allData.RowCount, numberOfOutputColumns];
            string[] allLines = File.ReadAllLines($"../../../DataSet/Data/{fileName}");


            return null;
        }


        private Matrix<double> _fillData(string[] columnTypes, string fileName, char splitOn,
                                         int[] outputColumns, int[] ignoredColumns, int inputsCount)
        {
            string[][] lines = _readLines(fileName, splitOn);

            // OLD STANDARIZER
            standardizer = new Standardizer(lines, columnTypes, outputColumns, ignoredColumns);
            double[][] dataJaggged = standardizer.StandardizeAll(lines);
            

            double[,] data = JaggedTo2DArray(dataJaggged);
            Matrix<double> result = Matrix<double>.Build.DenseOfArray(data);
            return result;
        }


        private string[][] _readLines(string fileName, char splitOn)
        {
            string[] allLines = File.ReadAllLines($"../../../DataSet/Data/{fileName}");

            Random rng = new Random(1);

            //Shuffle
            allLines = allLines.OrderBy(x => rng.Next()).ToArray();

            string[][] lines = new string[allLines.Length][];

            for (int i = 0; i < allLines.Length; i++)
            {
                lines[i] = allLines[i].Split(splitOn);
            }

            return lines;
        }

        private T[,] JaggedTo2DArray<T>(T[][] source)
        {
            try
            {
                int firstDim = source.Length;
                int secondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

                var result = new T[firstDim, secondDim];
                for (int i = 0; i < firstDim; ++i)
                    for (int j = 0; j < secondDim; ++j)
                        result[i, j] = source[i][j];

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular.");
            }
        }

        private void DivideIntoTestingAndTrainingSet()
        {
            int trainingInputsCount = (int)(_percentage * _inputs.RowCount);

            LearningInputs = _inputs.SubMatrix(0, trainingInputsCount, 0,
                _inputs.ColumnCount);
            TestingInputs = _inputs.SubMatrix(trainingInputsCount, _inputs.RowCount - trainingInputsCount,
                0, _inputs.ColumnCount);

            LearningOutputs = _outputs.SubMatrix(0, trainingInputsCount, 0,
                _outputs.ColumnCount);
            TestingOutputs = _outputs.SubMatrix(trainingInputsCount, _outputs.RowCount - trainingInputsCount,
                0, _outputs.ColumnCount);
        }

        private string[][] ClearMissingValues(string[][] allData, int indexOfCorruptedCollumn, string errorString)
        {
            double[] x = new double[allData.Length];
            int k = 0;

            for (int i = 0; i < allData.Length; i++)
            {
                if (!allData[i][indexOfCorruptedCollumn].Equals(errorString))
                {
                    x[k] = double.Parse(allData[i][indexOfCorruptedCollumn]);
                    k++;
                }
            }

            double columnMedian = Math.Round((x.Sum() / x.Length), 2);

            for (int i = 0; i < allData.Length; i++)
            {
                if (allData[i][indexOfCorruptedCollumn].Equals(errorString))
                {
                    allData[i][indexOfCorruptedCollumn] = columnMedian.ToString(CultureInfo.InvariantCulture);
                }
            }

            return allData;
        }

        private List<string> CountDistinctTypesOfOutput(string[][] lines, int positionOfOutput)
        {
            List<string> x = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (j == positionOfOutput)
                    {
                        x.Add(lines[i][j]);
                    }
                }
            }

            foreach (string y in x.Distinct())
            {
                Console.WriteLine(y);
            }
            return x.Distinct() as List<string>;
        }

    }
}