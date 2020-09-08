using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Services.InputService;
using Snake_box;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraForMovie : MonoBehaviour
{
    List<IEnemy> enemies = new List<IEnemy>();
    public GameObject Player;
    public float distance;
    public bool FindPlayer;
    public Vector3 Range;
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;
    public List<GameObject> point = new List<GameObject>();


    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
            if (Player != null)
            {
                SetCamera();
                point.AddRange(GameObject.FindGameObjectsWithTag("TurretPoint"));
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && turret1 == null)
        {
            turret1 = Instantiate(Data.Instance.TurretData.CannonTurret.TurretPrefab, Player.transform);
            turret1.transform.position = point[1].transform.position;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && turret2 == null)
        {
            turret2 = Instantiate(Data.Instance.TurretData.FrostTurret.TurretPrefab, Player.transform);
            turret2.transform.position = point[2].transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && turret3 == null)
        {
            turret3 = Instantiate(Data.Instance.TurretData.GrenadeTurret.TurretPrefab, Player.transform);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && turret4 == null)
        {
            turret4 = Instantiate(Data.Instance.TurretData.LaserTurret.TurretPrefab, Player.transform);
        }
    }

    private void SetCamera()
    {
        transform.position = Range;
        gameObject.transform.SetParent(Player.transform);
        transform.LookAt(Player.transform);
    }

    private void UpdateTurret()
    {
        enemies.Clear();
        enemies.AddRange(Services.Instance.LevelService.ActiveEnemies);
        
    }
}
