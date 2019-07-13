using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public float newPointThreshold = .1f;

    // Line should always start from a player
    private GameObject player;
    private LineRenderer lineRenderer;
    private GameObject currentLine;
    private List<Vector2> fingerPositions;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        fingerPositions = new List<Vector2>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed left mouse button");
            StartLine();
        }
        if (Input.GetMouseButton(0))
        {
            Vector2 curFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(curFingerPos, fingerPositions[fingerPositions.Count - 1]) > newPointThreshold)
            {
                UpdateLine(curFingerPos);
            }
        }
    }

    void StartLine()
    {
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        fingerPositions.Clear();

        Vector2 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(GetFirstPoint(currentPosition));
        fingerPositions.Add(currentPosition);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }

    Vector2 GetFirstPoint(Vector2 defaultPosition)
    {
        if (player == null)
            return defaultPosition;

        return player.transform.position;
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newFingerPos);
    }
}
