using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Canon : Tower {

    public GameObject projectile;
    public float projectileSpeed;

    private List<Collider> colliders = new List<Collider>();
    private Collider target;


    public float counter = 0f;

    void Update() {

        counter += Time.deltaTime;

        if (counter >= fireRate) {

            if (target != null) {

                Vector3 direction = target.transform.position - transform.position;

                GameObject unit = Instantiate(projectile, transform.position, Quaternion.identity);
                unit.GetComponent<Projectile>().Initialize(damage, target, projectileSpeed);
            }

            counter = 0;
        }

    }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy") && !colliders.Contains(other)) {
            colliders.Add(other);
            if (target == null) {
                target = other;
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        RemoveEntry(other);
    }

    public override void RemoveEntry(Collider other) {

        colliders.Remove(other);

        if (target.Equals(other)) {

            if (colliders.Count != 0) {
                target = colliders[0];
            } else {
                target = null;
            }

        }
    }

}
