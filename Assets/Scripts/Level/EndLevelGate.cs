using UnityEngine;

public class EndLevelGate : MonoBehaviour
{
    [SerializeField]
    private Transform m_ExplosionAncor;
    [SerializeField]
    private GameObject m_PlayerScoredExplosion;

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Trigger activated by " + col.name);
        if (col.tag == "Player")
        {
            EventBus.LevelEnded.Dispatch(GameState.GameWon);
            if (m_PlayerScoredExplosion && m_ExplosionAncor)
                Instantiate(m_PlayerScoredExplosion, m_ExplosionAncor.position, Quaternion.identity);
        }
    }

}
