using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar 
{
    public static List<Vector2Int> GeneratePath(Vector2Int start, Vector2Int end, bool[,] passableGrid)
    {
        Node startNode = new Node(start);
        Node endNode = new Node(end);

        // return if start not passable
        if (!passableGrid[startNode.pos.x, startNode.pos.y])
        {
            return new List<Vector2Int>();
        }

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        
        open.Add(startNode);

        while(open.Count > 0)
        {
            Node currentNode = open[0];

            // node with lowest distance
            foreach(Node node in open)
            {
                if (currentNode == node) continue;

                if (currentNode.fCost > node.fCost || (currentNode.hCost > node.hCost && currentNode.fCost == node.fCost))
                    {
                    currentNode = node;
                }
            }
            open.Remove(currentNode);
            closed.Add(currentNode);

            // end was reached
            if(currentNode.pos == endNode.pos)
            {
                return RetracePath(currentNode);
            }

            foreach(Vector2Int direction in DIRECTIONS.allDirections)
            {
                Vector2Int neighbourPosition = currentNode.pos + direction;

                // edge check
                if(!CheckInBounds(neighbourPosition, passableGrid.GetLength(0))) continue;

                Node neighbour = new Node(neighbourPosition);

                // if checked already or not passable => continue
                if (closed.Contains(neighbour)) continue;
                if (!passableGrid[neighbour.pos.x, neighbour.pos.y]) continue;

                int newMoveCost = currentNode.gCost + 1;
                if(newMoveCost < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newMoveCost;
                    neighbour.hCost = Mathf.RoundToInt(10 * Vector2Int.Distance(neighbour.pos, endNode.pos));
                    neighbour.parent = currentNode;

                    if (!open.Contains(neighbour))
                    {
                        open.Add(neighbour);
                    }
                }
            }
        }

        return new List<Vector2Int>();
    }

    private static List<Vector2Int> RetracePath(Node node)
    {
        List<Vector2Int> finalPath = new List<Vector2Int>();
        Node currentNode = node;
        while(currentNode.parent != null)
        {
            finalPath.Add(currentNode.pos);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();

        return finalPath;
    }

    private static bool CheckInBounds(Vector2Int position, int maxSize)
    {
        return !(position.x < 0 || position.x >= maxSize || position.y < 0 || position.y >= maxSize);
    }
}