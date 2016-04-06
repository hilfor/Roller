using UnityEngine;

public class EndLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger activated by " + col.name);
        if (col.tag == "Player")
            EventBus.GameWon.Dispatch();
        //if (col.name == "ScaryBlock")
        //    EventBus.GameLost.Dispatch();
    }

}
