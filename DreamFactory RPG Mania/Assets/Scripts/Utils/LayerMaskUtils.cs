using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskUtils
{
    public static bool IsGameObjectInLayerMask(GameObject targetGameObject, LayerMask targetLayerMask)
    {
        return (targetLayerMask == (targetLayerMask | (1 << targetGameObject.layer)));
    }
}
