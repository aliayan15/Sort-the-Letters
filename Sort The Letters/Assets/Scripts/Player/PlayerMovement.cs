using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Players
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float forwartSpeed = 12f;
        [SerializeField] private float horizontalSpeed = 2f;
        [SerializeField] private float border;

        public float ForwartSpeed { get { return forwartSpeed; } set { forwartSpeed = value; } }
        public float HorizontalSpeed { get { return horizontalSpeed; } set { horizontalSpeed = value; } }


        private CharacterController _characterController;

        
        protected bool _canMove;
        private bool _mouseDown;
        private Vector3 _startPos;
        private float _targetPosX;
        private Vector3 _starCamPosX;
        private Transform _camParent;

        private float _PositionX;
        private Vector3 _currentPos;
        private Vector3 _deltaPos;
        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
            _camParent = _cam.transform.parent.transform;
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //if (!_canMove)
            //    return;

            if (Input.GetMouseButtonDown(0))
                MouseDown();
            if (Input.GetMouseButton(0))
                MouseHold();
            if (Input.GetMouseButtonUp(0))
                MouseUp();

            Move();
        }

        #region Mouse
        private void MouseDown()
        {
            // baþlangýç noktasý al
            _mouseDown = true;
            _startPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            _starCamPosX = _camParent.position;
            _PositionX = transform.position.x;
        }
        private void MouseHold()
        {
            // deltaPos hesapla
            _currentPos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
            _deltaPos = (_startPos + _camParent.position - _starCamPosX) - _currentPos;
        }
        private void MouseUp()
        {
            _mouseDown = false;
        }
        #endregion

        private void Move()
        {
            _targetPosX = _PositionX + -_deltaPos.x * horizontalSpeed;

            if (!_canMove)
            {
                return;
            }

            Vector3 move = transform.forward * forwartSpeed;
            _characterController.Move(move * Time.deltaTime);

            if (!_mouseDown)
                return;
            _targetPosX = Mathf.Clamp(_targetPosX, -border, border);
            var motionX = _targetPosX - transform.position.x;
            _characterController.Move(new Vector3(motionX, 0, 0) * Time.deltaTime * 15);
            
        }


        public void Stop()
        {
            _canMove = false;
        }

        private void OnGameStateChanged(GameStates states)
        {
            _canMove = states == GameStates.GAME;
        }

        private void OnEnable()
        {
            GameManager.OnGameStateChange += OnGameStateChanged;
        }

        private void OnDisable()
        {
            GameManager.OnGameStateChange -= OnGameStateChanged;

        }
    }
}
