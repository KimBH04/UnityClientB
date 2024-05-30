using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomFSM : MonoBehaviour
{
    public CustomerState currentState;
    private Timer timer;

    private NavMeshAgent agent;
    public bool isMoveDone = false;

    public Transform target;
    public Transform counter;

    public List<GameObject> targetPos = new List<GameObject>();
    public List<GameObject> myBoxes = new List<GameObject>();

    private static int nextPriority = 0;
    private static readonly object priorityLock = new object();

    public int boxesToPick = 5;
    private int boxesPicked = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
    }

    private void Start()
    {
        timer = new Timer();
        currentState = CustomerState.Idle;
    }

    private void Update()
    {
        timer.Update(Time.deltaTime);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch (currentState)
        {
            case CustomerState.Idle:
                Idle();
                break;

            case CustomerState.WalkingToShelf:
                WalkingToShelf();
                break;

            case CustomerState.PickingItem:
                PickingItem();
                break;

            case CustomerState.WalkingToCounter:
                WalkingTocounter();
                break;

            case CustomerState.PlacingItem:
                PlacingItem();
                break;

            default:
                break;
        }
    }

    private void AssignPriority()
    {
        lock (priorityLock)
        {
            agent.avoidancePriority = nextPriority;
            nextPriority = (nextPriority + 1) % 100;
        }
    }

    private void MovetoTarget()
    {
        isMoveDone = false;
        
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void ChangeState(CustomerState nextState, float waitTime = 0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }

    private void Idle()
    {
        if (timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MovetoTarget();
            ChangeState(CustomerState.WalkingToShelf, 2f);
        }
    }

    private void WalkingToShelf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CustomerState.PickingItem, 2f);
        }
    }

    private void PickingItem()
    {
        if (timer.IsFinished())
        {
            if (boxesPicked < boxesToPick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBoxes.Add(box);
                box.transform.parent = transform;
                box.transform.SetLocalPositionAndRotation(Random.insideUnitSphere * 5f, Random.rotation);

                boxesPicked++;
                timer.Set(0.01f);
            }
            else
            {
                target = counter;
                MovetoTarget();
                ChangeState(CustomerState.WalkingToCounter, 2f);
            }
        }
    }

    private void WalkingTocounter()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CustomerState.PlacingItem, 2f);
        }
    }

    private void PlacingItem()
    {
        if (timer.IsFinished())
        {
            if (myBoxes.Count != 0)
            {
                myBoxes[0].transform.position = counter.transform.position;
                myBoxes[0].transform.parent = counter.transform;
                myBoxes.RemoveAt(0);

                boxesPicked--;
                timer.Set(0.01f);
            }
            else
            {
                ChangeState(CustomerState.Idle, 2f);
            }
        }
    }
}

public class Timer
{
    private float timeRemaining;

    public void Set(float time)
    {
        timeRemaining = time;
    }

    public void Update(float deltaTime)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= deltaTime;
        }
    }

    public bool IsFinished()
    {
        return timeRemaining <= 0f;
    }
}

public enum CustomerState
{
    Idle,
    WalkingToShelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem
}