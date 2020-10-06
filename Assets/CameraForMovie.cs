using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Services.InputService;
using Snake_box;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraForMovie : MonoBehaviour
{
    public enum act
    {
        look,
        rotate,
    }
    
    
    [Serializable]public struct doing
    {
        public float starttime;
        public float endingtime;
        public act act;
        public GameObject prefab;
        public GameObject prefabs { get=>prefab;
            set => prefab = value;
        }
        public float angel;
        public Vector3 distace;
        public Vector3 axis;
    }
    
    
    List<IEnemy> enemies = new List<IEnemy>();
    public GameObject Player;
    public float distance;
    public bool FindPlayer;
    public Vector3 Range;
    public List<GameObject> point = new List<GameObject>();
    public List<doing> things;
    public float currenttime;
    public bool starting;
    public int num;
    private float offsetx;
    private float offsety;
    private GameObject rotatearound;
    private Queue<doing> _doings = new Queue<doing>();
    private doing current;
    private bool issetdistance;


    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
            if (Player != null)
            {
                SetCamera();
                starting = true;
                fillgamepref();
                
            }
        }

        if (starting)
        {
            Debug.Log(current.act);
            currenttime += Time.deltaTime;
            if (currenttime > _doings.Peek().starttime)
            {
                issetdistance = false;
                current = _doings.Dequeue();
            }

            if (currenttime <current.endingtime )
            {
                
                startnext(current);
            }

            if (currenttime >current.endingtime  )
            {
                SetCamera();
            }
        }
    }


    private void fillgamepref()
    {

        things.Sort((doing, doing1) => doing.starttime>doing1.starttime? 1:0 );
        

        for (int i = 0; i < things.Count; i++)
        {
            _doings.Enqueue(things[i]);
        }
        Debug.Log(_doings.Count);
    }


    private void startnext(doing a)
    {
        switch (a.act)
        {
            case act.look:
                look(a);
                break;
            case act.rotate:
                rotate(a);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void rotate(doing a)
    {
        if (!issetdistance)
        {
            gameObject.transform.SetParent(a.prefab.transform);
            transform.position = a.distace;
            issetdistance = true;
        }
        transform.RotateAround(a.prefabs.transform.position,a.axis,a.angel*Time.deltaTime);
        //transform.LookAt(a.prefab.transform);
    }

    private void look(doing a)
    {
        if (!issetdistance)
        {
            transform.position = a.distace;
            issetdistance = true;
            transform.LookAt(Player.transform);
        }
    }
    
    private void resetCamera()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("TurretPoint").transform);
        rotatearound = GameObject.FindGameObjectWithTag("TurretPoint");
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, 5, -5);
        transform.LookAt(rotatearound.transform);
    }


    private void SetCamera()
    {
        transform.position = Range;
        gameObject.transform.SetParent(Player.transform);
        transform.LookAt(Player.transform);
    }
}
