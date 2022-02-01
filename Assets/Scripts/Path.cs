using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private Vector3 start { get; set; }
    private Vector3 goal { get; set; }
    private List<Vector3> path { get; }

    public Path(Vector3 start, Vector3 goal) {
        this.start = start;
        this.goal = goal;
        path = new List<Vector3>();
    }
    
    public List<Vector3> generatePath() {
        Vector3 currentNode = start;

        while (currentNode != goal) {
            Vector3 nextNode = calcBestNode(currentNode);
            path.Add(nextNode);
            currentNode = nextNode;
        }

        return path;
    }

    private Vector3 calcBestNode(Vector3 origin) {
        List<Vector3> neighbors = new List<Vector3>();

        neighbors.Add(new Vector3(origin.x - 1, origin.y, origin.z));
        neighbors.Add(new Vector3(origin.x + 1, origin.y, origin.z));
        neighbors.Add(new Vector3(origin.x, origin.y, origin.z - 1));
        neighbors.Add(new Vector3(origin.x, origin.y, origin.z + 1));

        float smallestDistance = -1;
        Vector3 closestNode = neighbors[1];
        foreach(Vector3 node in neighbors) {
            float distance = Vector3.Distance(node, goal);

            if (smallestDistance == -1) {
                smallestDistance = distance;
                closestNode = node;
            } else if (distance < smallestDistance) {
                closestNode = node;
                smallestDistance = distance;
            }
        }

        return closestNode;
    }


}
