using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    public void StartTask();
    public void SwitchToTask();
    public void SwitchFromTask();
}
