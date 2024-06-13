#pragma warning disable UNT0007

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicTrajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 30;
    public float timeStep = 0.1f;

    public Transform launchPoint;
    public float myRotation;
    public float launchPower;
    public float launchAngle;
    public float launchDirection;
    public float gravity = -9.8f;
    public GameObject projectilePrefab;

    private void Start()
    {
        lineRenderer.positionCount = resolution;
    }

    private void Update()
    {
        RenderTrajectory();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile(Instantiate(projectilePrefab));
        }
    }

    private void RenderTrajectory()
    {
        lineRenderer.positionCount = resolution;
        Vector3[] points = new Vector3[resolution];

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            points[i] = CalculatePositionAtTime(t);
        }
        lineRenderer.SetPositions(points);
    }

    private Vector3 CalculatePositionAtTime(float time)
    {
        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        float x = launchPower * time * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float z = launchPower * time * Mathf.Sin(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float y = launchPower * time * Mathf.Sin(launchAngleRad) + 0.5f * gravity * time * time;

        return launchPoint.position + new Vector3(x, y, z);
    }

    public void LaunchProjectile(GameObject projectile)
    {
        projectile.transform.SetPositionAndRotation(launchPoint.position, launchPoint.rotation);
        projectile.transform.SetParent(null);

        Rigidbody rb = projectile.GetComponent<Rigidbody>() ?? projectile.AddComponent<Rigidbody>();

        rb.isKinematic = false;

        float launchAngleRad = Mathf.Deg2Rad * launchAngle;
        float launchDirectionRad = Mathf.Deg2Rad * launchDirection;

        float initialVelocityX = launchPower * Mathf.Cos(launchAngleRad) * Mathf.Cos(launchDirectionRad);
        float initialVelocityZ = launchPower * Mathf.Sin(launchAngleRad) * Mathf.Sin(launchDirectionRad);
        float initialVelocityY = launchPower * Mathf.Sin(launchAngleRad);

        Vector3 initialVelocity = new(initialVelocityX, initialVelocityY, initialVelocityZ);

        rb.velocity = initialVelocity;
    }
}
