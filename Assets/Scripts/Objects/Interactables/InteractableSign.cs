using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSign : InteractableBase {

    public string signText = "";
    bool Interacted = false;

    public override void OnInteract(Character character)
    {
        if (!Interacted)
        {
            DialogueBox.Show(signText);
            Interacted = true;
            character.MovementModel.SetFrozen(true,true,true);
        }
        else
        {
            DialogueBox.Hide();
            Interacted = false;
            character.MovementModel.SetFrozen(false,false,true);
        }
    }
}
