using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 5f;

    ThirdPersonCharacter ThirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;
        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        ThirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination= transform.position;
   }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        ProcessMouseMovement(); 
    }

     private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
            print("Cursor raycast hit layer: " + cameraRaycaster.currentLayerHit);
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = clickPoint;
                    currentDestination = ShortDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackMoveStopRadius);
                    break;
                default:
                    print("Unexpected layer found");
                    return;
            }

        }
        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude >= 0)
        {
            ThirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            ThirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        //Draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
    }

 }

