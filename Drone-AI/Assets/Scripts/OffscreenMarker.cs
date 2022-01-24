using System.Collections.Generic;
using UnityEngine;
class OffscreenMarker : MonoBehaviour
{
    public Texture Icon = null;
    public Texture Arrow = null;
    public Color Color = Color.white;

    void Start()
    {
        var instance = OffscreenMarkersCameraScript.Instance();
        if (instance)
        {
            instance.Register(this);
        }
    }
}