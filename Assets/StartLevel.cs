using UnityEngine;
using System.Collections;

public class StartLevel : MonoBehaviour {

    public GameObject _mainMenuCamera;
    public GameObject _mainMenuPlayer;
    public GameObject _mainMenuUIElements;

    public GameObject _levelCamera;
    public GameObject _levelPlayer;
    public GameObject _levelScoreCounter;

    public ScaryBlockController _scaryBlockMovementController;
    public UISequencer _sequencer;

    public void StartButtonClicked()
    {
        _mainMenuCamera.SetActive(false);
        _mainMenuPlayer.SetActive(false);
        _mainMenuUIElements.SetActive(false);

        _levelPlayer.SetActive(true);
        _levelCamera.SetActive(true);
        _levelScoreCounter.SetActive(true);

        _scaryBlockMovementController.EnableMovement();
        _sequencer.StartSequence();
        
    }

    public void PlayerDied()
    {
        _mainMenuCamera.SetActive(true);
        _mainMenuPlayer.SetActive(true);
        _mainMenuUIElements.SetActive(true);

        _levelPlayer.SetActive(false);
        _levelCamera.SetActive(false);
        _levelScoreCounter.SetActive(false);
    }

}
