using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerScript : MonoBehaviour
{
    //CORRECTIONS HANDS totation and position values
    private Vector3 STEP_2_ROT = new Vector3(349.7f,298.3f,30.3f);
    private Vector3 STEP_3_ROT = new Vector3(35.9f, 128.5f, 323.8f);
    private Vector3 STEP_4_ROT = new Vector3(352.9f, 287.4f, 19.3f);
    private Vector3 STEP_5_ROT = new Vector3(321.2f,43.0f,305.0f);
    private Vector3 STEP_6_ROT = new Vector3(19.5f ,187.5f, 5.96f);
    private Vector3 STEP_7_ROT = new Vector3(33.67f, 258.29f, 57.19f);

    private Vector3 STEP_2_POS = new Vector3(0f, -0.2f, 2f);
    private Vector3 STEP_3_POS = new Vector3(-0.06f,-0.23f,2.12f);
    private Vector3 STEP_4_POS = new Vector3(-0.01f,-0.27f,1.95f);
    private Vector3 STEP_5_POS = new Vector3(-0.06f,-0.11f,2.02f);
    private Vector3 STEP_6_POS = new Vector3(-0.02f,-0.26f,1.99f);
    private Vector3 STEP_7_POS = new Vector3(-0.04f,-0.16f,2.12f);

    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject animated_camera;
    [SerializeField] private GameObject soap;
    [SerializeField] private GameObject steps_grid;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject wave;
    //[SerializeField] private GameObject checkmark;


    Animator hands_animator;
    Animator wave_animator;
    Animator camera_animator;

    public LeanTweenType leanTweenType;
    public float speed;
    
    private Transform hands_trans;
    
    private RectTransform popup_recttrans;
    private RectTransform button_recttrans;
    private RectTransform steps_grid_recttrans;
    
    private Vector2 rest_popup = new Vector2(450.0f, 900.0f);
    private Vector2 rest_button = new Vector2(-450.0f, 900.0f);
    private Vector2 rest_steps_grid = new Vector2(450.0f, 0.0f);

    private LTSeq seq;



    private void Awake()
    {
        hands_animator = hands.GetComponent<Animator>();
        camera_animator = animated_camera.GetComponent<Animator>();
        wave_animator = wave.GetComponent<Animator>();

        hands_trans = hands.transform.parent.GetComponent<Transform>();
        
        popup_recttrans = popup.GetComponent<RectTransform>();
        button_recttrans = button.GetComponent<RectTransform>();
        steps_grid_recttrans = steps_grid.GetComponent<RectTransform>();

        seq = LeanTween.sequence();
    }

    //fix HANDS rotation for every different animation
//----------------------------------------------------------------
    private void setHandsRotation(Vector3 rotationVector)
    {
        hands_trans.localEulerAngles = rotationVector;
    }

    private void setHandsPosition(Vector3 positionVector)
    {
        hands_trans.position = positionVector;
    }  
//----------------------------------------------------------------  
    public void changeHandsAnimation(Component sender, object data)
    {
        switch((string)data)
        {
            case "step2":

            setHandsPosition(STEP_2_POS);
            setHandsRotation(STEP_2_ROT);   
            hands_animator.Play("animation_hands_2");

            break;

            case "step3":

            setHandsPosition(STEP_3_POS);
            setHandsRotation(STEP_3_ROT); 
            hands_animator.Play("animation_hands_3");

            break;

            case "step4":
            
            setHandsPosition(STEP_4_POS);
            setHandsRotation(STEP_4_ROT); 
            hands_animator.Play("animation_hands_4");

            break;

            case "step5":

            setHandsPosition(STEP_5_POS);
            setHandsRotation(STEP_5_ROT); 
            hands_animator.Play("animation_hands_5");
            
            break;

            case "step6":
            setHandsPosition(STEP_6_POS);
            setHandsRotation(STEP_6_ROT); 
            hands_animator.Play("animation_hands_6");
            
            break;
            
            case "step7":
            
            setHandsPosition(STEP_7_POS);
            setHandsRotation(STEP_7_ROT); 
            hands_animator.Play("animation_hands_7");
            
            break;
        }
    }

    //POPUP animations

    public void openPopupAnimation()
    {
        LeanTween.moveY(popup_recttrans, popup_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    public void closePopupAnimation()
    {
        LeanTween.moveY(popup_recttrans, popup_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
    
    public void movePopupLeftAnimation()
    {
        LeanTween.moveX(popup_recttrans, popup_recttrans.anchoredPosition.x - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
        
    public void resetPopupAnimation()
    {
        popup_recttrans.anchoredPosition = rest_popup;
    }

    //BUTTON animations

    public void openButtonAnimation()
    {
        LeanTween.moveY(button_recttrans, button_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    public void closeButtonAnimation()
    {
        LeanTween.moveY(button_recttrans, button_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    //STEPS GRID animations

    public void openStepsGridAnimation()
    {
        LeanTween.moveY(steps_grid_recttrans, steps_grid_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
    
    public void closeStepsGridAnimation()
    {
        LeanTween.moveY(steps_grid_recttrans, steps_grid_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    //Highlight animations
    public void showHighlightAnimation()
    {   
        LeanTween.scale(highlight, new Vector3(50f, 50f, 50f), 0.5f).setEase(leanTweenType);
    }

    public void hideHighlightAnimation()
    {   
        LeanTween.scale(highlight, new Vector3(30f, 30f, 30f), 0.5f).setEase(leanTweenType);
    }

    public void checkmarkAnimation(int data)
    {
        LeanTween.scale(steps_grid.transform.GetChild(data).transform.Find("checkmark_parent(Clone)").gameObject, new Vector3(19f, 19f, 19f), 0.6f).setEase(LeanTweenType.easeOutBack);
    }
    
    //CAMERA animations
    public void cameraZoomInLeft()
    {
        camera_animator.Play("gameplay_camera_zoom_in_left");
    }

    public void cameraPanLeftToRight()
    {
        camera_animator.Play("gameplay_camera_pan_left_to_right");
    }

    public void cameraZoomOut()
    {
        camera_animator.Play("gameplay_camera_zoom_out_right");
    }

    //wave animation

    public void waveAnimation()
    {
        wave_animator.Play("wave_animation");
    }

    //Soap animations

    public void moveSoapToTouchPosition(Vector3 touchPosition)
    {
            touchPosition.z = soap.transform.position.z;
            soap.transform.position = touchPosition;
    }
    public void scaleUpSoap()
    {
        LeanTween.scale(soap, new Vector3(30f, 30f, 30f), 0.5f).setEase(LeanTweenType.easeOutExpo);
    }

    public void scaleDownSoap()
    {
        LeanTween.scale(soap, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutExpo);
    }


    //get clip length
    // public float getAnimationClipLength(string clipname)
    // {
    //     AnimatorClipInfo[] currentClipInfo;
    //     float currentClipLength = 0f;

    //     currentClipInfo = camera_animator.GetCurrentAnimatorClipInfo(0);
    //     currentClipLength = currentClipInfo[0].clip.length;

    //     return currentClipLength;

    //     // float cliplength = 0f;
    //     // if(animated_camera != null)
    //     // {
    //     //     foreach(AnimationClip clip in camera_animator.runtimeAnimatorController.animationClips)
    //     //     {
    //     //         Debug.Log(clip.name);
    //     //         if(clip.name == clipname)
    //     //         {
    //     //             cliplength = clip.length;
    //     //         }
    //     //         else
    //     //         {
    //     //             Debug.Log("no such clip name...");
    //     //         }
    //     //     }
    //     // }
    //     // return cliplength;
    // }

    public void zoomedoutstate()
    {
        
    }

}
