using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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

    // Enemy prefabs
    public GameObject jeep;
    public GameObject truck;
    public GameObject vbtt;

    // Waypoints
    public GameObject waypointContainer;
    public GameObject waypoint;

    // Spawner
    public float timer = 0f;
    public float counter = 0f;
    private int nbRemainingToSpawn;

    // Waves
    public float timeBetweenWaves = 10.0f;
    private int waveIndex = 0;
    private bool allSpawned = false;

    // Tower prefabs
    public GameObject canon;
    public GameObject flammeThrower;
    public GameObject ion;



    public int gold;
    public int life;


    public TextMeshProUGUI baseLifeText;
    public TextMeshProUGUI goldText;

    public TextMeshProUGUI speedButtonText;

    /* BUTTONS */
    public GameObject buyCanonButton;
    public GameObject buyFTButton;
    public GameObject buyIonButton;

    public GameObject buyUpgradeButton;


    private Cell selectedCell;


    // Start is called before the first frame update
    void Start() {

        //Create first level here BUT it could be done in another way (reading config file or procedural generating)
        CreateFirstLevel();
        CreateMap();

        timer = timeBetweenWaves;
        nbRemainingToSpawn = level.waves[waveIndex].numberToSpawn;

        baseLifeText.text = life.ToString();
        goldText.text = gold.ToString();

        selectedCell = null;

        //Instantiate(flammeThrower, new Vector3(8.5f, 0.5f, 7.5f), flammeThrower.transform.rotation);
        //Instantiate(flammeThrower, new Vector3(7.5f, 0.5f, 7.5f), flammeThrower.transform.rotation);


    }


    void Update() {

        if (!allSpawned) {

            if (nbRemainingToSpawn == 0) {
                ++waveIndex;

                if (waveIndex >= level.waves.Count) {
                    allSpawned = true;
                } else {
                    nbRemainingToSpawn = level.waves[waveIndex].numberToSpawn;
                    timer = timeBetweenWaves;
                }
            }

            // Waves remaining
            counter += Time.deltaTime;

            if (counter >= timer) {

                // Spawn unit
                SpawnEnemy(level.waves[waveIndex].unitPrefab);

                // Decrement nbRemainingToSpawn
                --nbRemainingToSpawn;

                // Set timer
                timer = level.waves[waveIndex].unitPrefab.GetComponent<Enemy>().unitFrequency;

                // Set counter to deltaTime
                counter = 0;
            }

        }

    }



    public void ChangeSpeed() {

        if (speedButtonText.text == "x1") {
            Time.timeScale = 2;
            speedButtonText.text = "x2";

        } else if (speedButtonText.text == "x2") {
            Time.timeScale = 4;
            speedButtonText.text = "x4";

        } else if (speedButtonText.text == "x4") {
            Time.timeScale = 8;
            speedButtonText.text = "x8";

        } else if (speedButtonText.text == "x8") {
            Time.timeScale = 1;
            speedButtonText.text = "x1";
        }

    }


    public void SelectCell(Cell cell) {
        if (cell.HasTower) {
            Debug.Log("This cell has already a tower");
            return;
        }

        if (selectedCell != null) {
            selectedCell.Unselect();
        }

        selectedCell = cell;
        buyCanonButton.SetActive(true);
        buyFTButton.SetActive(true);
        buyIonButton.SetActive(true);
    }

    public void BuyTower(GameObject tower) {

        if (selectedCell == null) {
            //Print message no selected cell
            Debug.Log("No Selected Cell !");
            return;
        }

        if (gold < tower.GetComponent<Tower>().cost) {
            //Print message not enough money
            Debug.Log("Not enough money !");
            return;
        }

        UseGold(tower.GetComponent<Tower>().cost);

        Vector3 pos = selectedCell.transform.position;
        pos.y = 0.5f;

        GameObject towerCreated = Instantiate(tower, pos, tower.transform.rotation);

        // Set selectedCell to null
        selectedCell.SetTower(towerCreated.GetComponent<Tower>());
        selectedCell.Unselect();
        selectedCell = null;

        buyCanonButton.SetActive(false);
        buyFTButton.SetActive(false);
        buyIonButton.SetActive(false);
    }

    public void BuyUpgrade() {

        buyUpgradeButton.SetActive(false);
    }

    public void UseGold(int goldGivedDeath) {
        gold -= goldGivedDeath;
        goldText.text = gold.ToString();
    }

    public void GiveGold(int goldGivedDeath) {
        gold += goldGivedDeath;
        goldText.text = gold.ToString();
    }

    public void ApplyDamage(int quantity) {
        life -= quantity;
        baseLifeText.text = life.ToString();
        if (life <= 0) {
            PlayerLose();
        }
    }

    void PlayerLose() {
        Time.timeScale = 0;
        Debug.Log("Player LOSE");
    }


    private void SpawnEnemy(GameObject unitPrefeb) {
        GameObject unit = Instantiate(unitPrefeb, level.waypoints[0].transform.position, unitPrefeb.transform.rotation);
        unit.GetComponent<MovingEnemy>().SetLevel(level);
    }


    private void CreateFirstLevel() {

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

        // Waypoints
        level.waypoints = new List<GameObject>();

        level.waypoints.Add(Instantiate(waypoint, new Vector3(9.5f, 0.5f, 0), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(9.5f, 0.5f, 2.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(7.5f, 0.5f, 2.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(7.5f, 0.5f, 6.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(9.5f, 0.5f, 6.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(9.5f, 0.5f, 8.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(4.5f, 0.5f, 8.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(4.5f, 0.5f, 12.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(7.5f, 0.5f, 12.5f), Quaternion.identity, waypointContainer.transform));
        level.waypoints.Add(Instantiate(waypoint, new Vector3(7.5f, 0.5f, 14.5f), Quaternion.identity, waypointContainer.transform));

        // Waves
        level.waves = new List<Wave>();

        Wave w1 = new Wave(jeep, 10);
        level.waves.Add(w1);

        Wave w2 = new Wave(truck, 5);
        level.waves.Add(w2);
        
        Wave w3 = new Wave(vbtt, 2);
        level.waves.Add(w3);

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
