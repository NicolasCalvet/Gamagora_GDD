using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Damage dealt to base
    public int damage;

    // Speed of the unit
    public float normalSpeed;
    public float speed;


    void Start() {
        speed = normalSpeed;
    }


    public void ReceiveDamage(int damage) {

        HP -= damage;

        if (HP <= 0) {
            GameObject.Find("GameManager").GetComponent<GameManager>().GiveGold(goldGivedDeath);

            Tower[] towers = GameObject.FindObjectsOfType<Tower>();

            foreach (Tower t in towers) {
                t.RemoveEntry(GetComponent<Collider>());
            }

            Destroy(gameObject);
        }
    }

    // Fold given at the death of the unit
    public int goldGivedDeath;

    // Life points
    public int HP;

    // Frequency between each unit
    public float unitFrequency;
}
