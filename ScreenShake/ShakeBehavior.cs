using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehavior : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Transform affected by the magnitude.\nIf it is empty, the transform concerned will be the one from the gameObject where the script is form.")]
    Transform _transform;

    // Duration of the shake
    float _shakeDuration;

    // Magnitude of the shake
    float _shakeMagnitude;

    // Defines if the next shake is getting down by time
    bool _isGettingDown;

    [Range(0, 5)]
    [SerializeField] 
    [Tooltip("The duration the shake lasts.")]
    // Variable to define the base duration when the shake is triggered without informations.
    float _maxShakeDuration;

    [Range(0, 5)]
    [SerializeField] 
    [Tooltip("The maximum magnitude the transform can be shaked.")]
    // Variable to define the base magnitude when the is triggered without informations.
    float _maxShakeMagnitude;

    /// <inheritdoc cref="_maxShakeDuration"/>
    public float ShakeDuration
    {
        get { return _maxShakeDuration; }
        set
        {
            if (value > 0) { _maxShakeDuration = value; }
            else { Debug.LogError($"ShakeDuration can not be changed. Value must be positive."); }
        }
    }

    /// <inheritdoc cref="_maxShakeMagnitude"/>
    public float ShakeMagnitude
    {
        get { return _maxShakeMagnitude; }
        set 
        { 
            if(value > 0) { _maxShakeMagnitude = value; }
            else { Debug.LogError($"ShakeMagnitude can not be changed. Value must be positive."); } 
        }
    }

    /// <summary>
    /// Initial position of the transform triggered. Is set in the Awake() function of the script
    /// </summary>
    Vector3 _initialPosition;

    private void Awake()
    {
        // Apply a transform if it is empty at launch
        if(_transform == null)
        {
            _transform = GetComponent<Transform>();
        }

        // Sets the initial position of the transform
        _initialPosition = _transform.position;
    }

    private void FixedUpdate()
    {
        //_shakeDuration is set at a positive value when a shake function is triggered
        if(_shakeDuration > 0)
        {
            // Defines if the trigger is getting down or not
            if(!_isGettingDown)
            {
                // Changes the position of the transform in a random value set inside the _shakeMagnitude magnitude.
                _transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;
            }
            else
            {
                // Changes the position of the transform in a random value set inside the _shakeMagnitude magnitude.
                // The magnitude is getting down at every frame to set it to 0.
                _transform.localPosition = _initialPosition + Random.insideUnitSphere * Mathf.Lerp(0, _maxShakeMagnitude, _shakeDuration/_maxShakeDuration);
            }

            _shakeDuration -= Time.fixedDeltaTime;
        }
        else
        {
            //reboot any variable to be sure the shake is stopped
            _shakeDuration = 0f;
            _transform.localPosition = _initialPosition;
            _isGettingDown = false;
        }
    }

    /// <summary>
    /// Trigger a shake with variables set in the Inspector
    /// </summary>
    public void TriggerShake()
    {
        _shakeDuration = _maxShakeDuration;
        _shakeMagnitude = _maxShakeMagnitude;
    }

    /// <summary>
    /// Trigger a shake that gets slower by time with variables set in the Inspector
    /// </summary>
    public void TriggerShakeToSlow()
    {
        _shakeDuration = _maxShakeDuration;
        _shakeMagnitude = _maxShakeMagnitude;
        _isGettingDown = true;
    }
}
