using UnityEngine;
using System.Collections;

public class ShipTouchToDestination : ShipController
{

    public bool disableTouches = false;
    public Vector3 mDestination;

    //for performances
//     protected Quaternion mRotation;
//     protected Vector3 mPos;

    void Start()
    {
        mDestination = transform.position;
//         mRotation = transform.rotation;
//         mPos = transform.position;
    }

    void Update()//(optional) only use Update if you need to
    {
        if (disableTouches)
            return;

        Vector3 touchPos = Vector3.zero;
        if (Tools.GetTouchPositionInWater(out touchPos))
        {
            mDestination = touchPos;
            //Debug.Log(transform.position + " ------move------>" + mDestination);
        }

        ShipMovement();
    }

    void ShipMovement()
    {
        float distance = Vector3.Distance(transform.position, mDestination);

        //rotate
        Vector3 targetDir = mDestination - transform.position; 
        float rotateStep = mShipUnit.turningSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotateStep, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        if (distance < 0.1f)
            return;
    
        //move
        if (IsShallowWater())
        {
            mDestination = transform.position;
        }
        else
        {
            float step = mShipUnit.movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mDestination, step);
        }

        /*
        Vector3 targetDir = (mDestination - transform.position).normalized;
        ShipMovement(targetDir.x, targetDir.z);
         */
    }

}