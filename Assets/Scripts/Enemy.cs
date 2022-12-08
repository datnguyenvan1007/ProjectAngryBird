using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float delayTime;

    public GameObject deathEffect;
    public GameObject gameController;
    private GameController controller;
    private Rigidbody2D rig;
    private bool isDead = false;
    private Animator anim;

    public float health = 3f;
    float isAttacked = 0;

    private void Start()
    {
        controller = gameController.GetComponent<GameController>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null && !isDead)
        {
            if (collision.relativeVelocity.magnitude > 5)
            {
                isAttacked -= 1;
                anim.SetFloat("IsAttacked", isAttacked);
            }
            if (collision.gameObject.CompareTag("Wood") && collision.relativeVelocity.magnitude * collision.rigidbody.mass >= 3)
            {
                Die();
            }

            if (collision.relativeVelocity.magnitude > health && collision.gameObject.CompareTag("Player"))
            {
                Die();
            }
        }
        if (collision.gameObject.CompareTag("Ground") && !isDead)
        {
            if (collision.relativeVelocity.magnitude >= 2)
                Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject, delayTime);
        controller.enemyDead++;
        controller.DisplayScore();
        controller.DisplayEnemyAlive();
    }
}
