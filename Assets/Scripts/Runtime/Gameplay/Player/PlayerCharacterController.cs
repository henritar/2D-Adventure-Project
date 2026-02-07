using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    public class PlayerCharacterController : MonoBehaviour
    {
        private Animator _animator;

        private bool facingRight = false;
        private Vector2 _movementInput;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (_movementInput.x > 0 && !facingRight)
            {
                Flip(true);
            }
            else if (_movementInput.x < 0 && facingRight)
            {
                Flip(false);
            }

            _animator.SetBool("1_Move", _movementInput.magnitude > 0f);
            transform.Translate(new Vector3(_movementInput.x, _movementInput.y, 0) * Time.deltaTime * 5f);
        }

        private void Flip(bool faceRight)
        {
            facingRight = faceRight;

            Vector3 scale = transform.localScale;
            scale.x = faceRight ? -1 : 1;
            transform.localScale = scale;
        }

        public void OnMove(Vector2 moveDirection)
        {
            _movementInput = moveDirection;
        }
    }
}
