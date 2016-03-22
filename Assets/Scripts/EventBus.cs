using UnityEngine;

public static class EventBus
{
    public static Signal ShowMainMenu = new Signal();

    public static Signal SpawnRoadSegment = new Signal();
    public static Signal DeleteLastRoadSegment = new Signal();

    public static Signal GameOver = new Signal();
    public static Signal LevelPaused = new Signal();
    public static Signal LevelStarted = new Signal();
    public static Signal LevelUnPaused = new Signal();



}
