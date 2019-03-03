using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator_Button : Activator, IInteractable
{
  private bool _ableToInteract = true;
  public bool AbleToInteract
  {
    get { return _ableToInteract; }
    set { _ableToInteract = value; }
  }

  public override void Activate()
  {
    throw new System.NotImplementedException();
  }

  public void Interact()
  {
    if (AbleToInteract)
      Activate();
  }
}
