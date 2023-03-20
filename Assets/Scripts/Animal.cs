using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    public GameObject debugRotation;
    public GameObject debugDestination;

    private float _speed = 2;
    public float Speed { get { return _speed; } set {  _speed = value; } }

    private float range = 45;
    private float delay = 5f;
    private float counter = 0f;
    private float feedDelay = 7f;
    private float feedCounter = 0f;

    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isFed = false;
    [SerializeField] private bool isFeeding = false;

    private Vector3 destination;

    private NavMeshAgent _animalAgent;
    private Animator _animator;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _animalAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0f)
        {
            destination = GenerateRandomPosition();
            counter = delay;
        }

        if (canMove)
            Move(destination);
        else
            _animalAgent.isStopped = true;

        counter -= Time.deltaTime;
    }

    public void Move(Vector3 destination)
    {
        _animalAgent.isStopped = false;
        _animalAgent.SetDestination(destination);
        _animator.SetFloat("Speed_f", 0.65f);
    }

    Vector3 GenerateRandomPosition()
    {
        var result = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));

        return result;
    }

    protected virtual void Feed()
    {
        _animator.SetBool("Eat_b", true);
        _animator.SetFloat("Speed_f", 0);

        FreezeUnfreezeRigidBody(true);
        feedCounter = feedDelay;
    }

    private void FeedCounter(GameObject food)
    {
        feedCounter -= Time.deltaTime;
        if (feedCounter <= 0f)
        {
            canMove = true;
            isFed = true;
            isFeeding = false;
            FreezeUnfreezeRigidBody(false);
            _animator.SetBool("Eat_b", false);
            Destroy(food);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isFed)
        {
            canMove = false;
            isFeeding = true;
            Feed();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isFeeding)
            FeedCounter(other.gameObject);
    }

    private void FreezeUnfreezeRigidBody(bool freeze)
    {
        if (freeze)
            _rb.constraints = RigidbodyConstraints.FreezeAll;
        else
            _rb.constraints = RigidbodyConstraints.None;
    }
}
