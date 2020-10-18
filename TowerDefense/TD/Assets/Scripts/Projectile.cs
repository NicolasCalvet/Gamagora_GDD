using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private int damage;
    private Collider target;
    private float speed;

    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }

    public void Initialize(int dmg, Collider targ, float spd) {
        this.damage = dmg;
        this.target = targ;
        this.speed = spd;
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }

}
