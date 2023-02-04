using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variables
    static public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = (int)(player.transform.position.y * -1);
        if (score < 0) score = 0;
        

        // scoreText is the meters that the player has traveled
        scoreText.text = (int)(player.transform.position.y * -1) + " Meters" ;
    }
}
