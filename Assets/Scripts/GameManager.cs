using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_FirstFloorSegment;
    [SerializeField]
    private GameObject m_PlayerPrefab;
    [SerializeField]
    private Transform m_BlockSpawnPosition;
    [SerializeField]
    private GameObject[] m_FloorSegments;
    [SerializeField]
    private BorderSpawner m_TopSpawnBorder;
    [SerializeField]
    private AnimationCurve m_ScoreCurve;

    private Text m_ScoreCounter;


    private BallController m_PlayerController;

    private GameObject m_CurrentVisibleRoad;
    private Transform m_CurrentVisibleRoadTransform;

    //private float m_NextPositionProgress = 1f;

    private float m_ScoreCount = 0;

    void Awake()
    {

        m_ScoreCounter = GameObject.Find("ScoreData").GetComponent<Text>();

        m_CurrentVisibleRoad = Instantiate(m_FirstFloorSegment, m_BlockSpawnPosition.position, Quaternion.identity) as GameObject;
        m_CurrentVisibleRoadTransform = m_CurrentVisibleRoad.transform;
        GameObject playerObject = Instantiate(m_PlayerPrefab, m_BlockSpawnPosition.position + Vector3.up, Quaternion.identity) as GameObject;
        m_PlayerController = playerObject.GetComponent<BallController>();
        Camera.main.GetComponent<UnityStandardAssets.Utility.FollowTarget>().target = playerObject.transform;

    }

    void Start()
    {
        m_TopSpawnBorder.OnTriggetClearedEvent = SpawnNewRoadSegment;
    }

    void SpawnNewRoadSegment(Vector3 position)
    {
        GameObject spawnObject;
        if (m_FloorSegments.Length > 0)
        {
            spawnObject = m_FloorSegments[Random.Range(0, m_FloorSegments.Length - 1)];
        }
        else
        {
            spawnObject = m_FirstFloorSegment;
        }
        position.y = m_CurrentVisibleRoadTransform.position.y;
        position.x = m_CurrentVisibleRoadTransform.position.x;

        Debug.Log("Spawning next segment at " + position);

        m_CurrentVisibleRoad = Instantiate(spawnObject, position, Quaternion.identity) as GameObject;
        m_CurrentVisibleRoadTransform = m_CurrentVisibleRoad.transform;
    }

    public void UpdateScore(Vector3 movement)
    {
        m_ScoreCount += Time.deltaTime * m_ScoreCurve.Evaluate(movement.z);
        if (m_ScoreCounter)
        {
            m_ScoreCounter.text = m_ScoreCount.ToString("0.0");
        }
    }



}
