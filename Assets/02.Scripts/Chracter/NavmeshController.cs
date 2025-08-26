using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavmeshController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public float MoveSpeed
    {
        get => agent.speed;
        set => agent.speed = value;
    }

    public bool IsStopped
    {
        get => agent.isStopped;
        set => agent.isStopped = value;
    }

    private void Awake()
    {
        if(agent == null)
            agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
