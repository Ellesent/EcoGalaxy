using UnityEngine;
using System.Collections;

public class SpawnerResources : MonoBehaviour {
    int minX = -9;
    int minY = 13;
    int maxX = 17;
    int maxY = 26;
    int randY;
    int randX;
    int chooseObjectSpawn;
    int howManyObjectsToSpawn;
    int howManySpawned = 0;
    public GameObject lake;
    public GameObject carrot; 

	// Use this for initialization
	void Start () {
        howManyObjectsToSpawn = Random.Range(5, 21);
        randX = Random.Range(minX,maxX + 1);
        randY = Random.Range(minY, maxY + 1);
        chooseObjectSpawn = Random.Range(1, 3);

        while (howManySpawned <= howManyObjectsToSpawn)
        {
            Collider2D check = Physics2D.OverlapCircle(new Vector2(randX, randY), 1);
            if (check != null)
            {
                randX = Random.Range(minX, maxX + 1);
                randY = Random.Range(minY, maxY + 1);
            }
            else
            {
                switch (chooseObjectSpawn)
                {
                    case 1:
                        Instantiate(lake, new Vector3(randX, randY), Quaternion.identity);
                        randX = Random.Range(minX, maxX + 1);
                        randY = Random.Range(minY, maxY + 1);
                        howManySpawned += 1;
                        chooseObjectSpawn = Random.Range(1, 3);
                        break;
                    case 2:
                        Instantiate(carrot, new Vector3(randX, randY), Quaternion.identity);
                        randX = Random.Range(minX, maxX + 1);
                        randY = Random.Range(minY, maxY + 1);
                        howManySpawned += 1;
                        chooseObjectSpawn = Random.Range(1, 3);
                        break;
                }
            }
        }
	
	}
	
	// Update is called once per frame
	void Update () {

       
	
	}
}
