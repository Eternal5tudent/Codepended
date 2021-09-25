using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;
    private AIDestinationSetter destinationSetter;
    private AIPath path;
    private Seeker seeker;
    private Transform currentWayPoint;
    private Transform oldWayPoint;
    private bool transitioning;

    private void Start()
    {
        path = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        seeker = GetComponent<Seeker>();
        GenerateNewPath();
    }

    private void GenerateNewPath()
    {
        print("Generating");
        oldWayPoint = currentWayPoint;
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
            print("reached");
            GenerateNewPath();
        }    
    }
}
