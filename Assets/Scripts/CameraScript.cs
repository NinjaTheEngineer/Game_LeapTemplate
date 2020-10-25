using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform spawner;
    private Transform player;
    public float rotationSpeed = 0.3f;
    float timer = 0f;

    [SerializeField] private Vector3 positionToMove;
    [SerializeField] float movementSpeedX = 0.5f;
    void Start()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Transform>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(spawner.position - transform.position), rotationSpeed * Time.deltaTime);

        if(transform.position.x < player.position.x + 1.5f || transform.position.x > player.position.x - 1.5f)
        {
            MoveWithPlayer();
        }
    }

    private void MoveWithPlayer()
    {
        positionToMove = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, positionToMove, movementSpeedX * Time.deltaTime);
    }
}
