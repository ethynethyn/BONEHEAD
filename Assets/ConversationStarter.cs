using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;
using PLAYERTWO.PlatformerProject;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;

    private bool playerInTrigger = false;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Gameplay.Stomp.performed += OnStomp;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Stomp.performed -= OnStomp;
        inputActions.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;

            // End the conversation when the player leaves the trigger
            if (ConversationManager.Instance.IsConversationActive)
            {
                ConversationManager.Instance.EndConversation();
            }
        }
    }

    private void OnStomp(InputAction.CallbackContext context)
    {
        if (playerInTrigger && !ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }
}