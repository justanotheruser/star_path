using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PathController pathController;

    private Rigidbody2D _rb2D;
    private bool _isLaunched = false;
    private List<Vector2> _path;
    

    void Awake()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        if (_rb2D == null)
        {
            Debug.LogError("Rigit body could not be found");
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
        Debug.Log("Space pressed");
        if (pathController == null)
        {
            Debug.LogError("Path controller could not be found");
            return;
        }

        _path = pathController.GetPlayersPath().points;
        if (_path == null || _path.Count == 0)
        {
            Debug.Log("No path to follow");
            return;
        }
        
        Debug.Log("Start following path");
        _isLaunched = true;
        StartCoroutine(SmoothMovement(0, 1f));
    }

    protected IEnumerator SmoothMovement(int pointIdx, float inverseMoveTime)
    {
        Vector3 end = _path[pointIdx];
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            var newPosition = Vector3.MoveTowards(_rb2D.position, end, inverseMoveTime * Time.deltaTime);
            _rb2D.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        int nextPointIdx = pointIdx + 1;
        if (nextPointIdx < _path.Count)
        {
            StartCoroutine(SmoothMovement(nextPointIdx, inverseMoveTime));
        }
    }
}
