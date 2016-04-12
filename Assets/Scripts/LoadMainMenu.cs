using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main");
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            LoadMenu();
    }
}
