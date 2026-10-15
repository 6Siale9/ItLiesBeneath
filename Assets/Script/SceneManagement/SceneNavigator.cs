using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    #region Instance
    private static SceneNavigator _instance;
    public static SceneNavigator Instance { get => _instance; set => _instance = value; }

    private void TrySetInstance()
    {
        if (SceneNavigator.Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            SceneNavigator.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion Instance

    [SerializeField] private List<string> _gameScenes = new List<string>();
    [SerializeField] private List<AParentManager> _managers = new List<AParentManager>();
    private Dictionary<AParentManager, bool> _managersState;

    [Header ("WIP")]
    [SerializeField] private List<string> _debugScenes = new List<string>();
    [SerializeField] private bool _launchDebugScenes = false;
    [SerializeField] private int _toLoadScene;
    
    public Dictionary<AParentManager, bool> ManagersState { get => _managersState; set => _managersState = value; }

    private void Awake()
    {
        TrySetInstance();
        SetUpDictionnaries();
        StartAllManagers();
    }

    private void SetUpDictionnaries()
    {
        foreach (AParentManager manager in _managers)
        {
            _managersState.Add(manager, false);
        }
    }

    private void StartAllManagers()
    {
        foreach (AParentManager manager in _managers)
        {
            manager.Initialize();
        }
    }

    public void TryMoveOn()
    {
        foreach (bool b in _managersState.Values)
        {
            if (!b)
            {
                return;
            }
        }
        LoadScene();
    }

    private void LoadScene()
    {
        if (_launchDebugScenes)
        {
            SceneManager.LoadScene(_debugScenes[_toLoadScene]);
        }
        else
        {
            SceneManager.LoadScene(_gameScenes[1]);
        }
    }
}
