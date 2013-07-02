using UnityEngine;
using System.Collections.Generic;

public enum SoundEffectType
{
    Snow,
    Bump,
	Crack4,
	Crack6
}
public enum VoiceEffectType
{
    Dangling,
    Dangling_Idle,
    Idle,
    Falling,
    Jump,
    Tension,
	Victory
}
public enum CharacterType
{
    Aegir,
    Oberon
}

public enum LevelType
{
    Morning,
    Noon,
    Evening,
    Menu
}
public class Sounds : MonoBehaviour
{
	public static float _MusicVolume;
    public float MusicVolume = 0.9f;
    public float SoundEffectsVolume = 0.5f;

	public GameObject Aegir;
	public GameObject Oberon;

    public AudioClip BackgroundNoon;
    public AudioClip BackgroundMorning;
    public AudioClip BackgroundEvening;
    public AudioClip BackgroundMenu;

    public static AudioClip sBackgroundNoon;
    public static AudioClip sBackgroundMorning;
    public static AudioClip sBackgroundEvening;
    public static AudioClip sBackgroundMenu;

    public static AudioSource _Audio1;
    public static AudioSource _Audio2;
    public static AudioSource _VoiceAegir;
    public static AudioSource _VoiceOberon;
    public static AudioSource _SoundAegir;
    public static AudioSource _SoundOberon;

    private static int CurrentAudio = 0;
    private static float DesiredVolume1 = 1f;
    private static float DesiredVolume2 = 0f;
    private float currentVolume1 = 1f;
    private float currentVolume2 = 0f;

    public List<AudioClip> Aegir_Dangling;
    public List<AudioClip> Aegir_Dangling_Idle;
    public List<AudioClip> Aegir_Idle;
    public List<AudioClip> Aegir_Falling;
    public List<AudioClip> Aegir_Jump;
    public List<AudioClip> Aegir_Tension;
	public List<AudioClip> Aegir_Victory;

    public List<AudioClip> Oberon_Dangling;
    public List<AudioClip> Oberon_Dangling_Idle;
    public List<AudioClip> Oberon_Idle;
    public List<AudioClip> Oberon_Falling;
    public List<AudioClip> Oberon_Jump;
    public List<AudioClip> Oberon_Tension;
	public List<AudioClip> Oberon_Victory;
	
    public List<AudioClip> BumpEffects;
	public List<AudioClip> CrackingEffects4;
	public List<AudioClip> CrackingEffects6;
    //	public List<AudioClip> SnowEffects;

    //	private static int currentSoundEffectIndex;
    public static Dictionary<SoundEffectType, List<AudioClip>> SoundEffectsDict = new Dictionary<SoundEffectType, List<AudioClip>>();
    public static Dictionary<VoiceEffectType, List<AudioClip>> VoicesAegir = new Dictionary<VoiceEffectType, List<AudioClip>>();
    public static Dictionary<VoiceEffectType, List<AudioClip>> VoicesOberon = new Dictionary<VoiceEffectType, List<AudioClip>>();
    // Use this for initialization

    void Start() 
	{
		AudioSource[] sources = Camera.mainCamera.GetComponents<AudioSource>();
		
		_MusicVolume = MusicVolume;
		
		_Audio1 = sources[0];
		_Audio2 = sources[1];
		_Audio1.loop = true;
		_Audio2.loop = true;
		
		
		CurrentAudio = 0;
		
		_Audio1.volume = MusicVolume;
		_Audio2.volume = MusicVolume;
		
		AudioSource[] aegirAudio = Aegir.GetComponents<AudioSource>();
		_VoiceAegir = aegirAudio[0];
		_SoundAegir = aegirAudio[1];
		
		AudioSource[] oberonAudio = Oberon.GetComponents<AudioSource>();
		_VoiceOberon = oberonAudio[0];
		_SoundOberon = oberonAudio[1];
		
		sBackgroundNoon = BackgroundNoon;
		sBackgroundMorning = BackgroundMorning;
		sBackgroundEvening = BackgroundEvening;
		sBackgroundMenu = BackgroundMenu;
		
		SoundEffectsDict.Clear();
		SoundEffectsDict.Add(SoundEffectType.Bump,BumpEffects);
		SoundEffectsDict.Add(SoundEffectType.Crack4,CrackingEffects4);
		SoundEffectsDict.Add(SoundEffectType.Crack6,CrackingEffects6);
//		SoundEffectsDict.Add(SoundEffectType.Snow,);
		
		VoicesAegir.Clear();
	    VoicesAegir.Add(VoiceEffectType.Dangling,		    Aegir_Dangling	);
	    VoicesAegir.Add(VoiceEffectType.Dangling_Idle,		Aegir_Dangling_Idle	);
	    VoicesAegir.Add(VoiceEffectType.Idle,		        Aegir_Idle);
	    VoicesAegir.Add(VoiceEffectType.Falling,		    Aegir_Falling	);
	    VoicesAegir.Add(VoiceEffectType.Jump,		        Aegir_Jump	);
    	VoicesAegir.Add(VoiceEffectType.Tension,		    Aegir_Tension	);
		VoicesAegir.Add(VoiceEffectType.Victory,		    Aegir_Victory	);
		
		VoicesOberon.Clear();
        VoicesOberon.Add(VoiceEffectType.Dangling,		    Oberon_Dangling	);
	    VoicesOberon.Add(VoiceEffectType.Dangling_Idle,		Oberon_Dangling_Idle	);
	    VoicesOberon.Add(VoiceEffectType.Idle,		        Oberon_Idle);
	    VoicesOberon.Add(VoiceEffectType.Falling,		    Oberon_Falling	);
	    VoicesOberon.Add(VoiceEffectType.Jump,		        Oberon_Jump	);
	    VoicesOberon.Add(VoiceEffectType.Tension,		    Oberon_Tension	);
		VoicesOberon.Add(VoiceEffectType.Victory,		    Oberon_Victory	);
		
		
		PlayLevelBackground(LevelType.Morning);
		
	}

    public static void PlayVoiceEffect(VoiceEffectType voicetype, CharacterType chartype)
    {
        List<AudioClip> clips = null;
		AudioSource source = null;
        switch (chartype)
        {
            case CharacterType.Aegir:
                clips = VoicesAegir[voicetype];
				source = _VoiceAegir;
                break;
            case CharacterType.Oberon:
                clips = VoicesOberon[voicetype];
				source = _VoiceOberon;
                break;
			
        }
		if(clips.Count == 0)
		{
			Debug.LogWarning(string.Format("No voice effects on {0} for {1} assigned",voicetype.ToString(),chartype.ToString()));
			return;
		}
        int rand = Random.Range(0, clips.Count);

		
        source.PlayOneShot(clips[rand]);
    }

    public static void PlaySoundEffect(SoundEffectType type, CharacterType charType)
    {
		AudioSource source = null;
        switch (charType)
        {
            case CharacterType.Aegir:
				source = _SoundAegir;
                break;
            case CharacterType.Oberon:
				source = _SoundOberon;
                break;
			
        }
		
        List<AudioClip> clips = SoundEffectsDict[type];
        int rand = Random.Range(0, clips.Count);

        source.PlayOneShot(clips[rand]);
    }
    public static void PlayLevelBackground(LevelType level)
    {
        ++CurrentAudio;
        CurrentAudio %= 2;
        AudioSource nextAudio;

        if (CurrentAudio == 0)
        {
            nextAudio = _Audio1;
            DesiredVolume1 = _MusicVolume;
            DesiredVolume2 = 0f;
        }
        else
        {
            nextAudio = _Audio2;
            DesiredVolume1 = 0f;
            DesiredVolume2 = _MusicVolume;
        }

        if (nextAudio == null) return;
        switch (level)
        {
            case LevelType.Morning:
                nextAudio.clip = sBackgroundMorning;
                break;
            case LevelType.Noon:
                nextAudio.clip = sBackgroundNoon;
                break;
            case LevelType.Evening:
                nextAudio.clip = sBackgroundEvening;
                break;
            case LevelType.Menu:
                nextAudio.clip = sBackgroundMenu;
                break;
        }
        nextAudio.Play();

    }

    // Update is called once per frame
    void Update()
    {


        float lerpVal = Mathf.Clamp01(0.05f * Time.deltaTime * 60f);

        currentVolume1 = Mathf.Lerp(currentVolume1, DesiredVolume1, lerpVal);
        currentVolume2 = Mathf.Lerp(currentVolume2, DesiredVolume2, lerpVal);

        _Audio1.volume = currentVolume1;
        _Audio2.volume = currentVolume2;
    }
}
