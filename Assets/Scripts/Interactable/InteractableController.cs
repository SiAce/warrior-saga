using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableController : MonoBehaviour
{
    public InteractableSO interactableSO;
    public AudioEventSO FXAudioEventSO;

    private Animator promptAnimator;
    private GameObject interactionButtonPrompt;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private bool interacted;


    public void OnInteraction()
    {
        if (!interacted)
        {
            Debug.Log("Interact!");
            FXAudioEventSO.PlayAudio(interactableSO.interactionFXAudioClip);
            InteractEffect();
            interacted = true;
            interactionButtonPrompt.SetActive(false);
            boxCollider2D.enabled = false;
            InputSystem.onActionChange -= OnActionChange;
            if (spriteRenderer)
            {
                spriteRenderer.sprite = interactableSO.afterInteractionSprite;
            }

        }
    }

    protected virtual void InteractEffect()
    {
    }

    private void Awake()
    {
        interacted = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer)
        {
            spriteRenderer.sprite = interactableSO.beforeInteractionSprite;
        }
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = true;
        interactionButtonPrompt = transform.GetChild(0).gameObject;
        interactionButtonPrompt.SetActive(false);
        promptAnimator = interactionButtonPrompt.GetComponent<Animator>();
        promptAnimator.runtimeAnimatorController = interactableSO.interactionPromptAnimatorController;

    }

    private void OnEnable()
    {
        if (!interacted)
        {
            InputSystem.onActionChange += OnActionChange;
        }
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionChange;
    }

    private void OnActionChange(object arg1, InputActionChange change)
    {
        if (change == InputActionChange.ActionStarted)
        {
            switch (((InputAction)arg1).activeControl.device)
            {
                case Keyboard:
                    promptAnimator.SetInteger("device", 0);
                    break;
                case Gamepad:
                    promptAnimator.SetInteger("device", 1);
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactionButtonPrompt.SetActive(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactionButtonPrompt.SetActive(false);
    }

}
