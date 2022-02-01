using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public float MapSizeX;
    public float MapSizeZ;
    public float TileOffset;
    public float WaterThreshold;
    public int TreeCount;
    public float DetailScale;

    public GameObject GrassTile;
    public GameObject WaterTile;
    public GameObject TreeObject;

    public Dictionary<Vector3, string> tilePositions = new Dictionary<Vector3, string>();

    void Start()
    {
        for (int x = 0; x < MapSizeX; x++) {
            for (int z = 0; z < MapSizeZ; z++) {
                Vector3 pos = new Vector3(x + TileOffset, 0, z + TileOffset);

                GameObject toInstantiate;

                if (generateNoise(x, z, DetailScale) > WaterThreshold) {
                    toInstantiate = WaterTile;
                    tilePositions.Add(pos, "Water");
                } else {
                    toInstantiate = GrassTile;
                    tilePositions.Add(pos, "Grass");
                }

                GameObject tile = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
                tile.transform.SetParent(transform);
            }
        }

        SpawnTrees();
    }

    private void SpawnTrees() {
        for (int c = 0; c < TreeCount; c++) {
            Vector3 pos = ObjectSpawnLocation();
            Vector3 tilePos = new Vector3(pos.x, 0, pos.z);
            while (tilePositions[tilePos] == "Water") {
                pos = ObjectSpawnLocation();
                tilePos = new Vector3(pos.x, 0, pos.z);
            }
            GameObject toPlaceObject = Instantiate(TreeObject, pos, Quaternion.identity);
            toPlaceObject.transform.parent = transform;
        }
    }

    private float generateNoise(int x, int z, float detailScale) {
        float xNoise = (x + transform.position.x) / detailScale;
        float zNoise = (z + transform.position.z) / detailScale;

        return Mathf.PerlinNoise(xNoise, zNoise);
    }

    private Vector3 ObjectSpawnLocation() {
        int rndX = Random.Range(0, (int)MapSizeX);
        int rndZ = Random.Range(0, (int)MapSizeZ);

        return new Vector3(rndX, 1, rndZ);
    }

}
