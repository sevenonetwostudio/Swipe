using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;


    public Transform lastPlatform;
    Vector3 lastPosition;
    Vector3 newPOS;

    bool stop;
    // Start is called before the first frame update
    void Start()
    {
        lastPosition = lastPlatform.position;

        StartCoroutine(SpawnPlatforms());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnPlatforms()
    {
        while(!stop)
        {
            GeneratePosition();

            Instantiate(platform, newPOS, Quaternion.identity);

            lastPosition = newPOS;

            yield return new WaitForSeconds(0.1f);
        }
    }

    

    void GeneratePosition()
    {
        newPOS = lastPosition;

        int rand = Random.Range(0, 2);

        if(rand > 0)
        {
            newPOS.x += 2f;
        }
        else
        {
            newPOS.z += 2f;
        }
   
    }
}
