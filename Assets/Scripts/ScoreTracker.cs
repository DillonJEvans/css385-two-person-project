using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public int display_time;
    public int score;
    public int shotgun_score;
    public PlayerAttacks attacks;

    public CooldownBar display_health;
    public Health player_health;
    public Text score_time;

    public AsteroidManager asteroidManager;
    // Start is called before the first frame update
    void Start()
    {
        display_health.SetCooldownMax(player_health.max_health);
        display_health.SetCooldown(player_health.max_health);
    }

    // Update is called once per frame
    void Update()
    {
        display_time = (int) Time.time;
        UpdateScoreTime();
        UpdateHealthbar();
        if (score > shotgun_score && attacks.shotgun_enabled == false)
        {
            attacks.shotgun_enabled = true;
            asteroidManager.health_factor = 12;
            asteroidManager.spawn_rate = 0.4f;
        }
    }
    
    public void IncreaseScore(int addition)
    {
        score += addition;
    }

    public void UpdateHealthbar()
    {
        display_health.SetCooldown(player_health.health);
    }

    private void UpdateScoreTime()
    {
        score_time.text = " Score: " + score + "\r\n\r\n Time: " + display_time + "\r\n\r\n Shotgun Unlocked: \n " + attacks.shotgun_enabled;
    }
}
