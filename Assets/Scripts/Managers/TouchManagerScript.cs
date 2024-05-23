using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    [Header("   Touch Events\n")]
    public GameEvent gameEvent_touchObject;
    
    [SerializeField] private Camera maincamera;
    [SerializeField] private Camera uicamera;
    [SerializeField] private GameObject[] touchableObjects;

    private PlayerInput playerInput;

    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchHoldAction;

    private Vector3 touchPosition;
    private int reach;
    private int goal = 50;

    [Header("   Soap interaction elements\n")]

    [SerializeField] private GameObject touch_particles;
    [SerializeField] private GameEvent gameEvent_changeState;
    [SerializeField] private GameEvent _gameEvent_particles;
    private string which_particles = "soap";

    private Ray ray;
    RaycastHit hit;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
        touchHoldAction = playerInput.actions["TouchHold"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += handleTouchPressInteraction;
    } 

    private void OnDisable()
    {
        touchPressAction.performed -= handleTouchPressInteraction;
    }

//...

    public void toggleTouchSettings(Component sender, string function, object data)
    {
        switch(function)
        {
            case "TouchHold":
                toggleTouchHold((string) data);
            break;
            case "TouchPress":
                toggleTouchPress((string) data);
            break;
            case "ParticleChoice":
                chooseParticles((string) data);
            break;

        }
    }

    private void chooseParticles(string data)
    {
        which_particles = data; 
    }

    public void toggleTouchPress(string data)
    {
        switch(data)
        {
            case "enable":
                touchPressAction.performed += handleTouchPressInteraction;
                Debug.Log("touch enabled");
            break;
            case "disable":
                touchPressAction.performed -= handleTouchPressInteraction;
                Debug.Log("touch disabled");
            break;
        }
    }

    public void toggleTouchHold(string data)
    {
        switch(data)
        {
            case "enable":
                reach = 0;
                toggleTouchPress("disable");
                touchHoldAction.started += handleTouchHoldInteraction;
            break;
            case "disable":
                toggleTouchPress("enable");
                touchHoldAction.started -= handleTouchHoldInteraction;
                gameEvent_changeState.Raise(this, "changeState", 0);
            break;
        }
    }

    private void handleTouchPressInteraction(InputAction.CallbackContext context)
    {
        if(uicamera != null)
        {
            if(!touchObject(uicamera) && maincamera != null)
            {
                if(touchObject(maincamera))
                {
                    Debug.Log("go_touched");
                }
            }
            else
            {
                Debug.Log("ui_touched");
            }
        }
    }

    private bool touchObject(Camera camera)
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
                            gameEvent_touchObject.Raise(this, "touchedObject", touchableObjects[i].name);
                            return true;
                        }
                    }
                }
            }
        } 
        return false;
    }

    private void handleTouchHoldInteraction(InputAction.CallbackContext context)
    {
        StartCoroutine(touchHoldInteraction());
    }

    private IEnumerator touchHoldInteraction()
    {
        _gameEvent_particles.Raise(this, "enableTouchParticles", which_particles);

        while(touchHoldAction.IsPressed())
        {
            touchPosition = uicamera.ScreenToWorldPoint(touchPositionAction.ReadValue<Vector2>());

            touchPosition.z = touch_particles.transform.position.z;
            touch_particles.transform.position = touchPosition;

            ray = maincamera.ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());

            if(Physics.Raycast(ray, out hit))
            {
                //check if collider hit
                if(hit.collider != null)
                {
                    reach += 1;
                }
            } 

            if(reach >= goal)
            {
                break;
            }

            yield return new WaitForFixedUpdate();
        }
        
        yield return new WaitForFixedUpdate();
        
        _gameEvent_particles.Raise(this, "disableTouchParticles", which_particles);
        
        yield return new WaitForFixedUpdate();

        if(reach >= goal)
        { 
            toggleTouchHold("disable");    
        }
    }
}
