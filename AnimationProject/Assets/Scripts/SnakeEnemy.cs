using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeEnemy : MonoBehaviour
{
    public List<GameObject> BodyParts = new List<GameObject>();

    public float distanceBody = 0.25f;

    public int beginSize;

    public float moveSpeed = 1;
    public float rotationSpeed = 50;

    public GameObject bodyPrefab;

    private float dis;
    private Transform currentBodyPart;
    private Transform prevBodyPart;
    private GameObject player;
    public NavMeshAgent navMeshAgent;

    public float maxDistanceToWalk = 10.0f;
    public float minDistanceTowalk = 5.0f;
    public float timeToWalk = 5.0f;
    public float distanceToFollowPlayer = 300.0f;
    public float distanceAtack = 3.5f;
    public float timeAtack = 2.0f;
    public float dmg = 10;
    public EnemyHeal health;

    private float distance;

    private bool follow;
    private bool moveRandom;
    private bool dead;
    private Vector3 newPos;
    private PlayerManager healtPlayer;


    // Start is called before the first frame update

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        healtPlayer = player.GetComponent<PlayerManager>();
        health = GetComponent<EnemyHeal>();
        timeAtack = 0;

    }
    void Start()
    {
        for(int i=0; i<beginSize-1;i++)
        {
            AddBodyPart();
        }
    }

    void Update()
    {
        if (health.currentHealth <= 0)
        {
            dead = true;
            navMeshAgent.SetDestination(transform.position);
        }

        Move();
    }

    // Update is called once per frame
    void Move()
    {
        float currentSpeed = moveSpeed;

        if (!dead)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= distanceToFollowPlayer)
            {
                follow = true;
                moveRandom = false;

            }
            else
            {
                follow = false;
                moveRandom = true;
            }

            if (follow)
            {
                navMeshAgent.speed = currentSpeed;

                if (distance <= distanceAtack)
                {
                    Vector3 destinationLook = player.transform.position;
                    destinationLook.y = transform.position.y;
                    transform.LookAt(destinationLook, Vector3.up);

                    navMeshAgent.SetDestination(transform.position);

                    timeAtack -= Time.deltaTime;

                    if (timeAtack <= 0)
                    {
                        healtPlayer.ReceiveDamage(dmg, 0);
                        timeAtack = 2.0f;
                    }
                }
                else
                {
                    navMeshAgent.SetDestination(player.transform.position);
                    timeAtack -= Time.deltaTime;
                }
            }
            else if (moveRandom)
            {
                timeToWalk -= Time.deltaTime;
                if (timeToWalk <= 0)
                {
                    moveRandomPosition();
                    timeToWalk = Random.Range(minDistanceTowalk, maxDistanceToWalk);
                }
            }

        }
        

        for (int i = 1; i < BodyParts.Count; i++)
        {
            currentBodyPart = BodyParts[i].transform;
            prevBodyPart = BodyParts[i - 1].transform;

            dis = Vector3.Distance(prevBodyPart.position, currentBodyPart.position);

            Vector3 newpos = prevBodyPart.position;
            newpos.y = BodyParts[0].transform.position.y;

            float T = Time.deltaTime * dis / distanceBody * currentSpeed;
            if (T > 0.5f)
                T = 0.5f;

            currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newpos, T);
            currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, prevBodyPart.rotation, T);
        }
    }

    public void AddBodyPart()
    {
        GameObject newpart = Instantiate(bodyPrefab, BodyParts[BodyParts.Count - 1].transform.position, BodyParts[BodyParts.Count - 1].transform.rotation);
        newpart.transform.SetParent(transform);
        BodyParts.Add(newpart);
    }

    public void moveRandomPosition()
    {
        newPos = transform.position + new Vector3(Random.onUnitSphere.x * 10.0f, 1.0f, Random.onUnitSphere.z * 10.0f);
        //print("En funcion:"+newPos);
        navMeshAgent.SetDestination(newPos);
        navMeshAgent.speed = 5.0f;
    }
}
