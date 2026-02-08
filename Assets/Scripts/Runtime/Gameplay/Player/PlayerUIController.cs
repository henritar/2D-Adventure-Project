using Assets.Scripts.Runtime.Gameplay.Interactables;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] PlayerInteractor interactor;
        [SerializeField] InteractPromptUI prompt;

        void Awake()
        {
            interactor.InteractableChanged += interactable =>
            {
                if (interactable != null)
                    prompt.Show(interactable);
                else
                    prompt.Hide();
            };
        }
    }
}
