using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject ExplosionEffect;
    private GameController controller;

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
    public float fieldofImpact;
    public float force;
    public LayerMask LayerToHit;
    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            explode();
            // speedup();
            controller.DisplaySoundBirdExplosion();
        }
    }
    // Vector3 InitialPos;
    // void speedup(){
    //     Vector3 vectorForce = transform.position;
    //     GetComponent<Rigidbody2D>().AddForce(vectorForce * 1000);
    //     GetComponent<Rigidbody2D>().gravityScale = 1;
    // }

    void explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofImpact, LayerToHit);
        foreach(Collider2D obj in objects){
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction*force);
        }

        GameObject ExplosionEffectIns = Instantiate(ExplosionEffect, transform.position,Quaternion.identity);
        Destroy(ExplosionEffectIns,10);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,fieldofImpact);
    }


}
