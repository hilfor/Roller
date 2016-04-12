using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowScore : MonoBehaviour
{

    private Text m_Text;
    private GameManager m_GameManager;
    void Awake()
    {
        m_GameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        m_Text = GetComponent<Text>();
        EventBus.LevelEnded.AddListener(UpdateScore);
    }

    public void OnDestroy()
    {
        EventBus.LevelEnded.RemoveListener(UpdateScore);
    }

    void UpdateScore(GameState state)
    {
        m_Text.text = "Your Score " + m_GameManager.Score;
    }

}
