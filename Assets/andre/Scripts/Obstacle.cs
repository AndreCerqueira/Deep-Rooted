using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject platformDestroyer;
    private ObstacleGenerator obstacleGenerator;

    public int resource = -10;

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
            obstacleGenerator.DestroyObstacle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResourceManager.instance.ChangeResource(resource);
        }
    }
}
