using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManagerScript : MonoBehaviour
{
    //CORRECTIONS HANDS totation and position values
    private Vector3 STEP_0_ROT = new Vector3(8.234f, 127.829f, 350.803f);
    private Vector3 STEP_1_ROT = new Vector3(8.476f, 220.291f, 5.867f);
    private Vector3 STEP_2_ROT = new Vector3(349.7f, 298.3f, 30.3f);
    private Vector3 STEP_3_ROT = new Vector3(35.9f, 128.5f, 323.8f);
    private Vector3 STEP_4_ROT = new Vector3(352.9f, 287.4f, 19.3f);
    private Vector3 STEP_5_ROT = new Vector3(321.2f, 43.0f, 305.0f);
    private Vector3 STEP_6_ROT = new Vector3(19.5f, 187.5f, 5.96f);
    private Vector3 STEP_7_ROT = new Vector3(33.67f, 258.29f, 57.19f);
    private Vector3 STEP_8_ROT = new Vector3(12.954f , 234.862f, 17.096f);
    private Vector3 STEP_9_ROT = new Vector3(15.242f, 340.414f, 351.889f);
    private Vector3 STEP_10_ROT = new Vector3(5.229f, 240.92f, 3.264f);
    private Vector3 STEP_11_ROT = new Vector3(0.904f, 175.477f, 2.266f);

    private Vector3 STEP_0_POS = new Vector3(0.032f, -0.298f, 2.973f);
    private Vector3 STEP_1_POS = new Vector3(-0.005f,-0.301f, 1.897f);
    private Vector3 STEP_2_POS = new Vector3(0f, -0.2f, 2f);
    private Vector3 STEP_3_POS = new Vector3(-0.06f,-0.23f,2.12f);
    private Vector3 STEP_4_POS = new Vector3(-0.01f,-0.27f,1.95f);
    private Vector3 STEP_5_POS = new Vector3(-0.06f,-0.11f,2.02f);
    private Vector3 STEP_6_POS = new Vector3(-0.02f,-0.26f,1.99f);
    private Vector3 STEP_7_POS = new Vector3(-0.04f,-0.16f,2.12f);
    private Vector3 STEP_8_POS = new Vector3(-0.081f, -0.279f, 3.038f);
    private Vector3 STEP_9_POS = new Vector3(-0.004f, -0.268f, 1.714f);
    private Vector3 STEP_10_POS = new Vector3(-0.082f, -0.268f, 2.96f);
    private Vector3 STEP_11_POS = new Vector3(-0.01f, -0.195f, 1.7f);

    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject start_end_level_hands;
    [SerializeField] private GameObject popup;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject animated_camera;
    [SerializeField] private GameObject soap;
    [SerializeField] private GameObject steps_grid;
    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject button_highlight;
    [SerializeField] private GameObject wave;
    [SerializeField] private GameObject enemyPosition;
    [SerializeField] private GameObject BACTERIA_1_pos;
    [SerializeField] private GameObject BACTERIA_2_pos;
    [SerializeField] private GameObject BACTERIA_3_pos;
    [SerializeField] private GameObject BACTERIA_1;
    [SerializeField] private GameObject BACTERIA_2;
    [SerializeField] private GameObject BACTERIA_3;
    [SerializeField] private GameObject NINJA;
    //[SerializeField] private GameObject checkmark;


    Animator hands_animator;
    Animator start_end_level_hands_animator;
    Animator wave_animator;
    Animator camera_animator;
    Animator enemyPosition_animator;
    Animator BACTERIA_1_pos_animator;
    Animator BACTERIA_2_pos_animator;
    Animator BACTERIA_3_pos_animator;
    Animator BACTERIA_1_animator;
    Animator BACTERIA_2_animator;
    Animator BACTERIA_3_animator;
    Animator NINJA_animator;
    public LeanTweenType leanTweenType;
    public float speed;
    
    private Transform hands_trans;
    private Transform start_end_level_hands_trans;
    
    private RectTransform popup_recttrans;
    private RectTransform button_recttrans;
    private RectTransform steps_grid_recttrans;
    
    private Vector2 rest_popup = new Vector2(450.0f, 900.0f);
    private Vector2 rest_button = new Vector2(-450.0f, 900.0f);
    private Vector2 rest_steps_grid = new Vector2(450.0f, 0.0f);

    private LTSeq seq;



    private void Awake()
    {   


        if(hands)
        {
            hands_animator = hands.GetComponent<Animator>();
            hands_trans = hands.transform.parent.GetComponent<Transform>();
        }
        
        if(start_end_level_hands)
        {
            start_end_level_hands_animator = start_end_level_hands.GetComponent<Animator>();
            start_end_level_hands_trans = start_end_level_hands.transform.parent.GetComponent<Transform>();
        }


        if(animated_camera) camera_animator = animated_camera.GetComponent<Animator>();
        if(wave) wave_animator = wave.GetComponent<Animator>();
        if(enemyPosition) enemyPosition_animator = enemyPosition.GetComponent<Animator>();
        if(BACTERIA_1_pos) BACTERIA_1_pos_animator = BACTERIA_1_pos.GetComponent<Animator>();
        if(BACTERIA_2_pos) BACTERIA_2_pos_animator = BACTERIA_2_pos.GetComponent<Animator>();
        if(BACTERIA_3_pos) BACTERIA_3_pos_animator = BACTERIA_3_pos.GetComponent<Animator>();
        if(BACTERIA_1) BACTERIA_1_animator = BACTERIA_1.GetComponent<Animator>();
        if(BACTERIA_2) BACTERIA_2_animator = BACTERIA_2.GetComponent<Animator>();
        if(BACTERIA_3) BACTERIA_3_animator = BACTERIA_3.GetComponent<Animator>();
        if(NINJA) NINJA_animator = NINJA.GetComponent<Animator>();
        if(popup) popup_recttrans = popup.GetComponent<RectTransform>();
        if(button) button_recttrans = button.GetComponent<RectTransform>();
        if(steps_grid) steps_grid_recttrans = steps_grid.GetComponent<RectTransform>();

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

    //fix START END LEVEL HANDS rotation for every different animation
//----------------------------------------------------------------
    private void setStartEndLevelHandsRotation(Vector3 rotationVector)
    {
        start_end_level_hands_trans.localEulerAngles = rotationVector;
    }

    private void setStartEndLevelHandsPosition(Vector3 positionVector)
    {
        start_end_level_hands_trans.position = positionVector;
    }  
//----------------------------------------------------------------  

    public void handleAnimationEvent(Component sender, string function, object data)
    {
        switch(function)
        {
            case "defeatedBacteriaAnimation":
                defeatedBacteriaAnimation((int) data);
            break;
            case "changeStartEndLevelHandsAnimation":
                changeStartEndLevelHandsAnimation((string) data);
            break;
            case "changeHandsAnimation":
                changeHandsAnimation((string) data);
            break;
            case "changeNinjaAnimation":
                changeNinjaAnimation((string) data);
            break;
            case "openPopupAnimation":
                openPopupAnimation();
            break;
            case "closePopupAnimation":
                closePopupAnimation();
            break;
            case "movePopupLeftAnimation":
                movePopupLeftAnimation();
            break;
    //...
            case "resetPopupAnimation":
                resetPopupAnimation();
                break;
            case "openButtonAnimation":
                openButtonAnimation();
                break;
            case "closeButtonAnimation":
                closeButtonAnimation();
                break;
            case "openStepsGridAnimation":
                openStepsGridAnimation();
                break;
            case "closeStepsGridAnimation":
                closeStepsGridAnimation();
                break;
            case "showHighlightAnimation":
                showHighlightAnimation();
                break;
            case "hideHighlightAnimation":
                hideHighlightAnimation();
                break;
            case "showButtonHighlightAnimation":
                showButtonHighlightAnimation();
                break;
            case "hideButtonHighlightAnimation":
                hideButtonHighlightAnimation();
                break;
            case "checkmarkAnimation":
                checkmarkAnimation((int) data);
                break;
            case "gameplayCameraZoomInLeft":
                gameplayCameraZoomInLeft();
                break;
            case "levelSelectCameraZoomInLeft":
                levelSelectCameraZoomInLeft();
                break;
            case "levelSelectCameraZoomOutLeft":
                levelSelectCameraZoomOutLeft();
                break;
            case "cameraPanLeftToRight":
                cameraPanLeftToRight();
                break;
            case "cameraZoomOut":
                cameraZoomOut();
                break;
            case "levelSelectCameraZoomInRight":
                levelSelectCameraZoomInRight();
            break;
            case "levelSelectCameraZoomOutRight":
                levelSelectCameraZoomOutRight();
            break;
            case "levelSelectCameraPanRightToLeft":
                levelSelectCameraPanRightToLeft();
            break;
            case "waveAnimation":
                waveAnimation();
                break;
            case "gameplayBacteriaWashAwayAnimation":
                gameplayBacteriaWashAwayAnimation();
            break;
            case "levelSelectBacteriaWashAwayAnimation":
                levelSelectBacteriaWashAwayAnimation();
            break;
        }
    }

    private void defeatedBacteriaAnimation(int data)
    {
        switch(data)
        {
            case 0:
                BACTERIA_1_animator.Play("BACTERIA_1_defeated_animation");
            break;
            case 1:
                BACTERIA_2_animator.Play("BACTERIA_2_defeated_animation");
            break;
            case 2:
                BACTERIA_3_animator.Play("BACTERIA_3_defeated_animation");
            break;
        }
    }

    private void changeHandsAnimation(string data)
    {
        switch(data)
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

    private void changeStartEndLevelHandsAnimation(string data)
    {
        switch(data)
        {
            case "step0":
                setStartEndLevelHandsRotation(STEP_0_ROT);
                setStartEndLevelHandsPosition(STEP_0_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step0_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|step0_valve_waterflow_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|cloth_idle_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_visible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|soap_dispenser_idle_invisible_animation", 4);

            break;
            case "step1":
                setStartEndLevelHandsRotation(STEP_1_ROT);
                setStartEndLevelHandsPosition(STEP_1_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step1_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|valve_waterflow_idle_invisible_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|cloth_idle_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_invisible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|step1_soap_dispenser_animation", 4);
            break;
            case "step8":
                setStartEndLevelHandsRotation(STEP_8_ROT);
                setStartEndLevelHandsPosition(STEP_8_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step8_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|step8_valve_waterflow_idle_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|cloth_idle_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_visible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|soap_dispenser_idle_invisible_animation", 4);
            break;
            case "step9":
                setStartEndLevelHandsRotation(STEP_9_ROT);
                setStartEndLevelHandsPosition(STEP_9_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step9_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|valve_waterflow_idle_invisible_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|step9_cloth_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_invisible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|soap_dispenser_idle_invisible_animation", 4);
            break;
            case "step10":
                setStartEndLevelHandsRotation(STEP_10_ROT);
                setStartEndLevelHandsPosition(STEP_10_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step10_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|step10_valve_waterflow_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|step10_cloth_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_visible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|soap_dispenser_idle_invisible_animation", 4);
            break; 
            case "step11":
                setStartEndLevelHandsRotation(STEP_11_ROT);
                setStartEndLevelHandsPosition(STEP_11_POS);

                start_end_level_hands_animator.Play("HANDS_armature|step11_hands_animation", 0);
                start_end_level_hands_animator.Play("VALVE_armature|valve_waterflow_idle_invisible_animation", 1);
                start_end_level_hands_animator.Play("CLOTH_armature|cloth_idle_animation", 2);
                start_end_level_hands_animator.Play("PROPS_armature|props_idle_invisible_animation", 3);
                start_end_level_hands_animator.Play("SOAP_DISP_armature|soap_dispenser_idle_invisible_animation", 4);
            break;
        }
    }

    //Chanhe NINJA animations
    private void changeNinjaAnimation(string data)
    {
        switch(data)
        {
            case "step0": case "step1": case "step9": case "step10": case "wait":
                NINJA_animator.SetInteger("Default", 2);
            break;
            case "step2": case "step3": case "step4": case "step5": case "step6": case "step7": case "step8": case "wash":
                NINJA_animator.SetInteger("Default", 3);
            break;
            case "step11": case "show":
                NINJA_animator.SetInteger("Default", 4);
            break;
            default:
                NINJA_animator.SetInteger("Default", 1);
            break;
        }
    }

    //POPUP animations

    private void openPopupAnimation()
    {
        LeanTween.moveY(popup_recttrans, popup_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    private void closePopupAnimation()
    {
        LeanTween.moveY(popup_recttrans, popup_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
    
    private void movePopupLeftAnimation()
    {
        LeanTween.moveX(popup_recttrans, popup_recttrans.anchoredPosition.x - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
        
    private void resetPopupAnimation()
    {
        popup_recttrans.anchoredPosition = rest_popup;
    }

    //BUTTON animations

    private void openButtonAnimation()
    {
        LeanTween.moveY(button_recttrans, button_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    private void closeButtonAnimation()
    {
        LeanTween.moveY(button_recttrans, button_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    //STEPS GRID animations

    private void openStepsGridAnimation()
    {
        LeanTween.moveY(steps_grid_recttrans, steps_grid_recttrans.anchoredPosition.y - 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }
    
    private void closeStepsGridAnimation()
    {
        LeanTween.moveY(steps_grid_recttrans, steps_grid_recttrans.anchoredPosition.y + 900.0f, speed).setEase(/*LeanTweenType.easeOutExpo*/leanTweenType);
    }

    //Highlight animations
    private void showHighlightAnimation()
    {   
        LeanTween.scale(highlight, new Vector3(50f, 50f, 50f), 0.5f).setEase(leanTweenType);
    }

    private void hideHighlightAnimation()
    {   
        LeanTween.scale(highlight, new Vector3(30f, 30f, 30f), 0.5f).setEase(leanTweenType);
    }

    //Button Highlight animations
    private void showButtonHighlightAnimation()
    {   
        LeanTween.scale(button_highlight, new Vector3(150f, 150f, 150f), 0.5f).setEase(leanTweenType);
    }

    private void hideButtonHighlightAnimation()
    {   
        LeanTween.scale(button_highlight, new Vector3(100f, 100f, 100f), 0.5f).setEase(leanTweenType);
    }

    private void checkmarkAnimation(int data)
    {
        LeanTween.scale(steps_grid.transform.GetChild(data).transform.Find("checkmark_parent(Clone)").gameObject, new Vector3(19f, 19f, 19f), 0.6f).setEase(LeanTweenType.easeOutBack);
    }
    
    //CAMERA animations
    private void gameplayCameraZoomInLeft()
    {
        camera_animator.Play("gameplay_camera_zoom_in_left");
    }

    private void cameraPanLeftToRight()
    {
        camera_animator.Play("gameplay_camera_pan_left_to_right");
    }

    private void cameraZoomOut()
    {
        camera_animator.Play("gameplay_camera_zoom_out_right");
    }
    
    
    private void levelSelectCameraZoomInLeft()
    {
        camera_animator.Play("level_select_camera_zoom_in_left");
    }
    private void levelSelectCameraZoomOutLeft()
    {
        camera_animator.Play("level_select_camera_zoom_out_left");
    }

    private void levelSelectCameraZoomInRight()
    {
        camera_animator.Play("level_select_camera_zoom_in_right");
    }

    private void levelSelectCameraZoomOutRight()
    {
        camera_animator.Play("level_select_camera_zoom_out_right");
    }

    private void levelSelectCameraPanRightToLeft()
    {
        camera_animator.Play("level_select_camera_pan_right_to_left");
    }

    //wave animation

    private void waveAnimation()
    {
        wave_animator.Play("wave_animation");
    }

    private void gameplayBacteriaWashAwayAnimation()
    {
        enemyPosition_animator.Play("enemyPosition_animation");
    }

    private void levelSelectBacteriaWashAwayAnimation()
    {
        BACTERIA_1_pos_animator.Play("BACTERIA_1_wash_away_animation");
        BACTERIA_2_pos_animator.Play("BACTERIA_2_wash_away_animation");
        BACTERIA_3_pos_animator.Play("BACTERIA_3_wash_away_animation");
    }
    // //Soap animations

    // public void moveSoapToTouchPosition(Vector3 touchPosition)
    // {
    //         touchPosition.z = soap.transform.position.z;
    //         soap.transform.position = touchPosition;
    // }
    // public void scaleUpSoap()
    // {
    //     LeanTween.scale(soap, new Vector3(30f, 30f, 30f), 0.5f).setEase(LeanTweenType.easeOutExpo);
    // }

    // public void scaleDownSoap()
    // {
    //     LeanTween.scale(soap, new Vector3(0f, 0f, 0f), 0.5f).setEase(LeanTweenType.easeOutExpo);
    // }
}
