using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampScript : MonoBehaviour
{
    public int id = 0;
    [SerializeField] private float speed = 1.0f;
    Color[] colors = new Color[6];

    private Transform coinLocation;
    private GameObject coinPrefab;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (id.Equals(1))
        {
            return;
        }

        if(Random.Range(0, 100) < 5)
        {
            coinLocation = GetComponentInChildren<Transform>();
            coinPrefab = gameManager.coinPrefab;
            Instantiate(coinPrefab, coinLocation.position, coinPrefab.transform.rotation);
        }

        colors[0] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colors[1] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colors[2] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colors[3] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colors[4] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        colors[5] = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
    }

    void Update()
    {
        if(id.Equals(1) && !gameManager.GameOnline())
        {
            return;
        }
        if (id.Equals(1))
        {
            transform.Translate(new Vector3(0, 2f, -4) * Time.deltaTime * 0.7f);
        }
        else
        {
            transform.Translate(new Vector3(0, 2f, -4) * Time.deltaTime * speed);
        }

        if (transform.position.z < -13f) {
            Destroy(this.gameObject);
        }
    }
}
