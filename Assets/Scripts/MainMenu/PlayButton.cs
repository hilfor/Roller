using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {

    public void LoadPlayLevel()
    {
        SceneManager.LoadScene("S1");
    }
}
