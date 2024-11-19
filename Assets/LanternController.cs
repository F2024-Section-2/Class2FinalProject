using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class LanternController : MonoBehaviour
{
    [SerializeField] private Light lanternLight;
    [SerializeField] private XRGrabInteractable grabInteractable;
    [SerializeField] private InputActionProperty toggleAction;

    private bool isLightOn = false;
    private IXRSelectInteractor currentInteractor;

    private void Awake()
    {
        // If not assigned in inspector, try to get components
        if (lanternLight == null)
            lanternLight = GetComponentInChildren<Light>();

        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();

        // Set initial light state
        lanternLight.enabled = isLightOn;

        // Subscribe to grab events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnEnable()
    {
        toggleAction.action.performed += OnToggleAction;
        toggleAction.action.Enable();
    }

    private void OnDisable()
    {
        toggleAction.action.performed -= OnToggleAction;
        toggleAction.action.Disable();
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        currentInteractor = args.interactorObject;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        currentInteractor = null;
    }

    private void OnToggleAction(InputAction.CallbackContext context)
    {
        if (currentInteractor != null)
        {
            ToggleLight();
        }
    }

    private void ToggleLight()
    {
        isLightOn = !isLightOn;
        lanternLight.enabled = isLightOn;
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }
    }
}
