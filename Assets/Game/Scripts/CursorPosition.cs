using System;
using UnityEngine;

public static class CursorPosition
{
    public static RaycastHit RayHit()
    {
        RaycastHit hitInfo;
        Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity);
        return hitInfo;
    }

    public static Vector3 RayPosotion() => RayHit().point;
    public static GameObject RayObject() => RayHit().transform.gameObject;
}
