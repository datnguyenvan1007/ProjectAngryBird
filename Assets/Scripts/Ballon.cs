using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public GameObject gameController;
    private GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controller.DisplaySoundBallonCollion();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude * collision.rigidbody.mass > collision.rigidbody.mass)
        {
            controller.DisplaySoundBallonCollion();
            Destroy(gameObject);
        }
    }
}
