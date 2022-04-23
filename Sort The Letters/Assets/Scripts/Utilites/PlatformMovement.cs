using Managers;
using UnityEngine;

namespace Utility
{

    public class PlatformMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;


        void Update()
        {
            if (GameManager.Instance.GameState != GameStates.GAME)
                return;
            Vector3 kuvvet = transform.forward * -_speed * Time.deltaTime;
            transform.position += kuvvet;
        }
    }
}

