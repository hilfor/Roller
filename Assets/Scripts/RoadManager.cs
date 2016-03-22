using UnityEngine;
using System.Collections;
using BitStrap;
public class RoadManager : MonoBehaviour
{

    public GameObject[] _roadSegmentsPrefabs;
    public Transform _newRoadAnchor;
    public Transform _topRoadBorder;
    public Transform _bottomRoadBorder;

    public int _roadLength = 4;

    private bool _gameIsRunning = true;
    private Random _randomNumberGen;
    private ArrayList _road;

    void Awake()
    {
        EventBus.DeleteLastRoadSegment.AddListener(DeleteLastRoadSegment);
        EventBus.SpawnRoadSegment.AddListener(SpawnNewRoadSegment);

    }

    void Start()
    {
        _randomNumberGen = new Random();
        _road = new ArrayList();
        InitiateRoad();
    }

    void InitiateRoad()
    {
        for (int i = 0; i < _roadLength; i++)
        {
            int randomIndex = Random.Range(0, _roadLength);
            GameObject go = (GameObject)Instantiate(_roadSegmentsPrefabs[randomIndex], _newRoadAnchor.position, Quaternion.identity);
            _road.Add(go);
        }
    }

    void SpawnNewRoadSegment()
    {
        int randomIndex = Random.Range(0, _roadLength);
        GameObject go = (GameObject)Instantiate(_roadSegmentsPrefabs[randomIndex], _newRoadAnchor.position, Quaternion.identity);
        _road.Add(go);

    }

    void DeleteLastRoadSegment()
    {

        GameObject go = (GameObject)_road[0];
        _road.RemoveAt(0);
        Destroy(go);
    }


}
