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

    private Vector3 destination;

    private NavMeshAgent _animalAgent;

    // Start is called before the first frame update
    void Start()
    {
        _animalAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter <= 0f)
        {
            destination = GenerateRandomPosition();
            counter = delay;
        }

        Move(destination);
        counter -= Time.deltaTime;
    }

    public void Move(Vector3 destination)
    {
        _animalAgent.SetDestination(destination);
    }

    Vector3 GenerateRandomPosition()
    {
        var result = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));

        return result;
    }
}
