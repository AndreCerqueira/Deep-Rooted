using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScreen : MonoBehaviour
{
    public void ChangeToMainScreen()
    {
        SceneManager.LoadScene("Main");
    }
}
