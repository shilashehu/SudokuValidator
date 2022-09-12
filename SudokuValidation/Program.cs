namespace SudokuValidator
{
    public class Program
    {
        public static void Main()
        {
            //given sudoku data structures

            int[][] goodSudoku1 =
            {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},
                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},
                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };

            int[][] goodSudoku2 =
            {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

            int[][] badSudoku1 =
            {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

            int[][] badSudoku2 =
            {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };

            try
            {
                ValidateSudoku(goodSudoku1);
                ValidateSudoku(goodSudoku2);
                ValidateSudoku(badSudoku1);
                ValidateSudoku(badSudoku2);
            }
            catch (Exception ex)
            {
                Console.Write("Error during Sudoku validation: " + ex);
            }
        }

        public static bool ValidateSudoku(int[][] sudoku)
        {
            try
            {
                int dimension = CalculateDimension(sudoku);

                //Data structure dimension: NxN where N > 0 and vN == integer
                if (!ValidateDimension(dimension))
                    throw new Exception("Invalid Sudoku Dimension!");

                //Rows may only contain integers: 1..N 
                if (HasInvalidRow(dimension, sudoku))
                    throw new Exception("Ivalid Sudoku rows!");

                //Columns may only contain integers: 1..N 
                if (HasInvalidColumn(dimension, sudoku))
                    throw new Exception("Invalid sudoku columns!");

                //'Little squares' may only contain integers: 1..N
                if (HasInvalidSquare(dimension, sudoku))
                    throw new Exception("Invalid sudoku squares!");

                Console.WriteLine(true + ": Sudoku Validation Successful.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(false + ": " + ex.Message);
                return false;
            }
        }

        public static int CalculateDimension(int[][] sudoku)
        {
            try
            {
                int cols = sudoku.Count();

                //sudoku size should be NxN
                if (Array.Exists(sudoku, row => row.Count() != cols))
                    throw new Exception();

                return cols;
            }
            catch
            {
                return -1;
            }
        }

        public static bool ValidateDimension(int dimension)
        {
            try
            {
                //sudoku dimension should be > 0 
                if (dimension < 0)
                    throw new Exception();

                var sqrt = Math.Sqrt(dimension);

                //sudoku square root of the dimension should be integer number 
                return sqrt == (int)sqrt;
            }
            catch
            {
                return false;
            }
        }

        public static bool HasInvalidRow(int dimension, int[][] sudoku)
        {
            bool hasInvalidRow = Array.Exists(sudoku, row => !HasUniqueNumbers(dimension, row));
            return hasInvalidRow;
        }

        public static bool HasInvalidColumn(int dimension, int[][] sudoku)
        {
            //this list keeps numbers in each column

            List<int> column = new List<int>(dimension);

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    column.Add(sudoku[j][i]);
                }
                bool hasInvalidColumn = !HasUniqueNumbers(dimension, column.ToArray());
                column.Clear();
                if (hasInvalidColumn)
                    return true;
            }
            return false;
        }

        public static bool HasInvalidSquare(int dimension, int[][] sudoku)
        {
            //this list keeps numbers of each square
            List<int> square = new List<int>(dimension);
            int square_size = (int)Math.Sqrt(dimension);

            //goes to the start of the last square, steps with square_size
            for (int i = 0; i <= dimension - square_size; i += square_size)
            {
                //same thing for the other dimension
                for (int j = 0; j <= dimension - square_size; j += square_size)
                {
                    int k, l;
                    //steps through the values of each square
                    for (k = i; k < i + square_size; k++)
                    {
                        for (l = j; l < j + square_size; l++)
                        {
                            square.Add(sudoku[k][l]);
                        }
                    }
                    bool hasInvalidSquare = !HasUniqueNumbers(dimension, square.ToArray());
                    square.Clear();
                    if (hasInvalidSquare)
                        return true;
                }
            }
            return false;
        }

        public static bool HasUniqueNumbers(int dimension, int[] sudoku_values)
        {
            try
            {
                int[] numbers = Enumerable.Range(1, dimension).ToArray();
                if (Array.Exists(numbers, number => !sudoku_values.Contains(number)))
                    throw new Exception();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}