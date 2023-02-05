using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    // Constants
    const float levelWidth = 20f;
    const float scoreIncreaseValue = 500;

    // Global Variables
    public Vector3 spawnObstaclePosition = new Vector3(0, -5, 0);
    public Vector3 spawnResourcePosition = new Vector3(0, -5, 0);
    public int obstacleQuantity = 10; // upgrade to 20 later
    public int obstacleCount = 1;
    public int resourceQuantity = 10; // upgrade to 20 later
    public int resourceCount = 1;

    public float obstacleMinY;
    public float obstacleMaxY;

    public float resourceMinY;
    public float resourceMaxY;

    float scoreNeededToIncreasePlatformDistance = scoreIncreaseValue;

    [Header("Obstacles")]
    public GameObject obstacle;
    
    [Header("Power Ups")]
    public GameObject resource;

    // Start is called before the first frame update
    void Start()
    {
        // Create initial obstacles
        for (int i = 1; i < obstacleQuantity; i++)
        {
            CreateObstacle();
        }

        // Create initial resources
        for (int i = 1; i < resourceQuantity; i++)
        {
            CreateResource();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Increase the platform distances
        if (GameManager.score > scoreNeededToIncreasePlatformDistance)
        {
            scoreNeededToIncreasePlatformDistance += scoreIncreaseValue;
            obstacleMinY -= 1;
            resourceMaxY -= 2;
        }

    }


    public void CreateObstacle()
    {
        // get a value between minY, maxY
        float y = Random.Range(obstacleMinY, obstacleMaxY);
        spawnObstaclePosition.y -= y;
        spawnObstaclePosition.x = Random.Range(-levelWidth, levelWidth);
        
        // Spawn obstacle in GameObject.Find("Obstacles").transform
        GameObject newObstacle = Instantiate(obstacle, spawnObstaclePosition, Quaternion.identity);
        newObstacle.transform.parent = GameObject.Find("Obstacles").transform;
        
        obstacleCount++;
    }


    public void DestroyObstacle(GameObject gameObject)
    {
        // Destroy obstacle
        Destroy(gameObject);

        // Create new obstacle
        CreateObstacle();
        
        obstacleCount--;
    }
    

    public void CreateResource()
    {
        // get a value between minY, maxY
        float y = Random.Range(resourceMinY, resourceMaxY);
        spawnResourcePosition.y -= y;
        spawnResourcePosition.x = Random.Range(-levelWidth, levelWidth);

        // Spawn obstacle in GameObject.Find("Obstacles").transform
        GameObject newResource = Instantiate(resource, spawnResourcePosition, Quaternion.identity);
        newResource.transform.parent = GameObject.Find("Resources").transform;

        resourceCount++;
    }


    public void DestroyResource(GameObject gameObject)
    {
        // Destroy obstacle
        Destroy(gameObject);

        // Create new obstacle
        CreateResource();
        
        resourceCount--;
    }
}
