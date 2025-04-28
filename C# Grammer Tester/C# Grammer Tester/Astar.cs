using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Node
{
    public int X { get; }
    public int Y { get; }
    public double G { get; set; } // Cost from start to this node
    public double H { get; set; } // Heuristic cost to goal
    public double F => G + H;     // Total cost
    public Node? Parent { get; set; }

    public Node(int x, int y)
    {
        X = x;
        Y = y;
        G = double.MaxValue;
        H = 0;
        Parent = null;
    }

    public override bool Equals(object? obj)
        => obj is Node other && X == other.X && Y == other.Y;

    public override int GetHashCode()
        => HashCode.Combine(X, Y);
}

class Astar
{
    const int Width = 10;
    const int Height = 10;

    static char[,] map = new char[Height, Width];
    static Node start = new Node(2, 2);
    static Node goal = new Node(7, 3);
    static List<Node> path = new();
    static Node current;

    static void Main()
    {
        InitMap();
        path = AStar(start, goal);
        current = start;
       
        while (true)
        {
            Console.Clear();
            DrawMap();
            if (current.Equals(goal)) break;

            int nextIndex = path.IndexOf(current) + 1;
            if (nextIndex < path.Count)
                current = path[nextIndex];

            Console.ReadKey(true);
        }

        Console.WriteLine("도착했습니다!");
    }

    static void InitMap()
    {
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                map[y, x] = '.';

        var obstacles = new List<(int x, int y)>
        {
            (1,1), (2,1), (4,1), (4,2), (1,3), (4,4), (5,4), (6,4)
        };

        foreach (var (x, y) in obstacles)
            map[y, x] = '█';

        map[start.Y, start.X] = 'S';
        map[goal.Y, goal.X] = 'G';
    }

    static void DrawMap()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (x == current.X && y == current.Y)
                    Console.Write('*');
                else
                    Console.Write(map[y, x]);
                Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    static List<Node> AStar(Node start, Node goal)
    {
        var open = new PriorityQueue<Node, double>();
        var openSet = new HashSet<Node>();
        var closed = new HashSet<Node>();

        start.G = 0;
        start.H = Heuristic(start, goal);

        open.Enqueue(start, start.F);
        openSet.Add(start);

        var directions = new (int dx, int dy)[]
        {
            (1,0), (-1,0), (0,1), (0,-1),
            (1,1), (-1,1), (1,-1), (-1,-1)
        };

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            openSet.RemoveWhere(n => n.Equals(current));

            if (current.Equals(goal))
                return ReconstructPath(current);

            closed.Add(current);

            foreach (var (dx, dy) in directions)
            {
                int nx = current.X + dx;
                int ny = current.Y + dy;

                if (!IsInBounds(nx, ny) || IsBlocked(nx, ny))
                    continue;

                var neighbor = new Node(nx, ny);

                if (closed.Contains(neighbor))
                    continue;

                double tentativeG = current.G + Distance(current, neighbor);

                var existing = openSet.FirstOrDefault(n => n.Equals(neighbor));
                bool inOpen = existing != null;

                if (!inOpen || tentativeG < existing.G)
                {
                    neighbor.G = tentativeG;
                    neighbor.H = Heuristic(neighbor, goal);
                    neighbor.Parent = current;

                    if (!inOpen)
                    {
                        open.Enqueue(neighbor, neighbor.F);
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return new List<Node>(); // 실패
    }

    static List<Node> ReconstructPath(Node node)
    {
        var path = new List<Node>();
        while (node != null)
        {
            path.Insert(0, node);
            node = node.Parent!;
        }
        return path;
    }

    static bool IsInBounds(int x, int y)
        => x >= 0 && x < Width && y >= 0 && y < Height;

    static bool IsBlocked(int x, int y)
        => map[y, x] == '█';

    static double Heuristic(Node a, Node b)
        => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));

    static double Distance(Node a, Node b)
        => Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
}
