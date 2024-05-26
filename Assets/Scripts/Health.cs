using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public int max_health;

    public float damage_cooldown = 0.5f;
    public float damage_time;

    private float damage_flash_length = 0.25f;
    bool flash_active = false;

    public ScoreTracker scoreTracker;

    private void Start()
    {
        health = max_health;
    }

    private void Update()
    {
        if (flash_active && gameObject.layer == 8 && Time.time > damage_flash_length + damage_time)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (flash_active && gameObject.layer == 6 && Time.time > damage_flash_length + damage_time)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }

    public void TakeDamage()
    {
        health--;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        flash_active = true;
        damage_time = Time.time;
        if (health <= 0)
        {
            if (gameObject.layer == 6)
            {
                scoreTracker.IncreaseScore(max_health);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.layer == 8 && Time.time > damage_cooldown + damage_time)
        {
            TakeDamage();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.layer == 8 && Time.time > damage_cooldown + damage_time)
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
