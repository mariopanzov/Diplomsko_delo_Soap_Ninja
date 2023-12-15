using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManagerScript : MonoBehaviour
{
    //Encapsulation
    [Header("Events")]
    public GameEvent Event;
    
    [SerializeField] private Camera[] cameras;

    [SerializeField] private GameObject[] touchableObjects;

    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;

    //(-^-) maybe


    private void Awake()
    {
        /*
        I reference the Player Input component attatched 
        to the same game object to which this script is attatched to
        Then I have it as a variable I guess?
        */
        playerInput = GetComponent<PlayerInput>();

        touchPositionAction = playerInput.actions["TouchPosition"];
        touchPressAction = playerInput.actions["TouchPress"];
    }

    private void OnEnable()
    {
        touchPressAction.performed += TouchSelected;
    } 

    private void OnDisable()
    {
        touchPressAction.performed -= TouchSelected;
    }

    private void TouchSelected(InputAction.CallbackContext context)
    {
        if(touchableObjects.Length != 0 /*()*/ && cameras.Length != 0)
        {
            for(int c = 0; c < cameras.Length; c++)
            {
                Ray ray = cameras[c].ScreenPointToRay(touchPositionAction.ReadValue<Vector2>());
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    //check if collider hit
                    if(hit.collider != null)
                    {
                        for(int i = 0; i < touchableObjects.Length; i++)
                        {            
                            if(hit.collider == touchableObjects[i].GetComponent<Collider>())
                            { 
                                Debug.Log("hit --------> object (" + touchableObjects[i].name + ")");
                                Event.Raise(this, touchableObjects[i].name);

                            }
                        }
                    }
                } 
            }
        }
    }
}
