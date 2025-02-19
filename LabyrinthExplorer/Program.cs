string filename = "labyrinth_5x5.txt";
Labyrinth labyrinth = new Labyrinth(filename);

DijkstraSolver solver = new DijkstraSolver(labyrinth);
int shortestCost = solver.FindShortestPath();

if (shortestCost != -1)
{
    Console.WriteLine($"✅ Shortest path cost: {shortestCost}");
    solver.PrintShortestPath();
}
else
{
    Console.WriteLine("❌ No path found.");
}