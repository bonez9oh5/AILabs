using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    GameObject target = null;
    [SerializeField]
    Transform hand;
    [SerializeField]
    float moveSpeed = 2.0f;
    [SerializeField]
    float turnSpeed = 2.0f;

    Rigidbody rb;
    GameObject enemy =  null;
    StateManager sm;

    int state;
    float dist;
    bool ballInHand;
    Quaternion targetRotation;




    // Use this for initialization
    void Start()
    {

        
        sm = FindObjectOfType<StateManager>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Target");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        ballInHand = false;
    }

    // Update is called once per frame
    void Update()
    {
        state = sm.GetState();
        Debug.DrawRay(transform.position, Vector3.forward, Color.red);

        switch (state)
        {

            case 0 : IdleState();
                break;
            case 1: AttackState();
                break;
            case 2: DefendState();
                break;

        }


        dist = Vector2.Distance( target.transform.position, transform.position);
        //Debug.Log("distance to target: " + dist);
        Debug.Log("state: " + state);

        //if (dist <= attackDistance)
        //{
        //    AttackState();
        //}

        //else if 
    }

    void AttackState()
    {
        var targetPosition = new Vector3(0, -target.transform.position.y, 0);
        targetRotation = Quaternion.LookRotation(target.transform.position - targetPosition, Vector3.up);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        //rb.MovePosition(  Vector3.MoveTowards(transform.position,target.transform.position, moveSpeed * Time.fixedDeltaTime));
        rb.velocity = Vector3.forward * (-moveSpeed * Time.fixedDeltaTime);
    }

    void IdleState()
    {
        targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        rb.velocity = Vector3.zero;
    }

    void DefendState()
    {
        targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime));
        var enemyPosition = new Vector3(transform.position.x, transform.position.y, enemy.transform.position.z);
        rb.velocity = Vector3.zero;
        transform.position = Vector3.Lerp(transform.position, enemyPosition, turnSpeed * Time.fixedDeltaTime);
        //rb.MovePosition(new Vector3(transform.position.x,transform.position.y, enemy.transform.position.z * Time.fixedDeltaTime));
    }

    public void SetState(int STATE)
    {
        state = STATE;
    }

    public float GetDist()
    {
        return dist;
    }

    public bool BallInHand()
    {
        return ballInHand;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            other.transform.parent = hand;
            ballInHand = true;
        }
    }
}
