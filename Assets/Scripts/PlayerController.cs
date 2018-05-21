using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Transform target;
    private Transform position;
    private NavMeshAgent myNavAgent;

    public float RotationSmoothness;

    private bool allowMovement = true;

    // Use this for initialization
    private void Start()
    {
        // get and cache refrences.
        myNavAgent = this.GetComponent<NavMeshAgent>();
        position = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        // check movement
        DoMovement();
    }

    private void DoMovement()
    {
        // if We are not allowing movement then return;
        if (!allowMovement)
            return;

        // Update Movement
        var tmoMotionVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * myNavAgent.speed;
        myNavAgent.Move(tmoMotionVector);
        myNavAgent.SetDestination(transform.position + tmoMotionVector);

        // Update Rotation
        //var desiredRotation = Quaternion.LookRotation(InputManager.MovementAxisVec3, Vector3.up);
        //var smoothedRotation = Quaternion.Lerp(this.transform.rotation, desiredRotation, RotationSmoothness);
        //this.transform.rotation = smoothedRotation;
        
        var mouse = Input.mousePosition;
        mouse.z = 5.23f; //The distance between the camera and object
        var screenPoint = Camera.main.WorldToScreenPoint(position.position);
        var angle = Mathf.Atan2(mouse.y - screenPoint.y, mouse.x - screenPoint.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, (180 - angle) - 90, 0));

    }
}