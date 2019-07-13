using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Is responsible for drawing and storing path, keeping it always single and always starting from player
public class PathController : MonoBehaviour
{
    public GameObject linePrefab;
    public float newPointThreshold = .1f;

    private GameObject player;
    private LineRenderer lineRenderer;
    private GameObject currentLine;
    private PlayersPath _path;

    public PlayersPath GetPlayersPath()
    {
        return _path;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        _path = new PlayersPath();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left mouse button");
            DestroyAllPathes();
            StartLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 curFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(curFingerPos, _path.points[_path.points.Count - 1]) > newPointThreshold)
            {
                UpdateLine(curFingerPos);
            }
        }
    }

    void DestroyAllPathes()
    {
        var pathes = GameObject.FindGameObjectsWithTag("PlayerPath");
        for (int i = 0; i < pathes.Length; i++)
        {
            Destroy(pathes[i]);
        }
    }

    void StartLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        _path.Clear();

        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _path.points.Add(GetFirstPoint(currentPosition));
        _path.points.Add(currentPosition);
        lineRenderer.SetPosition(0, _path.points[0]);
        lineRenderer.SetPosition(1, _path.points[1]);
    }

    Vector2 GetFirstPoint(Vector2 defaultPosition)
    {
        if (player == null)
            return defaultPosition;

        return player.transform.position;
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        _path.points.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newFingerPos);
    }
}
