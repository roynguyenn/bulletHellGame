using System.Collections;
using UnityEngine;

public class SprintingEnemy : Enemy
{
    public override string Name => "Sprinting Enemy";

    public float Speed { get; set; } = 12.0f;

    private Transform _playerTransform;
    private Vector3 _savedPlayerPosition;
    private float lifeTime = 20f;
    private float timer = 0f;
    private bool _isRushing = false;

    private static readonly float RotationSpeed = 180f; // Degrees per second
    private static readonly float MinWaitTime = 1.0f;
    private static readonly float MaxWaitTime = 2.0f;
    private static readonly float MinOvershootDistance = 1.0f;
    private static readonly float MaxOvershootDistance = 4.0f;

    private Coroutine _stateMachine;

    void Update(){
        timer += Time.deltaTime;
        if (timer >= lifeTime){
            Destroy(gameObject);
            timer = 0f;
        }
    }
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            _playerTransform = player.transform;
            _stateMachine = StartCoroutine(AttackCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            if (_playerTransform == null) yield break;

            _savedPlayerPosition = _playerTransform.position;

            yield return RotateTowardsTarget();

            float waitTime = Random.Range(MinWaitTime, MaxWaitTime);
            yield return new WaitForSeconds(waitTime);

            yield return RushTowardsTarget();
        }
    }

    IEnumerator RotateTowardsTarget()
    {
        Vector3 directionToTarget = (_savedPlayerPosition - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(
                transform.eulerAngles.z, targetAngle, RotationSpeed * Time.deltaTime
            );
            transform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }
    }

    IEnumerator RushTowardsTarget()
    {
        float overshootDistance = Random.Range(MinOvershootDistance, MaxOvershootDistance);
        Vector3 direction = (_savedPlayerPosition - transform.position).normalized;
        Vector3 destination = _savedPlayerPosition + direction * overshootDistance;

        _isRushing = true;
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position += direction * Speed * Time.deltaTime;
            yield return null;
        }
        _isRushing = false;
    }
}
