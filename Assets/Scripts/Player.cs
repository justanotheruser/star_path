using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pathControllerPrefab;

    private Rigidbody2D _rb2D;
    private PathController _pathController;
    private bool _isLaunched = false;
    private PlayerPath _path;

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if (_rb2D == null)
        {
            Debug.LogError("Rigid body could not be found");
        }
        if (pathControllerPrefab == null)
        {
            Debug.Log("Path controller is not set");
        }
        _pathController = pathControllerPrefab.GetComponent<PathController>();
        if (_pathController == null)
        {
            Debug.Log("Path controller couldn't be found");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!_isLaunched)
            {
                Launch();
            }
        }
    }

    void Launch()
    {
        Debug.Log("Launch player");
        if (_pathController == null)
        {
            Debug.LogError("Path controller could not be found");
            return;
        }

        _path = _pathController.GetPlayersPath();
        if (_path.points == null || _path.points.Count == 0)
        {
            Debug.Log("No path to follow");
            return;
        }
        
        Debug.Log("Start following path");
        _isLaunched = true;
        StartCoroutine(SmoothMovement(2));
    }

    protected IEnumerator SmoothMovement(int pointIdx)
    {
        Vector3 end = _path.points[pointIdx];
        float stillnessTime = _path.stillnessTimes[pointIdx-2];
        float inverseMoveTime = 1 / _path.moveTimes[pointIdx-2];
        yield return new WaitForSeconds(stillnessTime);

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            var newPosition = Vector3.MoveTowards(_rb2D.position, end, inverseMoveTime * Time.deltaTime);
            _rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        int nextPointIdx = pointIdx + 1;
        if (nextPointIdx < _path.points.Count)
        {
            StartCoroutine(SmoothMovement(nextPointIdx));
        }
    }
}
