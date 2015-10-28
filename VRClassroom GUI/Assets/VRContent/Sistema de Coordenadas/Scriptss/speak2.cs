using UnityEngine;
using System.Collections;

public class speak2 : MonoBehaviour {
	public AudioClip[] spanishAudioClip;
	public AudioClip[] englishAudioClip;
	public AudioClip[] attSpanishAudioClip;
	public AudioClip[] attEnglishAudioClip;
	AudioSource audio;
	private int currentClip;
	public Animator animator;
	
	private string nameAudio;
	private string nameAnimation;
	private string nameAnimationEn;
	private string nameAnimationSp;
	private int num;
	private int lenguage;
	private int att;
	private int pause;
	private int call;
	
	// Use this for initialization
	void Start () {
		att = 0;
		pause = 0;
		lenguage=0;
		currentClip = 1;
		num = 0;
		call = 0;
		nameAudio = "videoen";
		nameAnimation="";
		nameAnimationEn="animenex";
		nameAnimationSp="anim";
		Animation animation = GetComponent<Animation>(); 
		animator = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		animator.Play("interruption");
		
		StartCoroutine(playAudio());
	}
	
	void Update() {
		
	}
	
	void setLenguage(int l){
		lenguage=l;
	}
	
	void pauseAnimation(){
		pause = 1;
	}
	
	void unpauseAnimation(){
		pause = 0;
	}
	
	IEnumerator playAudio(){
		//Debug.Log (animator.StopPlayBack());
		
		//Debug.Log (audio.clip.length);
		for (int i = 0; i < 17+lenguage*32; i++) {
			if (!audio.isPlaying&&pause==0) {			
				
				if (att != 0 && call==1) {
					if (lenguage == 0) {
						audio.clip = attEnglishAudioClip [att - 1];
						nameAnimation = "atten";
					} else {
						audio.clip = attSpanishAudioClip [att - 1];
						nameAnimation = "attsp";
					}
					Debug.Log (nameAnimation + att);
					Debug.Log (audio.clip.length);
					animator.Play (nameAnimation + att);
					audio.Play ();
					
					yield return new WaitForSeconds (audio.clip.length);
					audio.Stop ();
					
					if (att > 4) {
						pause = 1;						
					}
				}else{
					
					if (lenguage == 0 ) {
						audio.clip = englishAudioClip [currentClip - 1];
						nameAnimation = nameAnimationEn;
					} else {
						audio.clip = spanishAudioClip [currentClip - 1];
						nameAnimation = nameAnimationSp;
					}
					Debug.Log (nameAnimation + currentClip);
					Debug.Log (audio.clip.length);
					animator.Play (nameAnimation + currentClip);
					audio.Play ();
					
					yield return new WaitForSeconds (audio.clip.length);
					audio.Stop ();
					currentClip++;
					
					if ((currentClip > 17 && lenguage == 0)||(currentClip > 49 && lenguage == 1)) {
						pause=1;
					}
				}
				
			} else {
				Debug.Log ("Esperando " +audio.clip.length);
				yield return new WaitForSeconds (audio.clip.length);
			}
		}
	}
	
	public void attention(){
		att++;
		call = 1;
	}	
}

