using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public GameObject unitPrefab;
    public int numberToSpawn;

    public Wave(GameObject unit, int nb) {
        this.unitPrefab = unit;
        this.numberToSpawn = nb;
    }
}
