using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class ActionQueue
{
    public static Queue<ActionTypes> queue = new Queue<ActionTypes>();

    public static void Add(ActionTypes actionType)
    {
        queue.Append(actionType);
    }

    public static bool IsAvailableAction()
    {
        return queue.Count > 0;
    }

    public static ActionTypes GetNext()
    {
        ActionTypes action = queue.Dequeue();
        return action;
    }
}
