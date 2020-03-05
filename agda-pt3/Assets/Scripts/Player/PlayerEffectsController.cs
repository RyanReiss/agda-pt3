using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
    public GameObject greenMuzzleFlashPrefab;
    public GameObject yellowMuzzleFlashPrefab;
    public GameObject whiteMuzzleFlashPrefab;
    public GameObject whiteBulletImpact;
    public GameObject yellowBulletImpact;
    public GameObject greenBulletImpact;

    private static PlayerEffectsController _instance;
    public static PlayerEffectsController Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public GameObject GetEffect(string effect){
        switch (effect)
        {
            case "greenFlash":
                return ObjectPoolingController.Instance.GetPooledObject(greenMuzzleFlashPrefab);
            case "yellowFlash":
                return ObjectPoolingController.Instance.GetPooledObject(yellowMuzzleFlashPrefab);
            case "whiteFlash":
                return ObjectPoolingController.Instance.GetPooledObject(whiteMuzzleFlashPrefab);
            case "whiteBulletImpact":
                return ObjectPoolingController.Instance.GetPooledObject(whiteBulletImpact);
            case "yellowBulletImpact":
                return ObjectPoolingController.Instance.GetPooledObject(yellowBulletImpact);
            case "greenBulletImpact":
                return ObjectPoolingController.Instance.GetPooledObject(greenBulletImpact);
            default:
                return null;
        }
        
    }
}
