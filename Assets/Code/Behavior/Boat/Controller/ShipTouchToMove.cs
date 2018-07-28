using UnityEngine;
using System.Collections;

public class ShipTouchToMove : ShipController
{

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            ShipMovement(-1, 0);

        if (Input.GetKeyDown(KeyCode.Q))
            ShipMovement(-1, -1);

        if (Input.GetKeyDown(KeyCode.W))
            ShipMovement(0, 1);

        if (Input.GetKeyDown(KeyCode.S))
            ShipMovement(0, -1);

        if (Input.GetKeyDown(KeyCode.E))
            ShipMovement(1, 1);

        if (Input.GetKeyDown(KeyCode.D))
            ShipMovement(1, 0);
    }
    /*
void OnGUI()
{ 
    if (GUI.Button(new Rect(10, 10, 80, 50), "move"))
        ShipMovement(inputX, inputY);
    if (GUI.Button(new Rect(90, 10, 80, 50), "->"))
        ShipMovement(1, 0);
         
    if (GUI.Button(new Rect(10, 100, 80, 50), "up"))
        ShipMovement(0, 1);

    if (GUI.Button(new Rect(90, 100, 80, 50), "down"))
        ShipMovement(0, -1);
}
     */

    Vector2 mSensitivity = new Vector2(6f, 1f);

    public float mSteering = 0f;
    public float mTargetSpeed = 0f;
    public float mTargetSteering = 0f;

    void ShipMovement(float inputX, float inputY)
    {
        bool shallowWater = IsShallowWater();

        // Being in shallow water immediately cancels forward-driving input
        if (shallowWater) inputY = 0f;
        float delta = Time.deltaTime;

        // Slowly decay the speed and steering values over time and make sharp turns slow down the ship.
        mTargetSpeed = Mathf.Lerp(mTargetSpeed, 0f, delta * (0.5f + Mathf.Abs(mTargetSteering)));
        mTargetSteering = Mathf.Lerp(mTargetSteering, 0f, delta * 3f);

        // Calculate the input-modified speed
        mTargetSpeed = shallowWater ? 0f : Mathf.Clamp01(mTargetSpeed + delta * mSensitivity.y * inputY);
        mSpeed = Mathf.Lerp(mSpeed, mTargetSpeed, Mathf.Clamp01(delta * (shallowWater ? 8f : 5f)));

        // Steering is affected by speed -- the slower the ship moves, the less maneuverable is the ship
        mTargetSteering = Mathf.Clamp(mTargetSteering + delta * mSensitivity.x * inputX * (0.1f + 0.9f * mSpeed), -1f, 1f);
        mSteering = Mathf.Lerp(mSteering, mTargetSteering, delta * 5f);


        // Move the ship

        string oldRotation = transform.localRotation.ToString();
        string oldPos = transform.localPosition.ToString();

        float turningSpeed = mShipUnit.turningSpeed;
        transform.localRotation = transform.localRotation * Quaternion.Euler(0f, mSteering * delta * turningSpeed, 0f);
        transform.localPosition = transform.localPosition + transform.localRotation * Vector3.forward * (mSpeed * delta * turningSpeed);

        Debug.Log(inputX + ", " + inputY + "  :  " + delta + "   ------>  mTargetSpeed = " + mTargetSpeed
                                            + " ||  mTargetSteering = " + mTargetSteering
                                             + " ||  mSpeed = " + mSpeed
                                              + " ||  mSteering = " + mSteering
                                               + " -------> Rotation " + oldRotation
                                                + " ||  " + transform.localRotation
                                               + " -------> new " + oldPos
                                                + "  " + transform.localPosition);
    }
}