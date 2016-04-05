using UnityEngine;

public class EndLevel : MonoBehaviour
{

    private void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
            EventBus.GameOver.Dispatch();
    }

}
