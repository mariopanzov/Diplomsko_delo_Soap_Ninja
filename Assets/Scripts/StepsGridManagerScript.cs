using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class StepsGridManager : MonoBehaviour
{

    [Header("   Highlight")]
    [SerializeField] private GameObject _highlight;
    private RectTransform _highlight_recttransform;

    [Header("   Checkmark")]
    [SerializeField] private AssetReferenceGameObject _checkmark;
    [System.NonSerialized] public GameObject _instanceReference;

    private string[][] _steps;

    private void Awake()
    {
        _steps = new string[2][];

        _steps[0] = new string[] {"step0", "step1"};
        _steps[1] = new string[] {"step8", "step9", "step10", "step11"};

        _highlight_recttransform = _highlight.GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        // release instantiated objects
        if(_instanceReference != null && _checkmark != null)
        {
            _checkmark.ReleaseInstance(_instanceReference);
        }
    }

    public void loadCheckmark(Component sender, string function, object data)
    {
        _checkmark.InstantiateAsync().Completed += (AsyncOperationHandle<GameObject> handle) => {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                _instanceReference = handle.Result;

                if(transform.childCount != 0)
                {
                    _instanceReference.transform.SetParent(transform.GetChild((int) data), false);
                }
                    
            }
        };
    }

    public void changeHighlightParent(Component sender, string function, object data)
    {
       _highlight_recttransform.SetParent(transform.GetChild((int) data), false);
    }

    public void toggleGridButtons(Component sender, string function, object data)
    {
        switch(function)
        {
            case "activate":
                activateGridButtons((int) data);
            break;
            case "deactivate":
                deactivateGridButtons();
            break;
        }
    }

    public void activateGridButtons(int data)
    {
        for(int i = 0; i < _steps[data].Length; i++)
        {
            foreach(Transform child in transform)
            {
                if(child.name == _steps[data][i])
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void deactivateGridButtons()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }

}
