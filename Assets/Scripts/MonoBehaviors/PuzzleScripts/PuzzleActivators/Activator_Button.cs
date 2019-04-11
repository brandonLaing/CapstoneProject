using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator_Button : Activator, IInteractable
{
  public Transform[] objectToActivate;

  private bool _ableToInteract = true;
  public bool AbleToInteract
  {
    get { return _ableToInteract; }
    set { _ableToInteract = value; }
  }

  public override void Activate()
  {
    for (int i = 0; i < objectToActivate.Length; i++)
    {
      objectToActivate[i].GetComponent<IActivateable>().Activate();
    }
  }

  public void Interact()
  {
    if (AbleToInteract)
      Activate();
  }

  public override void Lock()
  {
    AbleToInteract = false;
  }
}
