using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsUnit : MonoBehaviour
{
    [SerializeField] private float FOVAngle;
    [SerializeField] private float smoothDamp;

    private List<BoidsUnit> cohesionNeighbours = new List<BoidsUnit>();
    private List<BoidsUnit> avoidanceNeighbours = new List<BoidsUnit>();
    private List<BoidsUnit> aligmentNeighbours = new List<BoidsUnit>();
    private Boids assignedFlock;
    private Vector3 currentVelocity;
    private float speed;

    private GameObject player;

    private Transform myTransform;
    private bool detectPlayer;
    private Renderer render;
    public Material materialChase;

    private void Awake()
    {
        myTransform = transform;
        player = GameObject.FindWithTag("Player");
        render = GetComponent<Renderer>();
    }

    public void AssignFlock(Boids flock)
    {
        assignedFlock = flock;
    }

    public void InitializeSpeed(float speed)
    {
        this.speed = speed;
    }

    public void MoveUnit()
    {
        FindNeighbours();
        CalculateSpeed();
        var cohesionVector = CalculateCohesionVector() * assignedFlock.cohesionWeight;
        var avoidVector = CalculateAvoidVector() * assignedFlock.avoidWeight;
        var aligmentVector = CalculateAligmentVector() * assignedFlock.cohesionWeight;
        var boundsVector = CalculateBoundsVector() * assignedFlock.boundsWeight;

        var moveVector = cohesionVector + avoidVector + aligmentVector + boundsVector;
        moveVector = Vector3.SmoothDamp(myTransform.forward, moveVector, ref currentVelocity, smoothDamp);
        moveVector = moveVector.normalized * speed;
        if (moveVector == Vector3.zero)
        {
            moveVector = transform.position;
        }

        myTransform.forward = moveVector;
        myTransform.position += moveVector * Time.deltaTime;
    }

    private void CalculateSpeed()
    {
        if (cohesionNeighbours.Count == 0)
        {
            return;
        }
        speed = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            speed += cohesionNeighbours[i].speed;
        }
        speed /= cohesionNeighbours.Count;
        speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed);
    }

    private void FindNeighbours()
    {
        cohesionNeighbours.Clear();
        avoidanceNeighbours.Clear();
        aligmentNeighbours.Clear();
        var allUnits = assignedFlock.allUnits;
        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if (currentUnit != this)
            {
                float currentNeighbourDistanteSqr = Vector3.SqrMagnitude(currentUnit.myTransform.position - myTransform.position);
                if (currentNeighbourDistanteSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)
                {
                    cohesionNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanteSqr <= assignedFlock.avoidDistance * assignedFlock.avoidDistance)
                {
                    avoidanceNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanteSqr <= assignedFlock.aligmentDistance * assignedFlock.aligmentDistance)
                {
                    aligmentNeighbours.Add(currentUnit);
                }
            }
        }
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbours.Count == 0)
        {
            return cohesionVector;
        }
        int neighboursInFOV = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            if (IsInFOV(cohesionNeighbours[i].myTransform.position))
            {
                neighboursInFOV++;
                cohesionVector += cohesionNeighbours[i].myTransform.position;
            }
        }

        cohesionVector /= neighboursInFOV;
        cohesionVector -= myTransform.position;
        cohesionVector = cohesionVector.normalized;
        return cohesionVector;
    }

    private Vector3 CalculateAligmentVector()
    {
        var aligmentVector = myTransform.forward;
        if (aligmentNeighbours.Count == 0)
        {
            return myTransform.forward;
        }
        int neighboursInFOV = 0;
        for (int i = 0; i < aligmentNeighbours.Count; i++)
        {
            if (IsInFOV(aligmentNeighbours[i].myTransform.position))
            {
                neighboursInFOV++;
                aligmentVector += aligmentNeighbours[i].myTransform.forward;

            }
        }

        aligmentVector /= neighboursInFOV;
        aligmentVector = aligmentVector.normalized;
        return aligmentVector;
    }

    private Vector3 CalculateAvoidVector()
    {
        var avoidanceVector = Vector3.zero;
        if (avoidanceNeighbours.Count == 0)
        {
            return Vector3.zero;
        }
        int neighboursInFOV = 0;
        for (int i = 0; i < avoidanceNeighbours.Count; i++)
        {
            if (IsInFOV(avoidanceNeighbours[i].myTransform.position))
            {
                neighboursInFOV++;
                avoidanceVector += (myTransform.position - avoidanceNeighbours[i].myTransform.position);

            }
        }

        avoidanceVector /= neighboursInFOV;
        avoidanceVector = avoidanceVector.normalized;
        return avoidanceVector;
    }

    private Vector3 CalculateBoundsVector()
    {
        var offsetToCenter = assignedFlock.transform.position - myTransform.position;
        bool isNearCenter = (offsetToCenter.magnitude >= assignedFlock.boundsWeight * 0.9f);
        return isNearCenter ? offsetToCenter.normalized : Vector3.zero;
    }

    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle;
    }

    public bool DetectingPlayer()
    {
        return detectPlayer;
    }

    public void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detectPlayer = true;
            render.material = materialChase;
        }
    }
}
