using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;
    [SerializeField] private ParticleSystem attackParticles;
    private AIDestinationSetter destinationSetter;
    private AIPath path;
    private Transform currentWayPoint;
    private bool transitioning;
    private Animator animator;
    private int facingDirection = 1;
    private PlayerController player;

    private void Start()
    {
        path = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        animator = GetComponent<Animator>();
        GenerateNewPath();
    }

    private void GenerateNewPath()
    {
        List<Transform> nonUsedWayPoints = new List<Transform>();
        foreach (Transform waypoint in wayPoints)
        {
            if (waypoint != currentWayPoint)
            {
                nonUsedWayPoints.Add(waypoint);
            }
        }
        int Randnum = UnityEngine.Random.Range(0, nonUsedWayPoints.Count);
        currentWayPoint = nonUsedWayPoints[Randnum];
        destinationSetter.target = currentWayPoint;
        IEnumerator SetTransitioning_Cor()
        {
            yield return new WaitForSeconds(1f);
            transitioning = false;
        }
        StartCoroutine(SetTransitioning_Cor());
    }

    private void Update()
    {
        if(path.remainingDistance <= 0.1f && !transitioning)
        {
            transitioning = true;
            GenerateNewPath();
        }    

        if (Mathf.Abs(path.velocity.x) > Mathf.Abs(path.desiredVelocity.y))
        {
            animator.SetBool("side", true);
            if (path.velocity.x > 0)
            {
                if (facingDirection < 0)
                {
                    Flip();
                }
            }
            else if (path.desiredVelocity.x < 0)
            {
                if (facingDirection > 0)
                {
                    Flip();
                }
            }
        }
        else
        {
            animator.SetBool("side", false);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                bool canAttack = !player.IsHiding && !player.IsDead;
                if (canAttack)
                {
                    animator.SetTrigger("attack");
                }
            }
        }
    }

    public void KillPlayer()
    {
        player.Die();
        attackParticles.Play();
    }
}
