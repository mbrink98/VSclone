using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;


    [SerializeField] private AudioSource backgroundPlayer;
    [SerializeField] private AudioSource soundEffectPlayer;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip expHit;
    [SerializeField] private AudioClip enemyHit;
    [SerializeField] private AudioClip gunShot;
    [SerializeField] private AudioClip shotgunShot;
    [SerializeField] private AudioClip laserShot;
    public float backgroundVolume=0.3f;
    public float soundeffectVolume=0.3f;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("SoundManager is NULL");
            }
            return _instance;
        }
    }

    void Awake()
    {
        this.backgroundPlayer.loop = true;
        this.backgroundPlayer.clip = backgroundMusic;
        this.backgroundPlayer.volume = backgroundVolume;
       _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPlayerShoot(string type)
    {
        switch (type)
        {
        case "Gun":
            soundEffectPlayer.PlayOneShot(gunShot, soundeffectVolume);
            break;
        case "Shotgun":
            soundEffectPlayer.PlayOneShot(shotgunShot, soundeffectVolume);
            break;
        case "Laser":
            soundEffectPlayer.PlayOneShot(laserShot, soundeffectVolume);
            break;
        default:
            break;
        }
        
    }
    public void playEnemyHit()
    {
        soundEffectPlayer.PlayOneShot(enemyHit, soundeffectVolume);
    }
    public void playExpHit()
    {
        soundEffectPlayer.PlayOneShot(expHit, soundeffectVolume);
    }
}
