using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum RampPosition{LEFT, MID, RIGHT}
    RampPosition pos;
    RampPosition secondPos;

    public GameObject normalRamp;
    float timer = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.75f)
        {
            PickPosition();
            SpawnNormalRamp(pos, secondPos);
        }
    }

    void SpawnNormalRamp(RampPosition pos, RampPosition pos2)
    {
        if (pos == RampPosition.LEFT || pos2 == RampPosition.LEFT)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(-2.2f, -1.6f), -3, 7), normalRamp.transform.rotation);
        }
        if (pos == RampPosition.MID || pos2 == RampPosition.MID)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(0.3f, 0.3f), -3, 7), normalRamp.transform.rotation);
        }
        if (pos == RampPosition.RIGHT || pos2 == RampPosition.RIGHT)
        {
            Instantiate(normalRamp, new Vector3(Random.Range(1.6f, 2.2f), -3, 7), normalRamp.transform.rotation);
        }
        timer = 0f;
    }

    void PickPosition()
    {
        pos = GetRandomEnum<RampPosition>();
        secondPos = GetRandomEnum<RampPosition>();
        while(secondPos == pos)
        {
            secondPos = GetRandomEnum<RampPosition>();
        }
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }
}
