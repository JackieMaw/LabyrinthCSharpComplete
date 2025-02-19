string filename = @"..\..\..\labyrinth_5x5.txt";
Labyrinth labyrinth = new Labyrinth(filename);
labyrinth.PrintLabyrinth();

var shortestPath = DijkstraSolver.FindShortestPath(labyrinth);

Console.WriteLine("Shortest path found:");
foreach (var (x, y) in shortestPath)
{
    Console.WriteLine($"({x}, {y})");
}
