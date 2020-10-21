using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material grassMat;
    public Material pathMat;
    public Material baseMat;
    public Material hoverMat;

    public CellType type;
    private bool isSelected;

    public Tower tower;

    public bool HasTower { get; set; }

    void Start() {
        HasTower = false;
        isSelected = false;
        tower = null;
    }

    void OnMouseOver() {
        if (type == CellType.Grass) {
            GetComponent<MeshRenderer>().material = hoverMat;
        }
    }

    void OnMouseExit() {
        ChangeMaterial();
    }

    void OnMouseDown() {
        if (type != CellType.Grass) {
            Debug.Log("Can only buy on a grass cell");
            return;
        }
        if (!HasTower || !tower.IsUpgraded) {
            GameObject.Find("GameManager").GetComponent<GameManager>().SelectCell(this);
            isSelected = true;
        }
    }

    public void ChangeMaterial() {
        switch (type) {
            case CellType.Grass:
                GetComponent<MeshRenderer>().material = grassMat;
                if (isSelected) {
                    GetComponent<MeshRenderer>().material = hoverMat;
                }
                break;
            case CellType.Path:
                GetComponent<MeshRenderer>().material = pathMat;
                break;
            case CellType.Base:
                GetComponent<MeshRenderer>().material = baseMat;
                break;
        }
    }

    public void Unselect() {
        isSelected = false;
        ChangeMaterial();
    }

    public void SetTower(Tower tw) {
        this.HasTower = true;
        this.tower = tw;
    }

}
