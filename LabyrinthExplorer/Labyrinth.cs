class Labyrinth
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
        string[] size = lines[0].Split();
        Rows = int.Parse(size[0]);
        Cols = int.Parse(size[1]);

        Grid = new char[Rows, Cols];
        MovementCost = new int[Rows, Cols];

        for (int i = 1; i <= Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                Grid[i - 1, j] = lines[i][j];

                if (Grid[i - 1, j] == 'S') Start = (i - 1, j);
                if (Grid[i - 1, j] == 'E') Exit = (i - 1, j);

                // Default movement cost
                MovementCost[i - 1, j] = (Grid[i - 1, j] == '#') ? int.MaxValue : 1;

                // Increase cost for traps ('T'), decrease cost for treasures ('$')
                if (Grid[i - 1, j] == 'T') MovementCost[i - 1, j] = 10;
                if (Grid[i - 1, j] == '$') MovementCost[i - 1, j] = 0;
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
