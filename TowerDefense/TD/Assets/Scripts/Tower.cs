using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

    // Damage dealt
    public int damage;

    // Range
    public float range;

    // Fire rate
    public float fireRate;

    // Gold cost to build
    public int cost;


    SphereCollider sc;
    void Start() {

        //sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;
        sc = GetComponent<SphereCollider>();
        SetRange(range);
    }

    public void SetRange(float range) {
        sc.radius = range;
        transform.GetChild(0).localScale = new Vector3(range * 2.0f, range * 2.0f, range * 2.0f);
    }



    public virtual void RemoveEntry(Collider other) { }

}