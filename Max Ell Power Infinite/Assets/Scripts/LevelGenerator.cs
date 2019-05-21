using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator sharedInstance;

    public List<GameObject> allTerrainPrefabs = new List<GameObject>();

    public int actualMinRange;
    public int actualMaxRange;

    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
        actualMinRange = 0;
        actualMaxRange = 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTerrain(Vector2 position)
    {
        int randomIndex = Random.Range(actualMinRange, actualMaxRange);
        Instantiate(allTerrainPrefabs[randomIndex], position, Quaternion.identity);
    }
}
