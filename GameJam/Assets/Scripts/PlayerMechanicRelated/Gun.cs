using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform launchPos, pointsFather;
    [SerializeField] private GameObject  pointPrefab;
    [SerializeField] private ObjectPoolInstance ballPrefab1, ballPrefab2, ballPrefab3;
    [SerializeField] private float angle = 45, force, timeBetweenShots = 2f;
    [SerializeField] private int pointsAmount, initialSize, incrementAmount;

    private Vector2 dir;
    private float angleRad, multiplier = 0.5f, remainingCD;
    private GameObject[] points;
    private PlayerController playerCntrl;
    public Queue<ObjectPoolInstance> gun1 { get; private set; }
    public Queue<ObjectPoolInstance> gun2 { get; private set; } 
    public Queue<ObjectPoolInstance> gun3 { get; private set; }

    private float Angle
    {
        get => angle;
        set
        {
            if (value < 0)
            {
                angle = 0;
            }
            else if (value > 180)
            {
                angle = 180;
            }
            else
            {
                angle = value;
            }
        }
    }

    private void Awake()
    {
        points = new GameObject[pointsAmount];
        playerCntrl = FindObjectOfType<PlayerController>();
        pointsFather.gameObject.SetActive(false);

        gun1 = new Queue<ObjectPoolInstance>();
        gun2 = new Queue<ObjectPoolInstance>();
        gun3 = new Queue<ObjectPoolInstance>();

        remainingCD = timeBetweenShots;
        
        AddInstances(initialSize,1, ballPrefab1);
        AddInstances(initialSize, 2, ballPrefab2);
        AddInstances(initialSize, 3, ballPrefab3);
    }

    private void Start()
    {
        CreateVisualPoints();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDirection();
        
        SetVisualPoints();
        
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            Launch();
        }
        */
        
        if (playerCntrl.isturret)
        {
            pointsFather.gameObject.SetActive(true);
        }
        else
        {
            pointsFather.gameObject.SetActive(false);
        }
        
        CDBetweenShots();
    }

    private void FixedUpdate()
    {
        if (playerCntrl.isturret)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                Angle += 2f;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                Angle -= 2f;
            }
        }
    }

    void Launch(ObjectPoolInstance prefab)
    {
        Rigidbody2D _rb = prefab.gameObject.GetComponent<Rigidbody2D>();
        _rb.AddForce(dir * force, ForceMode2D.Impulse);
    }
    
    void CreateVisualPoints()
    {
        for (int i = 0; i < pointsAmount; i++)
        {
            points[i] = Instantiate(pointPrefab, pointsFather.position, Quaternion.identity, pointsFather);
        }
    }

    void SetVisualPoints()
    {
        for (int i = 0; i < pointsAmount; i++)
        {
            points[i].transform.position = PointsPosition(i * 0.1f);
        }
    }
    
    void CalculateDirection()
    {
        //Transformation to Radians
        angleRad = Mathf.Deg2Rad * angle;

        //Calculate direction
        dir.x = Mathf.Cos(angleRad);
        dir.y = Mathf.Sin(angleRad);
    }

    Vector2 PointsPosition(float time)
    {
        Vector2 currentPointPos = (Vector2)pointsFather.position + (dir.normalized * 9.8f * time) + multiplier * (Physics2D.gravity*2) * (time * time);

        return currentPointPos;
    }

    void CDBetweenShots()
    {
        if (remainingCD > 0 && playerCntrl.isturret)
        {
            remainingCD -= Time.deltaTime;
            if (remainingCD <= 0)
            {
                switch (PlayerStats.playerstats.gunEquipped)
                {
                    case GunType.Damage:
                        Launch(Allocate(1));
                        break;
                    case GunType.Slow:
                        Launch(Allocate(2));
                        break;
                    case GunType.KickBack:
                        Launch(Allocate(3));
                        break;
                }

                remainingCD = timeBetweenShots;
            }
        }
    }

    #region Pooling

    private void AddInstances(int amount, byte poolIndex, ObjectPoolInstance prefab)
    {
        for (int i = 0; i < amount; i++)
        {
            ObjectPoolInstance obj = Instantiate(prefab.gameObject, launchPos.transform).GetComponent<ObjectPoolInstance>();
            
            obj.Disable();
            
            switch (poolIndex)
            {
                case 1:
                    gun1.Enqueue(obj);
                    obj.SetPool(poolIndex, this);
                    break;
                case 2:
                    gun2.Enqueue(obj);
                    obj.SetPool(poolIndex, this);
                    break;
                case 3:
                    gun3.Enqueue(obj);
                    obj.SetPool(poolIndex, this);
                    break;
            }
        }
    }

    public ObjectPoolInstance Allocate(byte poolIndex)
    {
        Queue<ObjectPoolInstance> _instances = new Queue<ObjectPoolInstance>();
        ObjectPoolInstance instance = null;
        
        switch (poolIndex)
        {
            case 1:
                _instances = gun1;
                instance = ballPrefab1;
                break;
            case 2:
                _instances = gun2;
                instance = ballPrefab2;
                break;
            case 3:
                _instances = gun3;
                instance = ballPrefab3;
                break;
        }

        if (_instances.Count == 0)
        {
            AddInstances(incrementAmount,poolIndex, instance);
            return Allocate(poolIndex);
        }

        instance = _instances.Dequeue();
        instance.Prepare();
        return instance;
    }

    public void Return(ObjectPoolInstance instance, byte poolIndex)
    {
        switch (poolIndex)
        {
            case 1:
                gun1.Enqueue(instance);
                break;
            case 2:
                gun2.Enqueue(instance);
                break;
            case 3:
                gun3.Enqueue(instance);
                break;
        }
        
        instance.Disable();
        instance.transform.position = launchPos.transform.position;
    }

    #endregion
}
