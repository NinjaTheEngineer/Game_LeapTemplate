using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, 2f, -4) * Time.deltaTime * 1.5f);
    }

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }
}
