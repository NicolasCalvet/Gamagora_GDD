using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Damage dealt to base
    public int damage;

    // Speed of the unit
    public float speed;

    // Fold given at the death of the unit
    public int goldGivedDeath;

    // Life points
    public int HP;

    // Frequency between each unit
    public float unitFrequency;
}
