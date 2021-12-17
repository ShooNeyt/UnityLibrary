using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The information about each individual axle")]
    private List<AxleInfo> axleInfos;
    [SerializeField]
    [Tooltip("Maximum torque the motor can apply to wheel")]
    private float maxMotorTorque;
    [SerializeField]
    [Tooltip("Maximum steer angle the wheel can have")]
    private float maxSteeringAngle;

    public float MaxMotorTorque
    {
        get { return maxMotorTorque; }
        set { maxMotorTorque = value; }
    }

    public float MaxSteeringAngle
    {
        get { return maxSteeringAngle; }
        set { maxSteeringAngle = value; }
    }

    private void FixedUpdate()
    {
        // Applying controls to the motion
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        // Applying motion to the wheels
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            // Apply visual effects to the wheels
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);

        }

    }

    // Function that will apply visuals rotation to the gameObject
    private void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}
