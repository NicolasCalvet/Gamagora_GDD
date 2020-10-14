using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material grassMat;
    public Material pathMat;
    public Material baseMat;

    public CellType type;


    public void ChangeMaterial() {
        switch (type) {
            case CellType.Grass:
                GetComponent<MeshRenderer>().material = grassMat;
                break;
            case CellType.Path:
                GetComponent<MeshRenderer>().material = pathMat;
                break;
            case CellType.Base:
                GetComponent<MeshRenderer>().material = baseMat;
                break;
        }
    }
}
