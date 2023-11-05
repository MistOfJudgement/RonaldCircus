using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehavior
{
    public void TakeDamage(int damage);
    public void StartBehavior();
    public void StopBehavior();
}
