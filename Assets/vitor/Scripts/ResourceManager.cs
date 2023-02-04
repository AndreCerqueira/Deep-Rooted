using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Cecil;

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager instance;
    public TextMeshProUGUI text;
    int resourceAmount = 100;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        InvokeRepeating("ReduceResource", 1.0f, 0.5f);
    }

    public void ReduceResource()
    {
        resourceAmount -= 2;
        text.text = resourceAmount.ToString();
    }

    public void ChangeResource(int resource)
    {
        resourceAmount += resource;
        if (resourceAmount > 100)
            resourceAmount = 100;

        text.text = resourceAmount.ToString();
    }
}
