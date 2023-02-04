using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private GameObject platformDestroyer;
    private ObstacleGenerator obstacleGenerator;

    // Start is called before the first frame update
    void OnEnable()
    {
        platformDestroyer = GameObject.Find("Platform Destroyer");
        obstacleGenerator = GameObject.Find("Obstacle Generator").GetComponent<ObstacleGenerator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Destroy platform
        if (platformDestroyer.transform.position.y < transform.position.y)
        {
            obstacleGenerator.DestroyResource(gameObject);
        }
    }
}
