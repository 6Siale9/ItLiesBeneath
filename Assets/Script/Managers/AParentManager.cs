using UnityEngine;

public abstract class AParentManager : MonoBehaviour
{
    protected void SetAsReady()
    {
        SceneNavigator.Instance.ManagersState[this] = true;
        SceneNavigator.Instance.TryMoveOn();
    }

    public abstract void Initialize();
}
