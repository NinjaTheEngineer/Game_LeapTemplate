using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject normalRamp;
    float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            SpawnNormalRamp();
        }
    }

    void SpawnNormalRamp()
    {
        Instantiate(normalRamp, new Vector3(0, -3, 7), normalRamp.transform.rotation);
        timer = 0f;
    }
}
