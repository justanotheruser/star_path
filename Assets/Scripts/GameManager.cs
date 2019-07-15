using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float degreesPerSecond;

    private 

    void Awake()
    {
        if (player == null)
            Debug.Log("Player is not set");


    }

    void Update()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        for (int i = 0; i < obstacles.Length; i++)
        {
            //StartCoroutine(RotateBlackHole(obstacles[i]));
            var spriteRenderer = obstacles[i].GetComponent<SpriteRenderer>();
            spriteRenderer.transform.Rotate(Vector3.forward * degreesPerSecond * Time.deltaTime);
        }
    }

    /*IEnumerator RotateBlackHole(GameObject blackHole)
    {
        Debug.Log("Rotate black hole");
        var spriteRenderer = blackHole.GetComponent<SpriteRenderer>();
        spriteRenderer.transform.Rotate(Vector3.forward * rotationSpeed / 360 * Time.deltaTime);
        yield return new WaitForSeconds(1 / rotationSpeed * Time.deltaTime);
    }*/
}
