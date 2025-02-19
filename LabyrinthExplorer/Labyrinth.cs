public class Labyrinth
{
    public char[,] Grid { get; private set; }
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public (int, int) Start { get; private set; }
    public (int, int) Exit { get; private set; }
    public int[,] MovementCost { get; private set; }

    public Labyrinth(string filename)
    {
        LoadLabyrinth(filename);
    }

    private void LoadLabyrinth(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        Rows = lines.Length;
        Cols = lines[0].Length;

        Grid = new char[Rows, Cols];
        MovementCost = new int[Rows, Cols];

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Grid[i, j] = lines[i][j];

                if (Grid[i, j] == 'S') Start = (i, j);
                if (Grid[i, j] == 'E') Exit = (i, j);

                MovementCost[i, j] = (Grid[i, j] == '#') ? int.MaxValue : 1;
                if (Grid[i, j] == 'M') MovementCost[i, j] = 5;
                if (Grid[i, j] == '*') MovementCost[i, j] = 0;
            }
        }
    }

    public void PrintLabyrinth()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Console.Write(Grid[i, j]);
            }
            Console.WriteLine();
        }
    }
}
