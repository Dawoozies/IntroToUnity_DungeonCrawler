using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    public float reloadTime;
    float t;
    NavMeshAgent agent;
    Transform playerTransform;
    public float resetDistance;
    public Transform[] resetPositions;
    int lastReset;
    MeshRenderer[] renderers;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
        renderers = GetComponentsInChildren<MeshRenderer>();
    }
    void ToggleRenderers(bool value)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = value;
        }
    }
    void Update()
    {
        if(t <= 0)
        {
            agent.SetDestination(playerTransform.position);
            ToggleRenderers(true);
        }
        else
        {
            agent.SetDestination(transform.position);
        }
        if(Vector3.Distance(transform.position, playerTransform.position) < resetDistance && t <= 0)
        {
            ToggleRenderers(false);
            t = reloadTime;
            int newReset = Random.RandomRange(0, resetPositions.Length);
            int loopBreak = 10;
            while(newReset == lastReset)
            {
                newReset = Random.RandomRange(0, resetPositions.Length);
                loopBreak--;
                if (loopBreak == 0)
                    break;
            }
            lastReset = newReset;
            transform.parent = resetPositions[newReset];
            transform.localPosition = Vector3.zero;
        }
        if(t > 0)
        {
            t -= Time.deltaTime;
        }
    }
}
