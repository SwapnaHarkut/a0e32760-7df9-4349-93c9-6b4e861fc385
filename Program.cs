using System;
using System.IO;
using System.Linq;

namespace RobotCodingPuzzle
{
    class Program
    {
        public static void Main()
        {
            Robot rb = new Robot(5);
            StreamReader myReader = new StreamReader(@"C:\Users\swapna\source\repos\RobotCodingPuzzle\Values.txt");
            string line = "";

            while (line != null)
            {
                line = myReader.ReadLine();
                rb.performAction(line);
                if (line.Equals("REPORT"))
                {
                    return;
                }
            }
            myReader.Close();
            Console.ReadLine();
        }
    }
    class Robot
    {
        private Tuple<int, int> position;
        private int[,] plate;
        private int matrixSize = 0;

        public Robot(int size)
        {
            matrixSize = size;
            plate = new int[size, size];
            position = null;
        }

        public int getMatrixSize()
        {
            return plate[0, 0];
        }

        public void cmdPlace(String location)
        {
            String[] args = location.Split(',');
            int x = Int32.Parse(args[0]);
            int y = Int32.Parse(args[1]);
            if (x >= matrixSize || y >= matrixSize) return;

            position = new Tuple<int, int>(x, y);
        }

        public String cmdDetect()
        {
            if (position == null) return "ERR";
            if (plate[position.Item1, position.Item2] == 1) return "FULL";
            else return "EMPTY";
        }

        public void cmdDrop()
        {
            plate[position.Item1, position.Item2] = 1;
        }

        public void cmdMove(String direction)
        {
            if (position == null) return;
            switch (direction)
            {
                case "N":
                    if (position.Item2 + 1 < matrixSize)
                    {
                        position = new Tuple<int, int>(position.Item1, position.Item2 + 1);
                    }
                    break;
                case "S":
                    if (position.Item2 > 0)
                    {
                        position = new Tuple<int, int>(position.Item1, position.Item2 - 1);
                    }
                    break;
                case "E":
                    if (position.Item1 + 1 < matrixSize)
                    {
                        position = new Tuple<int, int>(position.Item1 + 1, position.Item2);
                    }
                    break;
                case "W":
                    if (position.Item1 > 0)
                    {
                        position = new Tuple<int, int>(position.Item1 - 1, position.Item2);
                    }
                    break;
                default:
                    break;
            }
        }

        public void cmdReport()
        {
            if (position == null) return;
            Console.WriteLine("{0},{1},{2}", position.Item1, position.Item2, cmdDetect());
        }

        public void performAction(String cmd)
        {
            String[] args = cmd.Split(' ');
            switch (args[0])
            {
                case "PLACE":
                    cmdPlace(args[1]);
                    break;
                case "DETECT":
                    cmdDetect();
                    break;
                case "DROP":
                    cmdDrop();
                    break;
                case "MOVE":
                    cmdMove(args[1]);
                    break;
                case "REPORT":
                    cmdReport();
                    break;
                default:
                    break;
            }
        }
    }
}
