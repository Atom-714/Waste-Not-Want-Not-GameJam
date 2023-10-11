using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    bool canAct { get; set; }
    public void Damage(int amount);
    public void Die();
}
