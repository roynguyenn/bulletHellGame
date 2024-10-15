using System.Collections;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    float moveSpeed = 5f;
    bool isDashing = false;
    bool canDash = true;

    static readonly float dashCooldown = 1f;
    static readonly float dashDistance = 5f;
    static readonly float dashDuration = 0.15f;
    static readonly float slowMotionFactor = 0.5f;

    Vector3 dashDirection;
    TrailRenderer trailRenderer;

    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
    }

    void Update()
    {
        if (!isDashing) Move();
        if (Input.GetKeyDown(KeyCode.Space) && canDash) StartCoroutine(Dash());
    }

    void Move()
    {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) movementVector += Vector3.up;
        if (Input.GetKey(KeyCode.S)) movementVector += Vector3.down;
        if (Input.GetKey(KeyCode.D)) movementVector += Vector3.right;
        if (Input.GetKey(KeyCode.A)) movementVector += Vector3.left;

        movementVector.Normalize();
        transform.position += movementVector * moveSpeed * Time.deltaTime;

        if (movementVector != Vector3.zero) dashDirection = movementVector;
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Time.timeScale = slowMotionFactor;

        trailRenderer.emitting = true;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + dashDirection * dashDistance;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isDashing = false;
        trailRenderer.emitting = false;

        Time.timeScale = 1f;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
