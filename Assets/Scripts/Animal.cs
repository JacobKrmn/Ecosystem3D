using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float Hunger;
    public float Thirst;

    public float SearchRadius;
    public float MoveSpeed;

    public GameObject DebugPathNode;

    void Update() {
            
    }

    private void Start() {
        Debug.Log("Started");
        DrawDebugPath(new Vector3(20, 1, 30));
    }

    private void DrawDebugPath(Vector3 goal) {
        Path path = new Path(transform.position, goal);
        List<Vector3> pathToDraw = path.generatePath();

        foreach(Vector3 node in pathToDraw) {
            Instantiate(DebugPathNode, new Vector3(node.x, 1, node.y), Quaternion.identity, transform);
        }
    }
}
