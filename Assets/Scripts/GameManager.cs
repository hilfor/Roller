using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_PlayerPrefab;
    [SerializeField]
    private GameObject m_ScaryBlockPrefab;
    [SerializeField]
    private Transform m_BlockSpawnPosition;
    [SerializeField]
    private Transform m_CameraInitialPosition;
    [SerializeField]
    private GameObject m_FirstFloorSegment;
    [SerializeField]
    private GameObject[] m_FloorSegments;
    [SerializeField]
    private GameObject m_WinFloorSegment;
    [SerializeField]
    private BorderSpawner m_TopSpawnBorder;
    [SerializeField]
    private AnimationCurve m_ScoreCurve;
    [SerializeField]
    private int m_CourseLength = 3;

    private int m_CurrentCourseLength = 0;

    private Text m_ScoreCounter;

    private BallController m_PlayerController;

    private GameObject m_CurrentVisibleRoad;
    private Transform m_CurrentVisibleRoadTransform;

    //private float m_NextPositionProgress = 1f;

    private float m_ScoreCount = 0;

    private GameObject m_ScaryBlock;
    private GameObject m_Player;
    private List<GameObject> m_RoadSegments = new List<GameObject>();

    private bool m_GamePaused = false;
    private bool m_GameEnded = false;

    void Awake()
    {

        m_ScoreCounter = GameObject.Find("ScoreData").GetComponent<Text>();
        RegisterEvents();
        InitializeLevel();

    }

    void RegisterEvents()
    {
        EventBus.NextDifficulty.AddListener(ResetLevel);
    }

    public void OnDestroy()
    {
        RemoveEvents();
    }

    void RemoveEvents()
    {
        EventBus.NextDifficulty.RemoveListener(ResetLevel);
    }

    void InitializeLevel()
    {
        m_CurrentVisibleRoad = Instantiate(m_FirstFloorSegment, m_BlockSpawnPosition.position, Quaternion.identity) as GameObject;
        m_RoadSegments.Add(m_CurrentVisibleRoad);
        m_CurrentVisibleRoadTransform = m_CurrentVisibleRoad.transform;

        m_Player = Instantiate(m_PlayerPrefab, m_BlockSpawnPosition.position + Vector3.up, Quaternion.identity) as GameObject;
        m_PlayerController = m_Player.GetComponent<BallController>();
        m_ScaryBlock = Instantiate(m_ScaryBlockPrefab, m_BlockSpawnPosition.position - new Vector3(0, 0, 15), Quaternion.identity) as GameObject;

        m_CurrentCourseLength = m_CourseLength;

        Camera.main.transform.position = m_CameraInitialPosition.position;
        Camera.main.transform.rotation = m_CameraInitialPosition.rotation;

        Camera.main.GetComponent<UnityStandardAssets.Utility.FollowTarget>().target = m_Player.transform;
    }

    void ResetLevel()
    {
        ClearLevel();
        MakeLevelHarder();
        InitializeLevel();
        m_GameEnded = false;
    }

    void MakeLevelHarder()
    {
        m_CourseLength++;
    }

    void ClearLevel()
    {
        Destroy(m_Player);
        Destroy(m_ScaryBlock);
        for (int i = 0; i < m_RoadSegments.Count; i++)
        {
            Destroy(m_RoadSegments[i]);
        }
    }

    void Start()
    {
        m_TopSpawnBorder.OnTriggetClearedEvent = SpawnNewRoadSegment;
    }

    void SpawnNewRoadSegment(Vector3 position)
    {
        if (m_CurrentCourseLength == 0)
            return;

        GameObject spawnNextPrefab;
        if (m_CurrentCourseLength == 1)
        {
            spawnNextPrefab = m_WinFloorSegment;
        }
        else
        {
            if (m_FloorSegments.Length > 0)
            {
                spawnNextPrefab = m_FloorSegments[Random.Range(0, m_FloorSegments.Length - 1)];
            }
            else
            {
                spawnNextPrefab = m_FirstFloorSegment;
            }
        }
        m_CurrentCourseLength--;

        position.y = m_CurrentVisibleRoadTransform.position.y;
        position.x = m_CurrentVisibleRoadTransform.position.x;

        m_CurrentVisibleRoad = Instantiate(spawnNextPrefab, position, Quaternion.identity) as GameObject;
        m_RoadSegments.Add(m_CurrentVisibleRoad);
        m_CurrentVisibleRoadTransform = m_CurrentVisibleRoad.transform;
    }

    public void UpdateScore(Vector3 movement)
    {
        if (m_GameEnded)
            return;
        m_ScoreCount += Time.deltaTime * m_ScoreCurve.Evaluate(movement.z);
        if (m_ScoreCounter)
        {
            m_ScoreCounter.text = m_ScoreCount.ToString("0.0");
        }
    }



}
