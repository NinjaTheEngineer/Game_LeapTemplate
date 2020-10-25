using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerScript player;
    public GameManager gameManager;
    void Start()
    {
        player = gameObject.GetComponentInParent<PlayerScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision -- " + other.gameObject.tag);
        if (other.gameObject.tag == "Ground")
        {
            player.isGrounded = true;
            other.GetComponent<MeshRenderer>().material = GetComponentInParent<MeshRenderer>().material;
            FindObjectOfType<AudioManager>().PlaySound("ColorChanged");
            gameManager.IncreaseScore();
        }
        else if(other.gameObject.tag == "GroundInvis")
        {
            player.isGrounded = true;
        }else if(other.gameObject.tag == "Coin")
        {
            FindObjectOfType<AudioManager>().PlaySound("CoinCollected");
            Destroy(other.gameObject);
            gameManager.CollectCoin();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        player.isGrounded = false;
        Debug.Log("Exit collision -- " + other.gameObject.tag);
        /*if (other.gameObject.tag == "Ground")
        {
            player.isGrounded = false;
        }*/

    }
}
