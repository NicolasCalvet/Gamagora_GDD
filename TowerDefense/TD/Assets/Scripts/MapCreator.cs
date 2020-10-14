using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    
    // Grid data
    public GameObject gridContainer;
    public int xAxisSize = 16;
    public int zAxisSize = 16;

    // Grid
    private Cell[,] grid;

    // Terrain tiles
    public GameObject cell;

    // Level data
    public Level level;


    // Start is called before the first frame update
    void Start() {

        //Create first level here BUT it could be done in another way (reading config file or procedural generating)
        createFirstLevel();
        CreateMap();
    }

    private void createFirstLevel() {

        level = new Level();

        // Path
        level.pathNodes = new List<Vector2>();

        level.pathNodes.Add(new Vector2(9, 0));
        level.pathNodes.Add(new Vector2(9, 1));
        level.pathNodes.Add(new Vector2(9, 2));
        level.pathNodes.Add(new Vector2(8, 2));
        level.pathNodes.Add(new Vector2(7, 2));
        level.pathNodes.Add(new Vector2(7, 3));
        level.pathNodes.Add(new Vector2(7, 4));
        level.pathNodes.Add(new Vector2(7, 5));
        level.pathNodes.Add(new Vector2(7, 6));
        level.pathNodes.Add(new Vector2(8, 6));
        level.pathNodes.Add(new Vector2(9, 6));
        level.pathNodes.Add(new Vector2(9, 7));
        level.pathNodes.Add(new Vector2(9, 8));
        level.pathNodes.Add(new Vector2(8, 8));
        level.pathNodes.Add(new Vector2(7, 8));
        level.pathNodes.Add(new Vector2(6, 8));
        level.pathNodes.Add(new Vector2(5, 8));
        level.pathNodes.Add(new Vector2(4, 8));
        level.pathNodes.Add(new Vector2(4, 9));
        level.pathNodes.Add(new Vector2(4, 10));
        level.pathNodes.Add(new Vector2(4, 11));
        level.pathNodes.Add(new Vector2(4, 12));
        level.pathNodes.Add(new Vector2(5, 12));
        level.pathNodes.Add(new Vector2(6, 12));
        level.pathNodes.Add(new Vector2(7, 12));
        level.pathNodes.Add(new Vector2(7, 13));

        // Base
        level.basePos = new Vector2(7, 14);
    }

    private void CreateMap() {

        grid = new Cell[xAxisSize, zAxisSize];

        // Setting every cell as grass
        for (int i = 0; i < xAxisSize; i++) {
            for (int j = 0; j < zAxisSize; j++) {
                grid[i, j] = Instantiate(cell, new Vector3(i + 0.5f, 0, j + 0.5f), Quaternion.identity, gridContainer.transform).GetComponent<Cell>();
            }
        }

        // Setting path cells
        foreach (Vector2 node in level.pathNodes) {
            SetGrid((int)node.x, (int)node.y, CellType.Path);
        }

        // Setting base cell
        SetGrid((int)level.basePos.x, (int)level.basePos.y, CellType.Base);

    }

    void SetGrid(int i, int j, CellType type) {
        grid[i, j].type = type;
        grid[i, j].ChangeMaterial();
    }


}
