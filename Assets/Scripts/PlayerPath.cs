using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    public List<Vector2> points;
    // i-th element is equal to time it should take to move from points[i-1] to points[i]
    public List<float> moveTimes;
    // i-th element is equal to time of rest on point[i]
    public List<float> stillnessTimes;
    // Minimum distance between current cursor position and current point to create a new point
    public float newPointThreshold = .1f;

    private LineRenderer lineRenderer;
    private GameObject player;
    private float _stillnessTime;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.Log("Player couldn't be found");
        }
    }

    void Start()
    {
        Debug.Log("New path created");
        points = new List<Vector2>();
        moveTimes = new List<float>();
        stillnessTimes = new List<float>();

        Vector2 playerStartPosition = player.transform.position;
        Debug.Log(string.Format("Player start position: {0}", playerStartPosition));
        points.Add(playerStartPosition);
        lineRenderer.SetPosition(0, points[0]);
        points.Add(playerStartPosition);
        lineRenderer.SetPosition(1, points[1]);
    }

    public void AddPoint(Vector2 point, float moveTime)
    {
        points.Add(point);
        moveTimes.Add(moveTime);
        stillnessTimes.Add(_stillnessTime);
        _stillnessTime = 0;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }

    public void StayStill(float time)
    {
        _stillnessTime += time;
    }

    public void Clear()
    {
        points.Clear();
        moveTimes.Clear();
        stillnessTimes.Clear();
    }
}
