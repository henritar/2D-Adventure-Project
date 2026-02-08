using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Gameplay.Inventory.ScriptableObjects;
using Assets.Scripts.Runtime.Manager;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    [RequireComponent(typeof(PlayerInteractor), typeof(PlayerInventory))]
    public class PlayerCharacterController : MonoBehaviour
    {
        public bool IsAttacking => _isAttacking;
        public bool IsInteracting => _interactor.Current != null && _interactor.Current.IsBusy;

        private Animator _animator;
        private PlayerInteractor _interactor;
        private PlayerInventory _inventory;

        private bool facingRight = false;
        private Vector2 _movementInput;
        private bool _isAttacking = false;

        private void Awake()
        {
            // TODO: Cache the Animator and other components in a more robust way,
            // especially if the hierarchy changes (SPUM Prefab for instance).
            _animator = GetComponentInChildren<Animator>();
            _interactor = GetComponent<PlayerInteractor>();
            _inventory = GetComponent<PlayerInventory>();
        }

        private void Update()
        {
            //TODO: Consider using a more robust movement system,
            //especially if we want to add features like dashing or knockback in the future.
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

            if (_movementInput.x > 0 && !facingRight)
            {
                Flip(true);
            }
            else if (_movementInput.x < 0 && facingRight)
            {
                Flip(false);
            }

            _animator.SetBool("1_Move", _movementInput.magnitude > 0f);
        }

        public void OnAttack()
        {
            if (_isAttacking) return;

            SoundManager.Instance.PlaySFX(SoundsEnum.SwordSwing);
            _animator.SetTrigger("2_Attack");
        }

        public void OnInteract()
        {
            if (_interactor.CanInteract)
            {
                _interactor.Interact(this);
            }
        }

        public void OnAttackStart()
        {
            _isAttacking = true;
        }

        public void OnAttackEnd()
        {
            _isAttacking = false;
        }

        public void OnFootstep()
        {
            SoundManager.Instance.PlaySFX(SoundsEnum.Steps);
        }

        public void AddItemToInventory(ItemData item, int quantity)
        {
            SoundManager.Instance.PlaySFX(SoundsEnum.Pickup);
            _inventory.Model.AddItem(item, quantity);
        }
    }
}
