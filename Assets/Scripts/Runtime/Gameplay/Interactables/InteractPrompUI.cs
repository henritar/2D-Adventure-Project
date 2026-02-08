using Assets.Scripts.Runtime.Shared.Interfaces.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Interactables
{
    public class InteractPromptUI : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private TMP_Text text;

        public void Show(IInteractable interactable)
        {
            transform.position = interactable.InteractionAnchor.position;
            text.text = interactable.InteractionText;
            canvas.enabled = true;
        }

        public void Hide()
        {
            canvas.enabled = false;
        }
    }
}
