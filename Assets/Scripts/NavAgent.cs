using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgent : MonoBehaviour
{
    public Transform[] waypoint;
    private int currentWaypoint = 0;
    private NavMeshAgent navMeshAgent;
    public float DetRange = 10.0f;
    private Transform player;
    public Material Detected;
    public Material NotDetected;
    public bool Canwalk;
    public LayerMask obstacleLayer;
   
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        setDestination();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInFront() && PlayerInRange())
        {
            ChasePlayer();
            Debug.Log("Chase");
        }
        else if (Canwalk && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            setDestination();
        }
    }

    private void setDestination()
    {
        if (waypoint.Length == 0)
        {
            return;
        }
        navMeshAgent.SetDestination(waypoint[currentWaypoint].position);
        currentWaypoint = (currentWaypoint + 1) % waypoint.Length;
        gameObject.GetComponent<MeshRenderer>().material = NotDetected;

    }

    private bool PlayerInRange()
    {
        if (player != null)
        {
            Debug.Log("PlayerInRange");
            float DisToPlayer = Vector3.Distance(transform.position, player.position);
            return DisToPlayer <= DetRange;
        }
        return false;
    }

    private bool IsPlayerInFront()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            //directionToPlayer.y = 0f;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= 45f * 0.5f)
            {
                directionToPlayer.Normalize();
                Debug.Log("Angle determination");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, DetRange, obstacleLayer))
                {
                    Debug.Log("Raycast");
                    if (!hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Player tag hit");
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            navMeshAgent.SetDestination(player.position);
            gameObject.GetComponent<MeshRenderer>().material = Detected;
        }
    }

   
}
