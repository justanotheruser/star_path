using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public GameObject linePrefab;
    public float newPointThreshold = .1f;

    private PlayerPath _currentPath;
    private bool _isDrawingPath;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_currentPath.points.Count == 0)
                return;

            Vector2 curFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lastPoint = _currentPath.points[_currentPath.points.Count - 1];
            if (Vector2.Distance(curFingerPos, lastPoint) > newPointThreshold)
                _currentPath.AddPoint(curFingerPos, Time.deltaTime);
            else
                _currentPath.StayStill(Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        if (!_isDrawingPath)
        {
            Debug.Log("Start path");
            DestroyAllPathes();
            _currentPath = Instantiate(linePrefab, Vector3.zero, Quaternion.identity).GetComponent<PlayerPath>();
            _isDrawingPath = true;
        }
    }

    void DestroyAllPathes()
    {
        Debug.Log("Destroy all pathes");
        var pathes = GameObject.FindGameObjectsWithTag("PlayerPath");
        for (int i = 0; i < pathes.Length; i++)
        {
            Destroy(pathes[i]);
        }
    }

    public PlayerPath GetPlayersPath()
    {
        return _currentPath;
    }

}
