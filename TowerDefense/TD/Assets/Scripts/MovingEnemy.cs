using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{

    // Contains values usefull such as : speed
    private Enemy enemyType;

    // Reference to the Level
    // Contains waypoint informations
    public Level level;

    // Current destination waypoint
    private int currentWaypoint = 0;




    public void SetLevel(Level l) {
        level = l;
    }


    // Start is called before the first frame update
    void Start()
    {
        enemyType = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {

        // Need to check if we reached current waypoint
        if (level.waypoints[currentWaypoint].transform.position.Equals(transform.position)) {
            // If true, increment index
            ++currentWaypoint;
        }

        // Need to check if we reached base (last waypoint)
        if (currentWaypoint == level.waypoints.Count) {
            // We reached base

            // Decrease base HP
            GameObject.Find("GameManager").GetComponent<GameManager>().ApplyDamage(enemyType.damage);

            // Destroy Unit
            Destroy(gameObject);

        } else {

            // Else, moving to the next waypoint
            transform.position = Vector3.MoveTowards(transform.position, level.waypoints[currentWaypoint].transform.position, enemyType.speed * Time.deltaTime);
        }
    }
}
