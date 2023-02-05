using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;

public class ResourceManager : MonoBehaviour
{
    public Animator animator;
    public PlayerController player;


    public static ResourceManager instance;
    public TextMeshProUGUI text;
    public int resourceAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        InvokeRepeating("ReduceResource", 1.0f, 0.8f);
    }

    public void ReduceResource()
    {
        // resourceAmount -= 4;
        text.text = resourceAmount.ToString();
    }

    public void ChangeResource(int resource)
    {
        resourceAmount += resource;

        if (resourceAmount <= -80)
        {
            //player.isDead = true;
            //animator.SetBool("isDead", true);
        }
        text.text = resourceAmount.ToString();
    }
}
