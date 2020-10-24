using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Conwoys_sGame
{
    /// <summary>
    /// Contains methods used to run Conway's Game of Life
    /// </summary>
    /// <author>
    /// STUDENT NAME : STUDENT ID
    /// </author>
    public class MyGame
    {
        /*
         * This function is creatded to get a random 
         * value of a cell. There is a 50% chance 
         * that a cell has value "true", the rest 
         * are "false".
         */
        public static double GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(0, 100);
        }

        /// <summary>
        /// Returns a new grid for Conway's Game of life using the given dimensions.
        /// Each cell has a 50% chance of initially being alive.
        /// </summary>
        /// <param name="rows">The desired number of rows</param>
        /// <param name="cols">The desired number of columns</param>
        /// <returns></returns>

        public static bool[,] MakeGrid(int rows, int columns)
        {
            bool[,] grid = new bool[rows,columns ];
            int i, j;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < columns; j++)
                {
                    double probablity = GetRandomNumber();      //Gets a random number between 0 & 100.
                    if (probablity >= 50)
                    {
                        grid[i, j] = true;
                    }
                    else
                    {
                        grid[i, j] = false;
                    }
                }
            }
            return grid;
        }

        /// <summary>
        /// Writes the given game grid to standard output
        /// </summary>
        /// <param name="grid">The grid to draw to standard output</param>
        /// 

        public static void DrawGrid(bool[,] grid)
        {
            int rowLen;
            int colLen;
            rowLen = grid.GetLength(0);     //This is to get the number of rows of the grid we made.
            colLen = grid.GetLength(1);     //This is to get the number of columns of the grid we made.
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if(grid[i,j]==true)
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();        //Just to move to the next row.
            }
        }

        /// <summary>
        /// Returns the number of living neighbours adjacent to a given cell position
        /// </summary>
        /// <param name="grid">The game grid</param>
        /// <param name="row">The cell's row</param>
        /// <param name="col">The cell's column</param>
        /// <returns>The number of adjacent living neighbours</returns>

        public static int CountNeighbours(bool[,] grid, int row, int col)
        {
            int rowLen;
            int colLen;
            rowLen = grid.GetLength(0);     //This is to get the number of rows of the grid we made.   
            colLen = grid.GetLength(1);     //This is to get the number of coulmns of the grid we made.
            ;
            int NumOfAliveNeighbors = 0;       //To keep count of living neighbours.

            for (int i = row - 1; i < row + 2; i++)
            {
                for (int j = col - 1; j < col + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= rowLen || j >= colLen)))
                    {
                        if (!((i == row) && (j == col)))
                        {
                            if (grid[i, j] == true)
                            {
                                NumOfAliveNeighbors++;
                            }
                        }
                    }
                }
            }
            return NumOfAliveNeighbors;
        }

        /// <summary>
        /// Returns an updated grid after progressing the rules of the Game of Life.
        /// </summary>
        /// <param name="grid">The original grid from which the new grid is derived</param>
        /// <returns>A new grid which has been updated by one time-step</returns>

        public static bool[,] UpdateGrid(bool[,] oldGrid) 
        {
            int rowLen;
            int colLen;
            rowLen = oldGrid.GetLength(0);       //This is to get the number of rowss of the grid we made.
            colLen = oldGrid.GetLength(1);       //This is to get the number of coulmns of the grid we made.
            bool[,] updatedGrid=new bool[rowLen,colLen];
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    if(oldGrid[i,j]==true)
                    {
                        if (CountNeighbours(oldGrid, i, j) > 2)
                        {
                            updatedGrid[i, j] = false;
                        }
                        if (CountNeighbours(oldGrid, i, j) == 2 || CountNeighbours(oldGrid, i, j) == 3)
                        {
                            updatedGrid[i, j] = true;
                        }
                        if (CountNeighbours(oldGrid, i, j) < 3)
                        {
                            updatedGrid[i, j] = false;
                        }
                    }
                    if(oldGrid[i, j] == false)
                    {
                        if (CountNeighbours(oldGrid, i, j) == 3)
                        {
                            updatedGrid[i, j] = true;
                        }
                    }
                }
            }
            return updatedGrid;
        }

    }


    class Program
    {
        /// <summary>
        /// Runs the game of life according to its rules and continuously refreshes the result
        /// </summary>
        static void Main(string[] args)
        {
            int rows, columns;
            Console.Write("Enter number of rows: ");
            rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter number of columns: ");
            columns = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("WELCOME!!! \nPress ENTER to start simulation or press any other key to exit the program\n");
            ConsoleKeyInfo keyEntered;
            keyEntered = Console.ReadKey();
            bool[,] grid = MyGame.MakeGrid(rows, columns);
            MyGame.DrawGrid(grid);
            Console.WriteLine();
            while (true)
            {
                keyEntered = Console.ReadKey();
                if (keyEntered.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    grid = MyGame.UpdateGrid(grid);
                    MyGame.DrawGrid(grid);
                    Console.WriteLine();
                    Console.WriteLine("Press enter to get going and Press any other key to exit the program.\n");
                }
                else
                {
                    return;
                }
            }
        }
    }

}