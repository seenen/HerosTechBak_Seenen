using UnityEngine;
using System.Collections;

public interface IFingerControl
{
    bool    bSelection { set; get; }
    bool    bRotation { set; get; }
    bool    bDrag { set; get; }

    void    Draging(Vector2 newpos);
    void    DragEnd();
    void    Rotation(Vector2 delta);

}

public class FingerControlBase : MonoBehaviour 
{

    #region Gesture event registration/unregistration
    void OnEnable()
    {
        Debuger.Log("Registering finger gesture events from C# script");

        // register input events
        FingerGestures.OnFingerLongPress += FingerGestures_OnFingerLongPress;
        FingerGestures.OnFingerTap += FingerGestures_OnFingerTap;
        FingerGestures.OnFingerDoubleTap += FingerGestures_OnFingerDoubleTap;
        FingerGestures.OnFingerSwipe += FingerGestures_OnFingerSwipe;
        FingerGestures.OnFingerDragBegin += FingerGestures_OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += FingerGestures_OnFingerDragMove;
        FingerGestures.OnFingerDragEnd += FingerGestures_OnFingerDragEnd;
        FingerGestures.OnFingerUp += FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDown += FingerGestures_OnFingerDown;

        FingerGestures.OnPinchBegin += FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove += FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd += FingerGestures_OnPinchEnd;

        FingerGestures.OnDragBegin += FingerGestures_OnDragBegin;
        FingerGestures.OnDragMove += FingerGestures_OnDragMove;
        FingerGestures.OnDragEnd += FingerGestures_OnDragEnd;
    }

    void OnDisable()
    {
        // unregister finger gesture events
        FingerGestures.OnFingerLongPress -= FingerGestures_OnFingerLongPress;
        FingerGestures.OnFingerTap -= FingerGestures_OnFingerTap;
        FingerGestures.OnFingerDoubleTap -= FingerGestures_OnFingerDoubleTap;
        FingerGestures.OnFingerSwipe -= FingerGestures_OnFingerSwipe;
        FingerGestures.OnFingerDragBegin -= FingerGestures_OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= FingerGestures_OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= FingerGestures_OnFingerDragEnd;
        FingerGestures.OnFingerUp -= FingerGestures_OnFingerUp;
        FingerGestures.OnFingerDown -= FingerGestures_OnFingerDown;

        FingerGestures.OnPinchBegin -= FingerGestures_OnPinchBegin;
        FingerGestures.OnPinchMove -= FingerGestures_OnPinchMove;
        FingerGestures.OnPinchEnd -= FingerGestures_OnPinchEnd;

        FingerGestures.OnDragBegin -= FingerGestures_OnDragBegin;
        FingerGestures.OnDragMove -= FingerGestures_OnDragMove;
        FingerGestures.OnDragEnd -= FingerGestures_OnDragEnd;
    }

    bool Key_W = false;
    bool Key_S = false;
    bool Key_D = false;
    bool Key_A = false;
    bool Key_0 = false;
    protected bool Key_Rotation = false;
    protected bool Key_Alt = false;



    virtual public void Update()
    {
#if UNITY_STANDALONE_WIN

        if (Input.GetKeyDown(KeyCode.LeftControl)) Key_Rotation = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) Key_Rotation = false;

        if (Input.GetKeyDown(KeyCode.LeftAlt)) Key_Alt = true;
        if (Input.GetKeyUp(KeyCode.LeftAlt)) Key_Alt = false;

        if (Input.GetKeyDown(KeyCode.W)) Key_W =(true);
        if (Input.GetKeyUp(KeyCode.W)) Key_W =(false);

        if (Input.GetKeyDown(KeyCode.S)) Key_S = (true);
        if (Input.GetKeyUp(KeyCode.S)) Key_S = (false);

        if (Input.GetKeyDown(KeyCode.A)) Key_A = (true);
        if (Input.GetKeyUp(KeyCode.A)) Key_A = (false);

        if (Input.GetKeyDown(KeyCode.D)) Key_D = (true);
        if (Input.GetKeyUp(KeyCode.D)) Key_D = (false);

        if (Input.GetKeyDown(KeyCode.UpArrow)) Key_W = (true);
        if (Input.GetKeyUp(KeyCode.UpArrow)) Key_W = (false);

        if (Input.GetKeyDown(KeyCode.DownArrow)) Key_S = (true);
        if (Input.GetKeyUp(KeyCode.DownArrow)) Key_S = (false);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) Key_A = (true);
        if (Input.GetKeyUp(KeyCode.LeftArrow)) Key_A = (false);

        if (Input.GetKeyDown(KeyCode.RightArrow)) Key_D = (true);
        if (Input.GetKeyUp(KeyCode.RightArrow)) Key_D = (false);

        if (Key_W)  Up();
        if (Key_S)  Down();
        if (Key_A)  Left();
        if (Key_D)  Right();

        if (Input.GetKeyDown(KeyCode.Keypad0)) Key_0 = (true);
        if (Input.GetKeyUp(KeyCode.Keypad0)) Key_0 = (false);

        if (Key_0) KeyDown(KeyCode.Keypad0);

        if (Input.GetKeyDown(KeyCode.Keypad1)) KeyDown(KeyCode.Keypad1);
        if (Input.GetKeyDown(KeyCode.Keypad2)) KeyDown(KeyCode.Keypad2);
        if (Input.GetKeyDown(KeyCode.Keypad3)) KeyDown(KeyCode.Keypad3);
        if (Input.GetKeyDown(KeyCode.Keypad4)) KeyDown(KeyCode.Keypad4);
        if (Input.GetKeyDown(KeyCode.Keypad5)) KeyDown(KeyCode.Keypad5);
        if (Input.GetKeyDown(KeyCode.Keypad6)) KeyDown(KeyCode.Keypad6);
        if (Input.GetKeyDown(KeyCode.Keypad7)) KeyDown(KeyCode.Keypad7);
        if (Input.GetKeyDown(KeyCode.Keypad8)) KeyDown(KeyCode.Keypad8);
        if (Input.GetKeyDown(KeyCode.Keypad9)) KeyDown(KeyCode.Keypad9);
        if (Input.GetKeyDown(KeyCode.LeftShift)) KeyDown(KeyCode.LeftShift);
        if (Input.GetKeyUp(KeyCode.LeftShift)) KeyUp(KeyCode.LeftShift);

#endif
    }

    #endregion

    #region Reaction to gesture events

    virtual public void FingerGestures_OnFingerLongPress(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerTap(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDoubleTap(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerSwipe(int fingerIndex, Vector2 startPos, FingerGestures.SwipeDirection direction, float velocity)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDown(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnDragEnd(Vector2 fingerPos)
    {
    }

    virtual public void FingerGestures_OnDragMove(Vector2 fingerPos, Vector2 delta)
    {
    }

    virtual public void FingerGestures_OnDragBegin(Vector2 fingerPos, Vector2 startPos)
    {
    }

    #region Drag & Drop Gesture

    virtual public void FingerGestures_OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        if (IsUILayer()) return;
    }

    virtual public void FingerGestures_OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        if (IsUILayer()) return;
    }

    #endregion

    #region Pinch & Pinch Gesture
    virtual public void FingerGestures_OnPinchBegin(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }

    virtual public void FingerGestures_OnPinchMove(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
    }

    virtual public void FingerGestures_OnPinchEnd(Vector2 fingerPos1, Vector2 fingerPos2)
    {

    }
    #endregion  //  Pinch & Pinch Gesture

    #region KeyBoard
    virtual public void Up()
    {

    }
    virtual public void Down()
    {

    }
    virtual public void Left()
    {

    }
    virtual public void Right()
    {

    }

    virtual public void KeyDown(KeyCode kc)
    {

    }
    virtual public void KeyUp(KeyCode kc)
    {

    }
    #endregion  // KeyBoard

    #endregion

    #region Utils

    // attempt to pick the scene object at the given finger position and compare it to the given requiredObject. 
    // If it's this object spawn its particles.
    bool CheckSpawnParticles(Vector2 fingerPos, GameObject requiredObject)
    {
        GameObject selection = PickObject(fingerPos);

        if (!selection || selection != requiredObject)
            return false;

        return true;
    }

    public Camera currentCamera;
    Ray ray;
    RaycastHit hit;
    int mask;

    virtual public bool IsUILayer()
    {
        if (!IsInScreen(Input.mousePosition))
            return true;

        return false;
    }

    public bool IsInScreen(Vector3 touchPos)
    {
        if (touchPos.x < 0) return false;
        if (touchPos.x > Screen.width) return false;
        if (touchPos.y < 0) return false;
        if (touchPos.y > Screen.height) return false;

        return true;
    }

    // Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
    public Vector3 GetWorldPos(Vector2 screenPos)
    {
        ray = currentCamera.ScreenPointToRay(screenPos);

        // we solve for intersection with z = 0 plane
        //float t = -ray.origin.z / ray.direction.z;

        return ray.GetPoint(-ray.origin.z / ray.direction.z);
    }

    // Return the GameObject at the given screen position, or null if no valid object was found
    public GameObject PickObject(Vector2 screenPos)
    {
        //  
        if (currentCamera == null)
        {
            currentCamera = Camera.main;
        }
        if (currentCamera != null)
        {
            ray = currentCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    public bool IsPickObject(Vector2 screenPos, GameObject target)
    {
        ray = currentCamera.ScreenPointToRay(screenPos);

        mask = 1 << target.layer;

        if (Physics.Raycast(ray, out hit, mask))
        {
            //Debuger.Log(hit.collider.name);
            return true;
        }

        return false;
    }

    public Vector3 PickObjectPos(Vector2 screenPos)
    {
        if (currentCamera != null)
        {
            ray = currentCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out hit))
                return hit.point;
        }

        return Vector3.zero;
    }
    #endregion

    #region MFUI

    public virtual void OnBeginCallBack()
    {

    }

    public virtual void OnClickCallBack()
    {
    }

    public virtual void OnMoveCallBack()
    {
    }

    public virtual void OnStationaryCallBack()
    {

    }
    #endregion

}
