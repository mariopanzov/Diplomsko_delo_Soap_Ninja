using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManagerScript : MonoBehaviour
{
    //Encapsulation
    [Header("   Touch Events\n")]
    public GameEvent Event;
    
    // [SerializeField] private Camera[] cameras;
    [SerializeField] private Camera maincamera;
    [SerializeField] private Camera uicamera;
    [SerializeField] private GameObject[] touchableObjects;

    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchHoldAction;

    private Vector3 touchPosition;
    private int reach;
    private int goal = 100;

    [Header("   Soap interaction elements\n")]

    //[SerializeField] private GameObject soap;
    [SerializeField] private GameEvent gameevent_changeState;
    [SerializeField] private GameObject animationManager;
    [SerializeField] private GameObject particleManager;
    private AnimationManagerScript animations;
    private ParticleManagerScript particles;

    private LTSeq seq;

    private Ray ray;
    RaycastHit hit;

    //(-^-) maybe


    private void Awake()
    {
        if(animationManager != null)
        {
            animations = animationManager.GetComponent<AnimationManagerScript>();
            particles = particleManager.GetComponent<ParticleManagerScript>();
        }

        /*
        I reference the Player Input component attatched 
        to the same game object to which this script is attatched to
        Then I have it as a variable I guess?
        */
        playerInput = GetComponent<PlayerInput>();

        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];

        touchHoldAction = playerInput.actions["TouchHold"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += manageTouch;
    } 

    private void OnDisable()
    {
        touchPressAction.performed -= manageTouch;
    }

    public void enableSoapInteraction(Component sender, object data)
    {
        reach = 0;
        touchHoldAction.started += handleSoapInteraction;
        touchPressAction.performed -= manageTouch;
    }

    private void disableSoapInteraction()
    {
        touchHoldAction.started -= handleSoapInteraction;
        touchPressAction.performed += manageTouch;
        gameevent_changeState.Raise(this, 0);
    }


    private void manageTouch(InputAction.CallbackContext context)
    {
        Debug.Log("touched");
        if(uicamera != null && maincamera != null)
        {
            // for(int c = 0; c < cameras.Length; c++)
            // {
            // }
            touchObject(uicamera);
            touchObject(maincamera);
        }
    }

    private void touchObject(Camera camera)
    {
        ray = camera.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());

        if(Physics.Raycast(ray, out hit))
        {
            //check if collider hit
            if(hit.collider != null)
            {
                if(touchableObjects.Length != 0)
                {
                    for(int i = 0; i < touchableObjects.Length; i++)
                    {            
                        if(hit.collider == touchableObjects[i].GetComponent<Collider>())
                        { 
                            //testing, can delete
                            Debug.Log("hit --------> object (" + touchableObjects[i].name + ")");
                            
                            Event.Raise(this, touchableObjects[i].name);

                        }
                    }
                }
            }
        } 

    }

    private void handleSoapInteraction(InputAction.CallbackContext context)
    {
        StartCoroutine(SoapInteraction());
    }

    private IEnumerator SoapInteraction()
    {

        //animations.scaleUpSoap();
        particles.enable_soap_particles_touch();

        while(touchHoldAction.IsPressed())
        {
            touchPosition = uicamera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());
            // touchPosition.z = soap.transform.position.z;
            // soap.transform.position = touchPosition;
            animations.moveSoapToTouchPosition(touchPosition);

            ray = maincamera.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());

            if(Physics.Raycast(ray, out hit))
            {
                //check if collider hit
                if(hit.collider != null)
                {
                    Debug.Log(reach);
                    reach += 1;
                }
            } 

            if(reach >= goal)
            {
                break;
            }

            //keep in synch with the frame update
            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForFixedUpdate();
        
        //animations.scaleDownSoap();
        particles.disable_soap_particles_touch();
        
        yield return new WaitForFixedUpdate();

        if(reach >= goal)
        { 
            disableSoapInteraction();    
        }
    }
}
