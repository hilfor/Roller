using UnityEngine;
using System.Collections;

public class TryHarderButton : MonoBehaviour {

    [SerializeField]
    private Animator m_Animator;

	public void TryHarder()
    {
        EventBus.NextDifficulty.Dispatch();
        m_Animator.SetTrigger("Hide");
    }
}
