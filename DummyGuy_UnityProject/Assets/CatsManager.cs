using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class CatsManager : MonoBehaviour
{
    [SerializeField] GameObject _catGameObject;
    [SerializeField] int _maxCats = 8;
    [SerializeField] NavMeshSurface _navMeshSurface;

    private void Awake()
    {
        for (int i = 0; i < _maxCats; i++)
        {
            var cat = Instantiate(_catGameObject, transform);
            cat.transform.position = GetRandomNavMeshPoint(cat.GetComponent<NavMeshAgent>(),5000);
        }
    }
    public Vector3 GetRandomNavMeshPoint(NavMeshAgent agent, float maxDistance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(agent.transform.position, out hit, maxDistance, agent.areaMask))
        {
            return hit.position;
        }
        else
        {
            return agent.transform.position;
        }
    }
}
