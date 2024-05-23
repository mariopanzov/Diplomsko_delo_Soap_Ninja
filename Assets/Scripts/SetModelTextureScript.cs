using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModelTexture : StateMachineBehaviour
{
    public Material _material;
    public Texture _texture;

    void OnStateEnter()
    {
        _material.SetTexture("_MainTex", _texture);
    }
}
