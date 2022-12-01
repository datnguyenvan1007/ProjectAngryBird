using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private GameController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wood"))
        {
            controller.DisplaySoundWoodCollision();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            controller.DisplaySoundBirdCollideEnemy();   
        }
        Destroy(GameObject.Find("bird(Clone)"), 2);
        controller.DisplaySoundBirdDestroy();
    }

}
