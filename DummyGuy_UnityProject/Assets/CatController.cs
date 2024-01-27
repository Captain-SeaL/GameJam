using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class CatController : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;
    private float _randomTime = 1f;
    
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField]private Animator animator;
    private float timer;
    private bool isWalking;
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Stop = Animator.StringToHash("Stop");

    void OnEnable () {
        agent = GetComponent<NavMeshAgent> ();
        
        timer = wanderTimer;
        agent.speed = 1;
        agent.angularSpeed = 120;
        Debug.Log(agent.angularSpeed);
    }
    void Update () {
        timer += Time.deltaTime;
        if (agent.pathPending)
        {
            animator.SetTrigger(Walk);
            isWalking = true;
        }
        else
        {
            if (isWalking)
            {
                animator.SetTrigger(Stop);
                isWalking = false;
            }
            
            if (timer >= wanderTimer + _randomTime) {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
                _randomTime = Random.value * 4f;
            }
        }
    }
 
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
