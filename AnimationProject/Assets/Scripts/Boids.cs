using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private BoidsUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }

    [Header("Detection Distances")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance { get { return _cohesionDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidDistance;
    public float avoidDistance { get { return _avoidDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _aligmentDistance;
    public float aligmentDistance { get { return _aligmentDistance; } }

    [Range(0, 100)]
    [SerializeField] private float _boundsDistance;
    public float boundsDistance { get { return _boundsDistance; } }


    [Header("Behaviour Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight { get { return _cohesionWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidWeight;
    public float avoidWeight { get { return _avoidWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _aligmentWeight;
    public float aligmentWeight { get { return _aligmentWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight { get { return _boundsWeight; } }
    public BoidsUnit[] allUnits { get; set; }

    public GameObject centralBoidsObject;


    // Start is called before the first frame update
    void Start()
    {
        GenerateUnits();
    }

    // Update is called once per frame
    void Update()
    {
        if(allUnits[0]==null)
        {
            Destroy(this);
        }

        Vector3 directionToPlayer = allUnits[0].PlayerPosition().position - centralBoidsObject.transform.position;
        directionToPlayer.Normalize();

        for (int i = 0; i < allUnits.Length; i++)
        {
            if(allUnits[i].DetectingPlayer())
            {
                if(!allUnits[i].alreadyPaint)
                {
                    for (int j = 0; j < allUnits.Length; j++)
                    {
                        allUnits[j].ChangeTexture();
                    } 
                }
                centralBoidsObject.transform.position = allUnits[i].PlayerPosition().position - 30.0f * directionToPlayer;

                allUnits[i].MoveUnit();
                if(allUnits[i].canShoot)
                {
                    allUnits[i].Shoot();
                }
            }
            else
            {
                allUnits[i].MoveUnit();
            }
            
        }
    }

    public void GenerateUnits()
    {
        allUnits = new BoidsUnit[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializeSpeed(Random.Range(minSpeed, maxSpeed));
        }
    }
}
