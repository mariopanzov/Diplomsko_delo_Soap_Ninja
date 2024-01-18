using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour
{
    public Texture2D[] image;
    
    private RawImage button_image;

    private void Awake()
    {
        button_image = GetComponent<RawImage>();
        Debug.Log(this.name);
    }

    public void changeButtonImage(Component sender, object data)
    {
        button_image.texture = image[(int) data];
        this.name = image[(int) data].name;
    }
}
