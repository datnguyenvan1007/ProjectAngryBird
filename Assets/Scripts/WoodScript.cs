using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    private float woodHealth = 20f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
            if (collision.relativeVelocity.magnitude * collision.rigidbody.mass >= woodHealth)
            {
                Destroy(gameObject);
                controller.DisplaySoundDestroyWood();
            }
    }
}
