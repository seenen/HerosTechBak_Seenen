using UnityEngine;

public static class CameraExtensions
{

    public static void LayerCullingShow(this Camera cam, int layerMask)
    {
        if (cam == null)
            return;

        cam.cullingMask |= layerMask;
    }

    public static void LayerCullingShow(this Camera cam, string layer)
    {
        if (cam == null)
            return;

        LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
    }

    public static void LayerCullingHide(this Camera cam, int layerMask)
    {
        if (cam == null)
            return;

        cam.cullingMask &= ~layerMask;
    }

    public static void LayerCullingHide(this Camera cam, string layer)
    {
        if (cam == null)
            return;

        LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
    }

    public static void LayerCullingToggle(this Camera cam, int layerMask)
    {
        if (cam == null)
            return;

        cam.cullingMask ^= layerMask;
    }

    public static void LayerCullingToggle(this Camera cam, string layer)
    {
        if (cam == null)
            return;

        LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
    }

    public static bool LayerCullingIncludes(this Camera cam, int layerMask)
    {
        if (cam == null)
            return false;

        return (cam.cullingMask & layerMask) > 0;
    }

    public static bool LayerCullingIncludes(this Camera cam, string layer)
    {
        if (cam == null)
            return false;

        return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
    }

    public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn)
    {
        if (cam == null)
            return;

        bool included = LayerCullingIncludes(cam, layerMask);
        if (isOn && !included)
        {
            LayerCullingShow(cam, layerMask);
        }
        else if (!isOn && included)
        {
            LayerCullingHide(cam, layerMask);
        }
    }

    public static void LayerCullingToggle(this Camera cam, string layer, bool isOn)
    {
        if (cam == null)
            return;

        LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
    }

    public static void LayerRemove(this Camera cam, int layer)
    {
        if (cam == null)
            return;

        cam.cullingMask &= ~(1 << layer);

    }
    public static void LayerAdd(this Camera cam, int layer)
    {
        if (cam == null)
            return;

        cam.cullingMask |= (1 << layer);

    }

}