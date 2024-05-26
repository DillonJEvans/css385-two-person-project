using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    public Camera camera;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    public GameObject asteroid_prefab;
    public float spawn_rate;
    private float spawn_time;
    private float x_bounds;
    public ScoreTracker scoreTracker;

    public int health_factor = 3;
    // Start is called before the first frame update
    void Start()
    {
        x_bounds = camera.ViewportToWorldPoint(new Vector3(1,1,0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawn_time + spawn_rate)
        {
            SpawnAsteroid();
            spawn_time = Time.time + Random.Range(-0.25f, 0.25f);
        }
    }

    private void SpawnAsteroid()
    {
        GameObject asteroid = Instantiate(asteroid_prefab);
        asteroid.transform.position = new Vector3(Random.Range(-x_bounds, x_bounds), transform.position.y, transform.position.z);
        asteroid.transform.rotation = new Quaternion(0, 0, Random.Range(-10, 10), 0);
        float scale = Random.Range(1, 4);
        asteroid.transform.localScale = new Vector3(scale, scale, 0);
        asteroid.GetComponent<Health>().max_health = (int) scale * health_factor;
        asteroid.GetComponent<Health>().health = (int) scale * health_factor;
        asteroid.GetComponent<Health>().scoreTracker = scoreTracker;
    }
}
