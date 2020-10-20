using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ion : Tower {

    //Damage is percentage reduction


    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().speed = other.gameObject.GetComponent<Enemy>().normalSpeed - other.gameObject.GetComponent<Enemy>().normalSpeed * damage / 100;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.gameObject.GetComponent<Enemy>().speed = other.gameObject.GetComponent<Enemy>().normalSpeed;
        }
    }

    public override void Upgrade() {
        base.Upgrade();

        damage += damageUpgrade;
        range += rangeUpgrade;

    }

}
