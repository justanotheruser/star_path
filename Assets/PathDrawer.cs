using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public GameObject linePrefab;
    public float newPointThreshold = .1f;

    private LineRenderer lineRenderer;
    private GameObject currentLine;
    private List<Vector2> fingerPositions;

    // Start is called before the first frame update
    void Start()
    {
        fingerPositions = new List<Vector2>();
    }

    // Update is called once per frame
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

        Vector2 firstPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        fingerPositions.Add(firstPoint);
        fingerPositions.Add(firstPoint);
        lineRenderer.SetPosition(0, fingerPositions[0]);
        lineRenderer.SetPosition(1, fingerPositions[1]);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        fingerPositions.Add(newFingerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount-1, newFingerPos);
    }
}
