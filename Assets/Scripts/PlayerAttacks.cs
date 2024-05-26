using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public float attack_cooldown;
    private float last_attack;
    public float bullet_lifetime;
    public GameObject bullet_prefab;
    public CooldownBar cooldown_bar;

    public bool shotgun_enabled;
    public float pellet_attack_cooldown;
    private float pellet_last_attack;
    public GameObject pellet_prefab;
    public CooldownBar pellet_cooldown_bar;
    // Start is called before the first frame update
    void Start()
    {
        cooldown_bar.SetCooldownMax((attack_cooldown));
        pellet_cooldown_bar.SetCooldownMax(pellet_attack_cooldown);
        pellet_cooldown_bar.SetCooldown(0);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown_bar.SetCooldown(last_attack + attack_cooldown - Time.time);
        pellet_cooldown_bar.SetCooldown(pellet_last_attack + pellet_attack_cooldown - Time.time);
        FireControl();
    }

    private void FireControl()
    {
        if (Time.time > last_attack + attack_cooldown)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                GameObject bullet = Instantiate(bullet_prefab);
                bullet.GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.position = transform.position;
                last_attack = Time.time;
            }
        }
        //shotgun cooldown
        if (shotgun_enabled && Time.time > pellet_last_attack + pellet_attack_cooldown)
        {
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
            {
                GameObject bullet = Instantiate(pellet_prefab);
                bullet.GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.GetChild(0).GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.GetChild(1).GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.GetChild(2).GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.GetChild(3).GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.GetChild(4).GetComponent<BulletControl>().life_limit = bullet_lifetime + Time.time;
                bullet.transform.position = transform.position;
                pellet_last_attack = Time.time;
            }
        }
    }
}
