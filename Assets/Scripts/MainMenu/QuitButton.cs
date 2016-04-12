using UnityEngine;
using System.Collections;

public class QuitButton : MonoBehaviour
{

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }
}
