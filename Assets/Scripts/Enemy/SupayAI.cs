using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Color;

public class SupayAI : MonoBehaviour
{
    public bool isWalking;
    public bool isChasing;
    public bool isIdle;
    public int sightDistance;
    public NavMeshAgent agent;
    public float walkingSpeed = 1f, chaseSpeed = 5f, minChaseTime, maxChaseTime, chaseTime, minIdleTime, maxIdleTime, idleTime;
    public int randNum;
    public float range; //radius of sphere
    public Animation anim;
    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    Vector3 dest;
    public Transform player;
    public Vector3 rayCastOffset;

    void Start()
    {
        //StartCoroutine(StayIdle());
        isWalking = true;
        //anim.Play("Walk");
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position + rayCastOffset, direction, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                //Debug.DrawRay(transform.position + rayCastOffset, Vector3.forward, UnityEngine.Color.red);
                isWalking = false;
                isChasing = true;
                StopCoroutine(StayIdle());
                StopCoroutine(chaseRoutine());
                StartCoroutine(chaseRoutine());
            }
        }
        if (isIdle == true)
        {
            StopCoroutine(StayIdle());
            anim.Play("Idle");
            StartCoroutine(StayIdle());
        }
        if (isWalking == true)
        {
            StopCoroutine(chaseRoutine());
            StopCoroutine(StayIdle());
            anim.Play("Walk");
            agent.speed = walkingSpeed;
            //anim.Play("Walk");
            if (agent.remainingDistance <= agent.stoppingDistance) //done with path
            {
                randNum = Random.Range(0, 5);
                if (randNum == 0)
                {
                    
                    isWalking = false;
                    isIdle = true;
                    
                }
                else
                {
                    StopCoroutine(StayIdle());

                    Vector3 point;
                    if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
                    {
                        //Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.SetDestination(point);
                    }
                    //anim.Stop("Walk");
                    //anim.Play("Idle");
                    //agent.speed = 0f;
                    //isWalking = false;
                    //StopCoroutine(StayIdle());
                    //anim.Play("Idle");
                    //StartCoroutine(StayIdle());
                    //isWalking = false;
                    //anim.Play("Idle");
                }
            }
        }
        if (isChasing == true)
        {
            anim.Play("Run");
            dest = player.position;
            agent.destination = dest;
            agent.speed = chaseSpeed;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                //player.gameObject.SetActive(false);
                //aiAnim.Stop("Run");
                //aiAnim.Play("Attack1");
                //StartCoroutine(deathRoutine());
                isChasing = false;

            }

        }
        

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
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
    IEnumerator StayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        agent.speed = 0;
        yield return new WaitForSeconds(idleTime);
        isIdle = false;
        anim.Stop("Idle");
        isWalking = true;
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        
        isChasing = false;
        isWalking = true;
        //anim.Play("Walk");
        
    }


}