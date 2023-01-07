using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
    public float woodHealth = 20f;

    public GameObject gameController;
    private GameController controller;
    float isAttacked = 1f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameController.GetComponent<GameController>();
        anim = gameObject.GetComponent<Animator>();
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
                controller.WoodBroken++;
                Destroy(gameObject);
                controller.DisplaySoundDestroyWood();
            }
        if (collision.relativeVelocity.magnitude > 3)
        {
            isAttacked *= -1;
            anim.SetFloat("IsAttacked", isAttacked);
        }
    }
}
