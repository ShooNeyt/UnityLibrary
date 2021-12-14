using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionBehavior : MonoBehaviour
{
    [Tooltip("Delay until the next scene loads")]
    [SerializeField] private float _transitionTime;

    [SerializeField]
    [Tooltip("Gets the animator of the Transition.\nIf empty, it will get the animator from the Game Object.")]
    private Animator _animator;

    [SerializeField]
    [Tooltip("Name of the trigger in the animator controller.\nIf empty, it will get the first animator parameter.")]
    private string _triggerName;

    // Variable that gets the scene to launch when called
    private string _sceneName;

    void Awake()
    {
        // Gets the animator from the Game Object if it was not informed in the Inspector
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();

            if(_animator == null)
            {
                Debug.LogError("Did not get the animator controller. The transition will not launch.");
            }
        }

        // Prevent the user the trigger name is not informed.
        if(_triggerName == "")
        {
            Debug.LogWarning("The trigger name is not informed. First trigger in the animator will automatically launch");
        }
    }

    // Résumé :
    //      Starts the transition animation and sets a delay before loading next scene
    public void LoadNextScene(string sceneName)
    {
        // Only launches the animation if the script detect an animator
        if(_animator != null)
        {
            if(_triggerName == "")
            {
                _animator.SetTrigger(0);
            }
            else
            {
                _animator.SetTrigger(_triggerName);
            }

        }

        Invoke("LaunchScene", _transitionTime);
        _sceneName = sceneName;
    }

    //Launches the scene asked
    private void LaunchScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
