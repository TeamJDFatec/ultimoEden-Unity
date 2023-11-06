using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ChangePlayerController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    private bool isPlayer1Active;

    private Vector3 lastPosPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        player1.transform.parent = null;
        player2.transform.parent = player1.transform;

        isPlayer1Active = true;
        SetPlayerActive(isPlayer1Active);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isPlayer1Active = !isPlayer1Active;
            SetPlayerActive(isPlayer1Active);
        }

    }

    void SetPlayerActive(bool isPlayer1Active)
    {
        if (isPlayer1Active)
        {
            player1.GetComponent<YuriScript>().enabled = true;
            player2.GetComponent<MekaScript>().enabled = false;
            
            player1.GetComponent<BoxCollider2D>().enabled = true;
            player2.GetComponent<BoxCollider2D>().enabled = false;
            
            player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            
            player1.GetComponent<SpriteRenderer>().sortingOrder = 1;
            player2.GetComponent<SpriteRenderer>().sortingOrder = 0;

            player1.transform.parent = null;
            player2.transform.parent = player1.transform;

            if (player1.GetComponent<PlayerController>().GetDirection() == -1)
            {
                lastPosPlayer2 = player2.transform.position;
                player2.transform.position = player1.transform.position + new Vector3(0.8f, 0, 0);
                player1.transform.position = lastPosPlayer2;
            }
            else
            {
                lastPosPlayer2 = player2.transform.position;
                player2.transform.position = player1.transform.position + new Vector3(-0.8f, 0, 0);
                player1.transform.position = lastPosPlayer2;
            }
        }
        else
        {
            player2.GetComponent<MekaScript>().enabled = true;
            player1.GetComponent<YuriScript>().enabled = false;
            
            player2.GetComponent<BoxCollider2D>().enabled = true;
            player1.GetComponent<BoxCollider2D>().enabled = false;
            
            player2.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            
            player2.GetComponent<SpriteRenderer>().sortingOrder = 1;
            player1.GetComponent<SpriteRenderer>().sortingOrder = 0;

            player2.transform.parent = null;
            player1.transform.parent = player2.transform;

            if (player2.GetComponent<PlayerController>().GetDirection() == -1)
            {
                lastPosPlayer2 = player1.transform.position;
                player1.transform.position = player2.transform.position + new Vector3(0.8f, 0, 0);
                player2.transform.position = lastPosPlayer2;
            }
            else
            {
                lastPosPlayer2 = player1.transform.position;
                player1.transform.position = player2.transform.position + new Vector3(-0.8f, 0, 0);
                player2.transform.position = lastPosPlayer2;
            }
        }
    }
}
