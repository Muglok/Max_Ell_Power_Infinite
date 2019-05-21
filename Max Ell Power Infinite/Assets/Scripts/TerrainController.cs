using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    bool created = false;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !created)
        {
            created = true;
            Vector2 position = new Vector3(transform.position.x + 16,transform.position.y, transform.position.z);
            LevelGenerator.sharedInstance.GenerateTerrain(position);
        }
        else if (other.gameObject.tag == "TerrainDestroyer")
        {
            Destroy(gameObject);
        }
    }
}
