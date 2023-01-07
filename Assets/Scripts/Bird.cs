using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public GameObject ExplosionEffect;
    private GameController controller;
    private bool isExploded;
    public float fieldOfImpact;
    public float force;
    public LayerMask LayerToHit;
    private Rigidbody2D rig;

    void Start()
    {
        isExploded = false;
        controller = GameObject.Find("GameController").GetComponent<GameController>();
        rig = gameObject.GetComponent<Rigidbody2D>();
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
        if (GameObject.Find("bird(Clone)") != null)
        {
            Destroy(GameObject.Find("bird(Clone)"), 2);
        }
        if (GameObject.Find("Bomb(Clone)") != null)
        {
            Destroy(GameObject.Find("Bomb(Clone)"), 2);
        }
        if (GameObject.Find("big_brother(Clone)") != null)
        {
            Destroy(GameObject.Find("big_brother(Clone)"), 2);
        }
        controller.DisplaySoundBirdDestroy();
        isExploded = true;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0) && ExplosionEffect != null && !isExploded && rig != null && rig.velocity.magnitude > 0.1f){
            Explode();
            controller.DisplaySoundBirdExplosion();
            isExploded = true;
        }
    }

    void Explode(){
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, LayerToHit);
        foreach(Collider2D obj in objects){
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);

            if (obj.gameObject.CompareTag("Balloon"))
                Destroy(obj.gameObject);
        }

        GameObject ExplosionEffectIns = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        Destroy(ExplosionEffectIns, 10);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
    }

}
