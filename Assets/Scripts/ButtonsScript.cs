using UnityEngine;
using UnityEngine.UI;

public class ButtonsScrSetButtonImageipt : MonoBehaviour
{

    public Texture2D[] images;
    private RawImage button_image;

    private void Awake()
    {
        button_image = GetComponent<RawImage>();
        Debug.Log(this.name);
    }

    public void changeButtonImage(Component sender, string function, object data)
    {
        if((int) data < images.Length)
        {
            button_image.texture = images[(int) data];
            this.name = images[(int) data].name;
        }
    }
}
