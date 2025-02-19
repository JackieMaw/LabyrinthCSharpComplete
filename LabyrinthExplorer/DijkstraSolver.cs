using System;
using System.Collections.Generic;

public class DijkstraSolver
{
    private static readonly (int, int)[] directions =
    {
        (-1, 0), (1, 0), (0, -1), (0, 1) // Up, Down, Left, Right
    };

    public static List<(int, int)> FindShortestPath(Labyrinth labyrinth)
    {
        int[,] distances = new int[labyrinth.Rows, labyrinth.Cols];
        (int, int)[,] previous = new (int, int)[labyrinth.Rows, labyrinth.Cols];
        
        // Initialize distances to infinity
        for (int i = 0; i < labyrinth.Rows; i++)
        {
            for (int j = 0; j < labyrinth.Cols; j++)
            {
                distances[i, j] = int.MaxValue;
                previous[i, j] = (-1, -1);
            }
        }

        distances[labyrinth.Start.Item1, labyrinth.Start.Item2] = 0;

        var pq = new SortedSet<(int, (int, int))>
        {
            (0, labyrinth.Start)
        };

        while (pq.Count > 0)
        {
            var (currentCost, (x, y)) = pq.Min;
            pq.Remove(pq.Min);

            if ((x, y) == labyrinth.Exit)
                break;

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx, ny = y + dy;

                if (nx >= 0 && nx < labyrinth.Rows && ny >= 0 && ny < labyrinth.Cols && labyrinth.Grid[nx, ny] != '#')
                {
                    int newCost = currentCost + labyrinth.MovementCost[nx, ny];

                    if (newCost < distances[nx, ny])
                    {
                        pq.Remove((distances[nx, ny], (nx, ny))); // Remove outdated entry if exists
                        distances[nx, ny] = newCost;
                        previous[nx, ny] = (x, y);
                        pq.Add((newCost, (nx, ny)));
                    }
                }
            }  
        }

        return ReconstructPath(previous, labyrinth.Start, labyrinth.Exit);
    }

    private static List<(int, int)> ReconstructPath((int, int)[,] previous, (int, int) start, (int, int) end)
    {
        var path = new List<(int, int)>();
        var current = end;

        while (current != start && current != (-1, -1))
        {
            path.Add(current);
            current = previous[current.Item1, current.Item2];
        }

        if (current == start)
            path.Add(start);

        path.Reverse();
        return path;
    }
}
