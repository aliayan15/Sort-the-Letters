using UnityEngine;
using Spawn;
using Players;
using Managers;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothness = 3;
    [SerializeField] private Vector3 offset;

    [Header("Axises To Follow")]
    [SerializeField] private bool X = true;
    [SerializeField] private bool Y = true;
    [SerializeField] private bool Z = true;

    private bool _canFollow;
    private Transform _target;
    float axisX;
    float axisY;
    float axisZ;

    private void StopFollow()
    {
        _canFollow = false;
    }

    private void StartFollow(Player player)
    {
        _canFollow = true;
        _target = player.transform;
        transform.position = _target.position - offset;
    }
   
    private void Update()
    {
        if (_target == null || !_canFollow)
            return;

        Vector3 diff;
        diff = _target.position - offset;

        if (X)
            axisX = diff.x;
        else
            axisX = transform.position.x;
        if (Y)
            axisY = diff.y;
        else
            axisY = transform.position.y;
        if (Z)
            axisZ = diff.z;
        else
            axisZ = transform.position.z;

        transform.position = Vector3.Lerp(transform.position,
            new Vector3(axisX, axisY, axisZ),
            Time.deltaTime * smoothness);
    }

    private void OnGameStateChange(GameStates states)
    {
        if (states == GameStates.GAMEOVER)
            StopFollow();
    }

    private void OnEnable()
    {
        var spawner = FindObjectOfType<PlayerSpawner>();
        if (spawner)
        {
            spawner.OnPlayerSpawnDone += StartFollow;
        }

        GameManager.OnGameStateChange += OnGameStateChange;
    }
    private void OnDisable()
    {
        var spawner = FindObjectOfType<PlayerSpawner>();
        if (spawner)
        {
            spawner.OnPlayerSpawnDone -= StartFollow;
        }

        GameManager.OnGameStateChange -= OnGameStateChange;
    }
}
