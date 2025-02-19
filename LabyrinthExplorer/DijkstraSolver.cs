class DijkstraSolver
{
    private Labyrinth labyrinth;
    private int[,] distances;
    private (int, int)[,] previous;
    private static readonly int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

    public DijkstraSolver(Labyrinth labyrinth)
    {
        this.labyrinth = labyrinth;
        distances = new int[labyrinth.Rows, labyrinth.Cols];
        previous = new (int, int)[labyrinth.Rows, labyrinth.Cols];
    }

    public int FindShortestPath()
    {
        for (int i = 0; i < labyrinth.Rows; i++)
            for (int j = 0; j < labyrinth.Cols; j++)
                distances[i, j] = int.MaxValue;

        distances[labyrinth.Start.Item1, labyrinth.Start.Item2] = 0;

        var pq = new SortedSet<(int cost, int x, int y)>(
            Comparer<(int, int, int)>.Create((a, b) => a.cost == b.cost ? (a.x == b.x ? a.y - b.y : a.x - b.x) : a.cost - b.cost)
        );

        pq.Add((0, labyrinth.Start.Item1, labyrinth.Start.Item2));

        while (pq.Count > 0)
        {
            var (currentCost, x, y) = pq.Min;
            pq.Remove(pq.Min);

            if ((x, y) == labyrinth.Exit) return distances[x, y];

            foreach (var dir in directions)
            {
                int nx = x + dir[0], ny = y + dir[1];

                if (nx >= 0 && nx < labyrinth.Rows && ny >= 0 && ny < labyrinth.Cols && labyrinth.Grid[nx, ny] != '#')
                {
                    int newCost = distances[x, y] + labyrinth.MovementCost[nx, ny];

                    if (newCost < distances[nx, ny])
                    {
                        pq.Remove((distances[nx, ny], nx, ny));
                        distances[nx, ny] = newCost;
                        previous[nx, ny] = (x, y);
                        pq.Add((newCost, nx, ny));
                    }
                }
            }
        }
        return -1; // No path found
    }

    public void PrintShortestPath()
    {
        var path = new List<(int, int)>();
        var current = labyrinth.Exit;

        while (current != labyrinth.Start)
        {
            path.Add(current);
            current = previous[current.Item1, current.Item2];
        }
        path.Add(labyrinth.Start);
        path.Reverse();

        Console.WriteLine("\nShortest Path:");
        foreach (var (x, y) in path)
        {
            labyrinth.Grid[x, y] = '*';
        }

        labyrinth.PrintLabyrinth();
    }
}
