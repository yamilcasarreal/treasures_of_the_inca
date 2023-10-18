using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine;
using UnityEngine.AI; //important

public class SmallEnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Animator anim;
    public float walkSpeed, chaseSpeed, idleSpeed, idleTime, minIdleTime, maxIdleTime, chaseTime, minChaseTime, maxChaseTime, sightDistance;
    public bool playerInSight, testCo, gotShot, reset;
    public bool isStaggered, isWalking, isIdle, isAlerted;

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    public Transform player;
    Transform currentDest;
    Vector3 dest;
    public Vector3 rayCastOffSet;

    void Start()
    {
        //gameObject.SetActive(false);
        chaseTime = 5f;
        agent = GetComponent<NavMeshAgent>();
        playerInSight = false;
        isWalking = true;
        testCo = true;
        //isWalking = true;
    }

    void Update()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffSet, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                playerInSight = true;
            }
        }

        if (!playerInSight)
        {
            Patrol();
        }
        else if (playerInSight)
            Chase();

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    } 

    void Patrol()
    {
        StopCoroutine(idleRoutine());
        if (isWalking)
        {
            //StopCoroutine(idleRoutine());
            anim.ResetTrigger("idle");
            anim.SetTrigger("walk");
            agent.speed = walkSpeed;
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 0)
                {
                    isWalking = true;

                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                }
                else
                {
                    isWalking = false;
                    //Idle();
                }
            }
        }
        else
        {
            //Stop
            Idle();
        }
    }

    void Chase()
    {
        if (isStaggered)
        {
            agent.speed = 0;
            StartCoroutine("staggerDelay");
            // anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.ResetTrigger("walk");
            anim.SetTrigger("staggered");

        }
        else
        {

            agent.speed = chaseSpeed;
            // anim.ResetTrigger("walk");
            anim.ResetTrigger("idle");
            anim.ResetTrigger("staggered");
            anim.SetTrigger("walk");
        }

        dest = player.position;
        agent.destination = dest;
        if (testCo)
        {
            testCo = false;
            StartCoroutine("chaseRoutine");
        }


        // StartCoroutine(waitAFrame());
        /*if (agent.remainingDistance <= agent.stoppingDistance + 2)
        {
            
            Debug.Log(agent.remainingDistance);
            playerCaptureRange = true;
        }*/
    }

    void Idle()
    {
        agent.speed = 0;
        anim.ResetTrigger("walk");
        anim.SetTrigger("idle");
        StartCoroutine(idleRoutine());

    }

    void Attack()
    {
        agent.speed = 0;
        anim.ResetTrigger("walk");
        anim.ResetTrigger("idle");
        anim.SetTrigger("attack");
    }

    IEnumerator idleRoutine()
    {
        yield return new WaitForSeconds(4f);
        //Debug.Log("it works");
        isWalking = true;
        //Patrol();
    }

    IEnumerator chaseRoutine()
    {
        //isChasing = true;
        yield return new WaitForSeconds(chaseTime);
        chaseTime = 5;
        testCo = true;
        playerInSight = false;
    }

    IEnumerator deathRoutine()
    {
        yield return 0;
    }

    IEnumerator staggerDelay()
    {
        yield return new WaitForSeconds(0.5f);
        isStaggered = false;
        //shouldMove = true;
        //isChasing = true;
    }

    IEnumerator waitAFrame()
    {
        yield return 0;
    }

    /*
       //NashMeshAgent variable
       public NavMeshAgent ai;

       //List variable that will contain a list of our destinations tranforms
       public List<Transform> destinations;

       //AI Animator variable
       public Animator animate;

       //float variables for walking speed, chase speed, idle, and destination amount
       public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idelTime, destinationAmount;

       //boolean variables to determine if enemy is walking or chasing
       public bool walking, chasing;

       //Transform variable for player

       public Transform player;

       //Transform for ai's current destination
       Transform currentDest;

       //Vector3 variable for AI's destination
       Vector3 dest;

       //int to randomize the AI's destinations
       //randNum2 will be used to randomize something when the AI
       //reaches its destination
       int randNum, randNum2;

       void Start()
       {
           walking = true;

           //randNum will equal to a random number from a random range of
            //numbers between 0 and destinationAmount
           randNum = Random.Range(0, destinationAmount);

           //currentDest will equal to a destination from the destinations list
            //based on what the randNum equals to
           currentDest = destinations[randNum];

       }

       void Update()
       {
           //if walking equals true, dest will equal to currentDest
           if(walking == true)
           {
               dest = currentDest;

               //AI destination will equal to dest, meaning the AI will move 
                //toward dest
               ai.destination = dest;

               //AI speed will equal walkSpeed
               ai.speed = walkSpeed;

               //if AI remaining distance is less than or equal to AI
               //stopping distance
               if(ai.remainingDistance <= ai.stoppingDistance)
               {
                   //randNum2 will equal a number between 0 and 1
                   randNum2 = Random.Range(0, 2);

                   if(randNum2 == 0)
                   {
                       //if randNum2 is equal to 0, the AI will pick a new 
                       //random destination and keep moving

                       randNum = Random.Range(0, destinationAmount);
                       currentDest = destinations[randNum];
                   }

                   if(randNum2 ==1)
                   {
                       //if randNum2 is equal to 1, AI will go idle and a 
                        //coroutine will start

                       animate.ResetTrigger("walk");
                       animate.ResetTrigger("idle");
                       StopCoroutine("stayIdle");
                       StartCoroutine("stayIdle");

                       //walking will equal false
                       walking = false;
                   }
               }
           }
       }
       IEnumerator stayIdle()
       {
           idleTime = Random.Range(minIdleTime, maxIdleTime);
           yield return new WaitForSeconds(idleTime)
       }
       */
}
