using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using DG.Tweening;

namespace Utilities
{

    public class TurnPlatform : MonoBehaviour
    {
        public Transform objToTurn;
        [SerializeField] private bool turnLeft;
        [SerializeField] private bool turnRight;

        PlayerMovement playerMovement;
        private float _speed;
        private float _horizontalSpeed;
        private float _startRosY;
        private float _lastRosY;
        private bool _used;


        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerMovement>();
            if (!player || _used)
                return;

            playerMovement = player;
            _speed = playerMovement.ForwartSpeed;
            _horizontalSpeed = playerMovement.HorizontalSpeed;
            playerMovement.HorizontalSpeed = 0;
            playerMovement.ForwartSpeed = 0;

            _startRosY = GetRotation();
            if (turnLeft)
                _lastRosY = _startRosY + 90;
            else if (turnRight)
                _lastRosY = _startRosY - 90;
            // parent al
            gameObject.transform.SetParent(null);
            objToTurn.SetParent(gameObject.transform);
            // do twen ile döndür
            transform.DORotate(new Vector3(0, _lastRosY, 0), 0.9f).OnComplete(() =>
            {
                playerMovement.ForwartSpeed = _speed;
                playerMovement.HorizontalSpeed = _horizontalSpeed;
                objToTurn.SetParent(null);
                transform.SetParent(objToTurn);
            });
            _used = true;
        }
        private float GetRotation()
        {
            float Rotation;
            if (transform.eulerAngles.y <= 180f)
            {
                Rotation = transform.eulerAngles.y;
            }
            else
            {
                Rotation = transform.eulerAngles.y - 360f;
            }
            return Rotation;
        }

    }
}
