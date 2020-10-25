using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public Transform target;

    public GameManager gameManager;

    private float timer = 0f;

    private float speed;
    private float left;
    private float right;

    private float timerToChangePos;
    private float xToMove;

    void Start()
    {
        speed = gameManager.GetSpawnerSpeed();
        left = -(gameManager.GetSpawnerMaxDistance());
        right = gameManager.GetSpawnerMaxDistance();
        timerToChangePos = gameManager.GetTimeBetweenPositionChange(); 
        xToMove = Random.Range(left, right);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timerToChangePos)
        {
            xToMove = Random.Range(left, right);
            timer = 0;
        }

        transform.position = Vector3.Lerp(transform.position, new Vector3( xToMove, transform.position.y, transform.position.z), Time.deltaTime * speed);
    }


    public float GetAngleDir()
    {
        Vector3 perp = Vector3.Cross(transform.position, target.position);
        float dir = Vector3.Dot(perp, Vector3.up);
        return dir;
    }
}
