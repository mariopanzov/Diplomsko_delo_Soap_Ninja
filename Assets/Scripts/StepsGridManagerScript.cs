using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class StepsGridManager : MonoBehaviour
{

    [Header("   Animation Manager")]
    [SerializeField] private GameObject _animationManager;
    private AnimationManagerScript _animations; 
    
    [Header("   Highlight")]
    [SerializeField] private GameObject _highlight;
    private RectTransform _highlight_recttransform;

    [Header("   Checkmark")]
    [SerializeField] private AssetReferenceGameObject _checkmark;
    [System.NonSerialized] public GameObject _instanceReference;

    private LTSeq seq;


    private void Awake()
    {
        _highlight_recttransform = _highlight.GetComponent<RectTransform>();
        seq = LeanTween.sequence();
    }

    private void OnDisable()
    {
        // release instantiated objects
        if(_instanceReference != null && _checkmark != null)
        {
            _checkmark.ReleaseInstance(_instanceReference);
        }
    }

    public void loadCheckmark(Component sender, object data)
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


    public void changeHighlightParent(Component sender, object data)
    {
       _highlight_recttransform.SetParent(transform.GetChild((int) data), false);
    }
}
