using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class DinoControl : MonoBehaviour, IKillable
{
    [SerializeField] float jumpForce = 500f;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip collisionClip;
    
    void Awake()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mAudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        GameControl.Instance.NofifyWhenDead(this);
        mAudioSource.clip = jumpClip;
        mAudioSource.mute = SoundControl.Instance.IsFXMuted;
        mAudioSource.volume = SoundControl.Instance.FXVolume;
    }

    // Update is called once per frame
    void Update()
    {
        mAudioSource.mute = SoundControl.Instance.IsFXMuted;
        mAudioSource.volume = SoundControl.Instance.FXVolume;
        if(!GameControl.gameStopped)
        {
            mUpOrDown = CrossPlatformInputManager.GetAxisRaw("Vertical");
            
            //Press up button.
            if(mUpOrDown > 0 && mRb.velocity.y == 0)
            {
                mCanJump = true;
            }

            //Press down button.
            if(mUpOrDown<0 && mRb.velocity.y == 0)
                mAnim.SetBool("isDown", true);

            //Release down button.
            if(mUpOrDown==0 && mRb.velocity.y == 0)
               mAnim.SetBool("isDown", false);
        }
    }

    void FixedUpdate()
    {
        if(mCanJump)
        {
            mCanJump = false;
            mRb.velocity = Vector2.zero;
            mRb.AddForce(Vector2.up*jumpForce);
            mAudioSource.Play();
        }
    }

    public void Die()
    {
        mAudioSource.clip = collisionClip;
        mAudioSource.Play();
        mAnim.SetBool("isDead", true);
    }

    Animator mAnim;
    Rigidbody2D mRb;
    float mUpOrDown;
    GameControl mGameControl;
    AudioSource mAudioSource;
    bool mCanJump = false;

}
