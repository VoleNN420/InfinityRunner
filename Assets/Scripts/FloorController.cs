using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public GameObject floor1;
    public GameObject floor2;

    public GameObject[] floorTiles;

    private void FixedUpdate()
    {
        if(!GameManager.instance.inGame) return;
            floor1.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0, 0);
            floor2.transform.position -= new Vector3(GameManager.instance.worldScrollingSpeed, 0, 0);

            if (floor2.transform.position.x < -7f)
            {
                int tileIndex = Random.Range(0, floorTiles.Length);
                var newTile = Instantiate(floorTiles[tileIndex], floor2.transform.position + new Vector3(21f, 0, 0), Quaternion.identity);
                Destroy(floor1);


                floor1 = floor2;
                floor2 = newTile;
                
            }
    }
}
