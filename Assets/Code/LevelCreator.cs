using Assets.Code.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private System.Random Random = new System.Random();

    public GameObject Ground;
    public GameObject Wall;

#if DEBUG
    public GameObject Debug;
#endif

    public Level CreateLevel(GameObject root, int size)
    {
        var worldSceme = this.CreateWorldSceme(size);

        this.CreateGameField(root, size, worldSceme);
        this.CreateBorders(root, size);

        var groundCells = this.FindGroundCell(worldSceme).ToArray();

        return new Level
        {
            GroundCells = groundCells
        };
    }

    private void CreateBorders(GameObject root, int size)
    {
        for (int i = 0; i < size + 2; i++)
        {
            GameObject.Instantiate(this.Wall, new Vector3(-1, i-1, 0), Quaternion.identity, root.transform);
            GameObject.Instantiate(this.Wall, new Vector3(size, i-1, 0), Quaternion.identity, root.transform);
        }
        for (int i = 0; i < size; i++)
        {
            GameObject.Instantiate(this.Wall, new Vector3(i, -1, 0), Quaternion.identity, root.transform);
            GameObject.Instantiate(this.Wall, new Vector3(i, size, 0), Quaternion.identity, root.transform);
        }
    }

    private void CreateGameField(GameObject root, int size, int[][] worldSceme)
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
            {
                if (worldSceme[i][j] != 1)
                    GameObject.Instantiate<GameObject>(this.Wall, new Vector3(i, j, 0), Quaternion.identity, root.transform);
            }
    }

    private IEnumerable<Vector2> FindGroundCell(int[][] worldSceme)
    {
        for (int i = 0; i < worldSceme.Length; i++)
            for (int j = 0; j < worldSceme[i].Length; j++)
                if (worldSceme[i][j] == 1)
                    yield return new Vector2(i, j);
    }

    private int[][] CreateWorldSceme(int size)
    {
        var basePoints = this.GenerateBasePoints(size).Distinct().ToArray();

#if DEBUG_Off
        foreach (var point in basePoints)
            GameObject.Instantiate(this.Debug, new Vector3(point.Coords.x, point.Coords.y, -1), Quaternion.identity);
#endif
        var linksBetweenPoints = this.GenerateLinks(basePoints).ToArray();

        var worldSceme = new int[size][];
        for (int i = 0; i < size; i++)
            worldSceme[i] = new int[size];

        foreach (var point in basePoints)
            worldSceme[(int)point.Coords.x][(int)point.Coords.y] = 1;

        this.DrawWays(worldSceme, basePoints, linksBetweenPoints);

        return worldSceme;
    }

    private void DrawWays(int[][] worldSceme, Point[] basePoints, Edge[] linksBetweenPoints)
    {
        foreach (var link in linksBetweenPoints)
        {
            var firstPoint = basePoints[link.FirstIndex];
            var secondPoint = basePoints[link.SecondIndex];
            var currentCoords = firstPoint.Coords;

            while (currentCoords != secondPoint.Coords)
            {
                bool changed = false;
                if (currentCoords.x != secondPoint.Coords.x)
                {
                    if (currentCoords.x > secondPoint.Coords.x)
                        currentCoords.x--;
                    else
                        currentCoords.x++;
                    changed = true;
                }

                if (currentCoords.y != secondPoint.Coords.y && !changed)
                {
                    if (currentCoords.y > secondPoint.Coords.y)
                        currentCoords.y--;
                    else
                        currentCoords.y++;
                }

                worldSceme[(int)currentCoords.x][(int)currentCoords.y] = 1;
            }
        }
    }

    private IEnumerable<Edge> GenerateLinks(Point[] points)
    {
        foreach (Point point in points)
        {
            var ordered = points.
                OrderBy(o => Vector3.Distance(o.Coords, point.Coords)).ToArray();
            var nearestPoint = ordered.Take(3);

            yield return new Edge
            {
                FirstIndex = point.Index,
                SecondIndex = nearestPoint.First().Index
            };

            foreach (var closePoint in nearestPoint.Skip(1))
            {
                yield return new Edge
                {
                    FirstIndex = point.Index,
                    SecondIndex = closePoint.Index
                };
            }

            foreach (var futherPoint in ordered.Reverse().Take(3).Where(o => this.Random.NextDouble() > 0.9))
            {
                yield return new Edge
                {
                    FirstIndex = point.Index,
                    SecondIndex = futherPoint.Index
                };
            }
        }
    }

    private IEnumerable<Point> GenerateBasePoints(int size)
    {
        var count = size;
        for (int i = 0; i < count; i++)
            yield return new Point
            {
                Index = i,
                Coords = new Vector2(this.Random.Next(0, size), this.Random.Next(0, size))
            };
    }

    class Point
    {
        public Vector2 Coords;
        public int Index;
    }

    class Edge
    {
        public int FirstIndex;
        public int SecondIndex;
    }
}
