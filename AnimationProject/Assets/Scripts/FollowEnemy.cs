using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowEnemy : MonoBehaviour
{
    //public Transform player;
    private GameObject player;
    public NavMeshAgent navMeshAgent;

    public float maxDistanceToWalk = 10.0f;
    public float minDistanceTowalk = 5.0f;
    public float timeToWalk = 5.0f;
    public float distanceToFollowPlayer = 300.0f;
    public float distanceAtack = 3.5f;
    public float timeAtack = 2.0f;
    public float dmg = 10;
    public Animator animator;
    public EnemyHeal health;
    public GameObject healthBar;

    private float distance;

    private bool follow;
    private bool moveRandom;
    private bool iddle;
    private bool atack;
    private bool dead;
    private bool alreadyDead =false;

    private Vector3 newPos;
    private PlayerManager healtPlayer;
    

    public void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        healtPlayer = player.GetComponent<PlayerManager>();
        animator = gameObject.GetComponent<Animator>();
        health = GetComponent<EnemyHeal>();

    }
    // Start is called before the first frame update
    void Start()
    {
        iddle = true;
        healthBar.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(health.currentHealth<=0 && !alreadyDead)
        {
            dead = true;
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("Dead", true);
            healthBar.SetActive(false);
            alreadyDead = true;
            //print("hola");
            //animator.SetBool("Dead", false);
        }

        if(!dead)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);
            //print(distance);

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
                navMeshAgent.speed = 10.0f;

                if (distance <= distanceAtack)
                {
                    Vector3 destinationLook = player.transform.position;
                    destinationLook.y = transform.position.y;
                    transform.LookAt(destinationLook,Vector3.up);
                    atack = true;
                    navMeshAgent.SetDestination(transform.position);

                    //navMeshAgent.isStopped = true;
                    timeAtack -= Time.deltaTime;
                    if (timeAtack <= 0)
                    {

                        healtPlayer.ReceiveDamage(dmg, 0);
                        timeAtack = 2.0f;
                    }
                }
                else
                {
                    atack = false;
                    navMeshAgent.SetDestination(player.transform.position);
                    //navMeshAgent.isStopped = false;
                    timeAtack += Time.deltaTime;
                    if (timeAtack >= 2.0f)
                    {
                        timeAtack = 2.0f;
                    }
                }
            }
            else if (moveRandom)
            {
                iddle = false;
                timeToWalk -= Time.deltaTime;
                timeAtack += Time.deltaTime;
                if (timeToWalk <= 0)
                {
                    moveRandomPosition();
                    //print("Sin funcion"+newPos);

                    

                    timeToWalk = Random.Range(minDistanceTowalk, maxDistanceToWalk);
                }

                Vector3 directionToDestination = transform.position - newPos;
                directionToDestination.y = 0;
                if (directionToDestination.sqrMagnitude <= 2 * 2)
                {
                    //print("he entrado");
                    //poner newPos = 0
                    iddle = true;

                }
                else
                {
                    iddle = false;

                }
            }

        }

        

        animator.SetBool("Iddle", iddle);
        animator.SetBool("MoveRandom", moveRandom);
        animator.SetBool("Follow", follow);
        animator.SetBool("Atack", atack);
        




    }

    public void moveRandomPosition()
    {
        newPos = transform.position + new Vector3(Random.onUnitSphere.x * 10.0f, 1.0f, Random.onUnitSphere.z * 10.0f);
        //print("En funcion:"+newPos);
        navMeshAgent.SetDestination(newPos);
        navMeshAgent.speed = 5.0f;
    }
}
