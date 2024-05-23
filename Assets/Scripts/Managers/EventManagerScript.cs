using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventObjectPair
{
    public GameEvent event_for_objects;
    public bool OVERRIDE_FUNCTION = false;
    public string function_for_event = "";
    public bool OVERRIDE_VALUE = false;
    public string pass_value = "";
    public GameObject[] touched_object;

    ~EventObjectPair(){}
}

public class EventManagerScript : MonoBehaviour
{
    [SerializeField] private EventObjectPair[] eventObjectPair;
    private object newdata = "";
    private string newfunction = "";


    public void ManageTouchedEvent(Component sender, string function, object data)
    {

        Debug.Log("im here");
        if(eventObjectPair.Length != 0)
        {
            for(int i = 0; i < eventObjectPair.Length; i++)
            {
                if(eventObjectPair[i].touched_object.Length != 0)
                {
                    for(int j = 0; j < eventObjectPair[i].touched_object.Length; j++)
                    {
                        if((string)data == eventObjectPair[i].touched_object[j].name)
                        {

                            if(eventObjectPair[i].OVERRIDE_FUNCTION)
                            {
                                newfunction = eventObjectPair[i].function_for_event;
                            }
                            else
                            {
                                newfunction = function;
                            }

                            if(eventObjectPair[i].OVERRIDE_VALUE)
                            {
                                newdata = eventObjectPair[i].pass_value;
                            }
                            else
                            {
                                newdata = data;
                            }
                            
                            eventObjectPair[i].event_for_objects.Raise(this, newfunction, newdata);
                        }
                    }
                }
            }
        }
    }
}
