using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallEnemyAI : MonoBehaviour
{
    /*
    //NashMeshAgent variable
    public UnityEngine.AI.NavMeshAgent ai;

    //List variable that will contain a list of our destinations tranforms
    public List<Transform> destinations;

    //AI Animator variable
    public Animator animate;

    //float variables for walking speed, chase speed, idle, and destination amount
    public float walkSpeed, chaseSpeed, minIdleTime, maxIdleTime, idelTime;

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

    public int destinationAmount;

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
            dest = currentDest.position;

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
                    animate.SetTrigger("idle");

                    ai.speed = 0;
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
        yield return new WaitForSeconds(idleTime);

        //after enemy stops being idle, ai's destination will be randomized again

        walking = true;
        randNum = Random.Range(0, destinationAmount);
        currentDest = destinations[randNum];
        
        //ai will continue walking
        animate.ResetTrigger("idle");
        animate.SetTrigger("walk");
    }
    */
}
