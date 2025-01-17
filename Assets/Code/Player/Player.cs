using UnityEngine;
using UnityEngine.InputSystem;

namespace GMS.Code.Player
{
    public class Player : MonoBehaviour, Control.IPlayerActions
    {
        private Animator _animCompo;
        private Rigidbody2D _rbCompo;
        private SpriteRenderer _renderer;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpPower;

        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _rayDistance = 1;

        private Control _controls;

        private Vector2 _xMovement;

        private bool _isJump;
        private bool _isGround;
        private bool _isMove;
        private bool _isAnimationEndTrigger;

        public void Awake()
        {
            if (_controls == null)
            {
                _controls = new Control();
                _controls.Player.SetCallbacks(this);
            }

            _controls.Player.Enable();

            _animCompo = GetComponent<Animator>();
            _rbCompo = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void OnDisable()
        {
            _controls.Player.Disable();
        }

        private void Update()
        {
            Debug.DrawRay(transform.position, Vector2.down, Color.red, _rayDistance);

            SetAnimation();
            GroundChecker();

            if (_xMovement.x < 0)
            {
                //_renderer.flipX = true;
                transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
            }
            else if (_xMovement.x > 0)
            {
                //_renderer.flipX = false;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            if (_isGround && _isAnimationEndTrigger)
            {
                _animCompo.SetBool("IsJump", false);
                _isAnimationEndTrigger = false;
            }
        }

        public void AnimationEndTrigger()
        {
            _isAnimationEndTrigger = true;
        }

        private void GroundChecker()
        {
            _isGround = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _whatIsGround);
        }

        private void SetAnimation()
        {
            _isMove = Mathf.Abs(_xMovement.x) > 0;

            //if (_isMove)
            //{
            //    _animCompo.SetBool("IsMove",true);
            //}
            //else
            //{
            //    _animCompo.SetBool("IsMove", false);
            //}

            _animCompo.SetBool("IsMove", _isMove);
        }

        private void FixedUpdate()
        {
            _rbCompo.linearVelocity = new Vector2(_xMovement.x * _moveSpeed, _rbCompo.linearVelocityY);
            Debug.Log(_rbCompo.linearVelocityX);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                //if (GroundCheck())
                //{
                //    _rbCompo.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

                //}

                if (_isGround)
                {
                    _rbCompo.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);

                    _animCompo.SetBool("IsJump", true);

                }
            }
        }

        private bool GroundCheck()
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _whatIsGround);

            return ray;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _xMovement = context.ReadValue<Vector2>();
        }
    }
}
