using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForceField.Interfaces;
using Microsoft.Xna.Framework;
using ForceField.Domain.GameLogic;
using ForceField.Domain.Comparers;
using System.Collections.ObjectModel;

namespace ForceField.Core.Managers.Services
{
    public class PathService : IPathService
    {
        public PathService()
        {

        }


        public List<Point> GetPath(TileMap tileMap, int area, Point start, Point end)
        {
            var closedSet = new Collection<PathNode>();
            var openSet = new Collection<PathNode>();

            PathNode startNode = new PathNode()
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, end)
            };
            openSet.Add(startNode);
            while (openSet.Count > 0)
            {

                var currentNode = openSet.OrderBy(node =>
                  node.EstimateFullPathLength).First();
                if (currentNode.Position == end)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode, area, end, tileMap))
                {

                    if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                        continue;
                    var openNode = openSet.FirstOrDefault(node =>
                      node.Position == neighbourNode.Position);

                    if (openNode == null)
                        openSet.Add(neighbourNode);
                    else
                        if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                        {
                            openNode.CameFrom = currentNode;
                            openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                        }
                }
            }
            return null;
        }

        private static int GetDistanceBetweenNeighbours()
        {
            return 1;
        }
        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        private static Collection<PathNode> GetNeighbours(PathNode currentPosition, int area, Point goal, TileMap tileMap)
        {
            Point CurrentTilePosition = currentPosition.Position;
            Point resultPos = new Point();
            
            int offset = area - 1;
            Collection<PathNode> paths = new Collection<PathNode>();
            Point position = currentPosition.Position;
            bool go = true;
            for (int i = 0; i < area; ++i)
            {
                Tile tile = tileMap.GetTile(position.X + i, position.Y - 1);
                if (tile.IsOccupy)
                {
                    i = area;
                    go = false;
                }
            }



            if (go == true)
            {
                resultPos = new Point(position.X, position.Y - 1);
                PathNode node = new PathNode()
                {
                    Position = resultPos,
                    CameFrom = currentPosition,
                    PathLengthFromStart = currentPosition.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(resultPos, goal)
                };

                paths.Add(node);
            }

            position = new Point(CurrentTilePosition.X + offset, CurrentTilePosition.Y + offset);

            for (int i = 0; i < area; ++i)
            {
                Tile tile = tileMap.GetTile(position.X - i, position.Y + 1);
                if (tile.IsOccupy)
                {
                    i = area;
                    go = false;
                }
            }

            if (go == true)
            {
                resultPos = new Point(position.X, position.Y + 1);

                PathNode node = new PathNode()
                {
                    Position = resultPos,
                    CameFrom = currentPosition,
                    PathLengthFromStart = currentPosition.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(resultPos, goal)
                };
                paths.Add(node);
            }

            position = new Point(CurrentTilePosition.X, CurrentTilePosition.Y + offset);

            for (int i = 0; i < area; ++i)
            {
                Tile tile = tileMap.GetTile(position.X - 1, position.Y - i);
                if (tile.IsOccupy)
                {
                    i = area;
                    go = false;
                }
            }

            if (go == true)
            {
                resultPos = new Point(position.X - 1, position.Y);

                PathNode node = new PathNode()
                {
                    Position = resultPos,
                    CameFrom = currentPosition,
                    PathLengthFromStart = currentPosition.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(resultPos, goal)
                };
                paths.Add(node);
            }

            position = new Point(CurrentTilePosition.X + offset, CurrentTilePosition.Y);

            for (int i = 0; i < area; ++i)
            {
                Tile tile = tileMap.GetTile(position.X + 1, position.Y + i);
                if (tile.IsOccupy)
                {
                    i = area;
                    go = false;
                }
            }

            if (go == true)
            {
                resultPos = new Point(position.X + 1, position.Y);

                PathNode node = new PathNode()
                {
                    Position = resultPos,
                    CameFrom = currentPosition,
                    PathLengthFromStart = currentPosition.PathLengthFromStart +
                    GetDistanceBetweenNeighbours(),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(resultPos, goal)
                };
                paths.Add(node);
            }

            return paths;
        }

        private static List<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new List<Point>();
            var currentNode = pathNode;
            while (currentNode != null)
            {
                result.Add(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }
            result.Reverse();
            return result;
        }
    }
}
