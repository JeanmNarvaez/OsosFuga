using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        // Move towards the target point
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the enemy has reached the target point
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Switch target points
            if (target == pointB.position)
            {
                target = pointA.position;
                // Look right when moving towards pointA
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                target = pointB.position;
                // Look left when moving towards pointB
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
}
