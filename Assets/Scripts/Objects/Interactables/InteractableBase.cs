using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    virtual public void OnInteract(Character character)
    {
        Debug.LogWarning("OnInteract function not implemented!");
    }
}
