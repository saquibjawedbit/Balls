using UnityEngine;
using System.Collections.Generic;

public class Trajectery : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Rigidbody rb;
    private Ball ball;

    [SerializeField] private int numPoints = 40;
    [SerializeField] private float timeBetweenPoints = .1f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
        ball = GetComponent<Ball>();
    }

    // Update is called once per frame
    /*void Update()
    {

        if (Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true;
            Vector3 dir = new Vector3(Mathf.Clamp((Input.mousePosition.x - ball.inital.x) * ball.forceMultiplier * -0.01f, -ball.maxForce.x, ball.maxForce.x), Mathf.Clamp((Input.mousePosition.y - ball.inital.y) * -0.01f * ball.forceMultiplier, -ball.maxForce.y, ball.maxForce.y), 0);
            lineRenderer.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = transform.position;
            lineRenderer.SetPosition(0, startingPosition);
            Vector3 startingVelocity = rb.velocity + dir * ball.forceMultiplier * 0.1f;
            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y * startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);
            }

            lineRenderer.SetPositions(points.ToArray());
        }
        else
        {

            lineRenderer.enabled = false;
        }
    }*/

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true;
            Vector3 dir = new Vector3(Mathf.Clamp((Input.mousePosition.x - ball.inital.x) * ball.forceMultiplier * -0.01f, -ball.maxForce.x, ball.maxForce.x), Mathf.Clamp((Input.mousePosition.y - ball.inital.y) * -0.01f * ball.forceMultiplier, -ball.maxForce.y, ball.maxForce.y), 0);
            lineRenderer.positionCount = numPoints;
            List<Vector3> points = new List<Vector3>();
            Vector3 startingPosition = transform.position;
            Vector3 startingVelocity = rb.velocity + dir * ball.forceMultiplier * 0.1f;
            for (float t = 0; t < numPoints; t += timeBetweenPoints)
            {
                Vector3 newPoint = startingPosition + t * startingVelocity;
                newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
                points.Add(newPoint);
            }

            lineRenderer.SetPositions(points.ToArray());
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
