using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject platformDestroyer;
    private ObstacleGenerator obstacleGenerator;

    public int resource = -20;

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
            DestroyItself();
        }
    }

    
    public void DestroyItself() 
    { 
        obstacleGenerator.DestroyObstacle(gameObject);
    }
    

    public void AnimateDestruction()
    {
        GetComponent<Animator>().SetTrigger("Destroy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResourceManager.instance.ChangeResource(resource);
        }
    }
}
