using Assets.Scripts.Runtime.Gameplay.Interactables;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Runtime.Gameplay.Player
{
    public class PlayerUIController : MonoBehaviour
    {
        [SerializeField] PlayerInteractor interactor;
        [SerializeField] InteractPromptUI prompt;

        void Awake()
        {
            interactor.InteractableEntered += interactable =>
            {
                if (!interactable.IsBusy)
                {
                    prompt.Show(interactable);
                }
            };

            interactor.InteractableExited += () =>
            {
                prompt.Hide();
            };
        }
    }
}
