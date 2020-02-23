using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_RestartButton : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
