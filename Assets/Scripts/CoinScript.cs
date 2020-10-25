using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float yToAdd = 0.5f;
    public float zToAdd = 0.5f;
    public GameObject target;
    public Vector3 targetPos;

    public bool onTop = false;

    public void Update()
    {

        if (onTop)
        {
            targetPos = target.gameObject.transform.position;
            transform.position = new Vector3(targetPos.x, targetPos.y + yToAdd, targetPos.z + zToAdd);
        }

        if (transform.position.z < -9f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("COIN COIN COIN");
            target = other.gameObject;
            onTop = true;
        }
    }

}
