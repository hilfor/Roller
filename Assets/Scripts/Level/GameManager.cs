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
    private GameObject m_DepthDeathTriggerPrefab;
    [SerializeField]
    private Transform m_AncorSpawnPosition;
    [SerializeField]
    private Transform m_CameraInitialPosition;
    [SerializeField]
    private GameObject m_FirstFloorSegment;
    [SerializeField]
    private GameObject[] m_FloorSegments;
    [SerializeField]
    private GameObject m_EndFloorSegment;
    [SerializeField]
    private BorderSpawner m_TopSpawnBorder;
    [SerializeField]
    private AnimationCurve m_ScoreCurve;
    [SerializeField]
    private float m_ScaryBlockSpeed = 1.7f;
    [SerializeField]
    private float m_ScaryBlockSpawnDisntance = 15f;
    [SerializeField]
    private int m_CourseLength = 3;

    private int m_CurrentCourseLength = 0;

    private GameObject m_ScoreObject;
    private Text m_ScoreCounter;

    private BallController m_PlayerController;

    private GameObject m_CurrentVisibleRoad;
    private Transform m_CurrentVisibleRoadTransform;

    //private float m_NextPositionProgress = 1f;

    private float m_ScoreCount = 0;

    public string Score
    {
        get { return m_ScoreCount.ToString("0"); }
    }

    private GameObject m_ScaryBlock;
    private GameObject m_Player;
    private GameObject m_DepthDeathTrigger;
    private List<GameObject> m_RoadSegments = new List<GameObject>();

    private bool m_GamePaused = false;
    private bool m_GameEnded = false;

    void Awake()
    {
        m_ScoreObject = GameObject.Find("Score");
        m_ScoreCounter = GameObject.Find("ScoreData").GetComponent<Text>();
        RegisterEvents();
        InitializeLevel();

    }

    void RegisterEvents()
    {
        EventBus.LevelEnded.AddListener(LevelEnded);
        EventBus.NextDifficulty.AddListener(ResetLevel);
    }

    private void LevelEnded(GameState state)
    {
        m_GameEnded = true;
        if (m_ScoreObject)
            m_ScoreObject.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        RemoveEvents();
    }

    void RemoveEvents()
    {
        EventBus.NextDifficulty.RemoveListener(ResetLevel);
        EventBus.LevelEnded.RemoveListener(LevelEnded);
    }

    void InitializeLevel()
    {
        m_CurrentVisibleRoad = Instantiate(m_FirstFloorSegment, m_AncorSpawnPosition.position, Quaternion.identity) as GameObject;
        m_RoadSegments.Add(m_CurrentVisibleRoad);
        m_CurrentVisibleRoadTransform = m_CurrentVisibleRoad.transform;

        m_Player = Instantiate(m_PlayerPrefab, m_AncorSpawnPosition.position + Vector3.up, Quaternion.identity) as GameObject;
        m_PlayerController = m_Player.GetComponent<BallController>();
        m_ScaryBlock = Instantiate(m_ScaryBlockPrefab, m_AncorSpawnPosition.position - new Vector3(0, 0, m_ScaryBlockSpawnDisntance), Quaternion.identity) as GameObject;
        m_ScaryBlock.GetComponent<ScaryBlockController>().MovementSpeed = m_ScaryBlockSpeed;
        m_DepthDeathTrigger = Instantiate(m_DepthDeathTriggerPrefab, m_Player.transform.position, Quaternion.identity) as GameObject;
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
        m_ScaryBlockSpeed += 0.2f;
    }

    void ClearLevel()
    {
        Destroy(m_DepthDeathTrigger);
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
            spawnNextPrefab = m_EndFloorSegment;
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
        m_ScoreCount += Time.deltaTime * 2 * m_ScoreCurve.Evaluate(movement.z)*10;
        if (m_ScoreCounter)
            m_ScoreCounter.text = Score;
    }

}
