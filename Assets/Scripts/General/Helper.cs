using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper  {

    public static void SetSortingLayerForAllRenderers(Transform parent, string sortingLayerName) {
        SpriteRenderer[] renderers = parent.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in renderers)
        {
            sr.sortingLayerName = sortingLayerName;
        }
    }
}
