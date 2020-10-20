using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlammeThrower : Tower
{
    public float counter = 0f;

    protected List<Collider> colliders = new List<Collider>();


    void Update() {

        counter += Time.deltaTime;

        if (counter >= fireRate) {

            foreach (Collider goCollider in colliders.ToList()) {
                goCollider.GetComponent<Enemy>().ReceiveDamage(this.damage);
            }

            counter = 0;
        }

    }


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy") && !colliders.Contains(other)) {
            colliders.Add(other);
        }
    }

    public void OnTriggerExit(Collider other) {
        RemoveEntry(other);
    }

    public override void RemoveEntry(Collider other) {
        base.RemoveEntry(other);
        colliders.Remove(other);
    }

    public override void Upgrade() {
        base.Upgrade();

        damage += damageUpgrade;
        range += rangeUpgrade;

    }

}
