using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Requirements {
    Hunger,
    Thirst
}

public class BunnyController : MonoBehaviour
{
    public float Hunger = 0;
    public float Thirst = 0;

    public float SearchRadius = 20f;
    public float moveSpeed = 20f;
    public float positionOffset = 0.5f;

    public List<string> RelevantObjects;

    void Start()
    {
        
    }

    void Update() {

        List<Collider> withinRadius = GetObjectsWithinRadius();
        if (withinRadius.Count > 0) {
            MoveTarget(getNearestTarget(withinRadius));
        } else {
            MoveRandom();
        }
    }

    private Requirements getPriorityRequirement() {
        if (Hunger > Thirst) {
            return Requirements.Hunger;
        } else if (Hunger < Thirst) {
            return Requirements.Thirst;
        } else { return Requirements.Thirst; }
    }

    private void Eat() {

    }

    private void Drink() {

    }

    /*
     * Returns a list of all relevant colliders (water, food, other bunnies, etc.)
     */
    private List<Collider> GetObjectsWithinRadius() {
        Collider[] objectColliders = Physics.OverlapSphere(transform.position, SearchRadius);
        List<Collider> relevantColliders = new List<Collider>();

        foreach (var objectCollider in objectColliders) {
            if (objectColliders.Length > 0) {
                if (RelevantObjects.Contains(objectCollider.name)) {
                    relevantColliders.Add(objectCollider);
                }
            }
        }

        return relevantColliders;
    }

    /*
     * Returns the Vector3 position of the nearest relevant object
     */
    private Vector3 getNearestTarget(List<Collider> colliders) {
        //Create a list of gameobjects instead of colliders
        List<GameObject> objects = new List<GameObject>();
        foreach (var collider in colliders) {
            objects.Add(collider.gameObject);
        }

        //Calculate distance to each gameObject and keep the nearest gameobject
        GameObject nearest = objects[0]; //set default to first object in list
        float smallestDistance = -1;
        foreach (GameObject gameObject in objects) {
            float distance = Vector3.Distance(transform.position, gameObject.transform.position);
            if (distance < smallestDistance || smallestDistance == -1) { //If smallestDistance == -1 it's the first check
                smallestDistance = distance;
                nearest = gameObject;
            }
        }

        return nearest.transform.position;
    }

    /*
     * Moves the bunny to the specified target
     */
    private void MoveTarget(Vector3 target) {
        //Vector3 offsetTarget = new Vector3(target.x + positionOffset, target.y, target.z + positionOffset);
        Vector3 moveDirection = (target - transform.position).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);

        //Prevent Y-movement
        Vector3 pos = transform.position;
        pos.y = 1;
        transform.position = pos;
    }

    /*
     * Moves the bunny randomly around
     */
    private void MoveRandom() {
        Vector3 randomLocation = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        MoveTarget(randomLocation);
    }
}
