using UnityEngine;
using System.Collections;

public class speak : MonoBehaviour {
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
	public int lenguage;
	public int att;
	public int pause;
	public int call;
	public bool f;
	
	// Use this for initialization
	void Start () {
		f=false;
		att = 0;
		pause = 0;
        lenguage = 1;//GameObject.Find("PlaneO").GetComponent<lenguage>().l;
		currentClip = 1;
		num = 0;
		call = 0;
		nameAudio = "videoen";
		nameAnimation="";
		nameAnimationEn="animen";
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
	
	public void pauseAnimation(){
		pause = 1;
	}
	
	public void unpauseAnimation(){
		pause = 0;
	}
	
	IEnumerator playAudio(){
		//Debug.Log (animator.StopPlayBack());  
		
		//Debug.Log (audio.clip.length);
		for (int i = 0; i < 17+lenguage*320; i++) {

			bool distracted=GameObject.Find("Scripts").GetComponent<IsLookingAt>().isStudentDistracted;
			//Debug.Log ("Distraido: "+distracted);
			//Debug.Log ("call: "+call);
			if (!audio.isPlaying) {			
				if ((att != 0 && call==1)||(distracted==true && call==1)) {
					i--;
					if (lenguage == 0) {
						audio.clip = attEnglishAudioClip [att - 1];
						nameAnimation = "atten";
					} else {
						audio.clip = attSpanishAudioClip [att - 1];
						nameAnimation = "attsp";
					}
					//Debug.Log (nameAnimation + att);
					///Debug.Log (audio.clip.length);
					animator.Play (nameAnimation + att);
					audio.Play ();
					
					yield return new WaitForSeconds (audio.clip.length);
					audio.Stop ();

					call=0;
					pause=0;
					att++;

					if (att > 4) {
						pause = 1;						
					}

				}else{
					if(pause==0){
						if (lenguage == 0 ) {
							audio.clip = englishAudioClip [currentClip - 1];
							nameAnimation = nameAnimationEn;
						} else {
							audio.clip = spanishAudioClip [currentClip - 1];
							nameAnimation = nameAnimationSp;
						}
						//Debug.Log (nameAnimation + currentClip);
						//Debug.Log (audio.clip.length);
						animator.Play (nameAnimation + currentClip);
						audio.Play ();
						
						yield return new WaitForSeconds (audio.clip.length);
						audio.Stop ();
						currentClip++;
						
						if ((currentClip > 17 && lenguage == 0)||(currentClip > 49 && lenguage == 1)) {
							pause=1;
							f=true;
						}

						distracted=GameObject.Find("Scripts").GetComponent<IsLookingAt>().isStudentDistracted;

						if(distracted==true&&call==0){
							if(att==0){
								att++;
							}
							call=1;						
						}
					}
				}
				
			} else {
				//Debug.Log ("Esperando 1" );
				yield return new WaitForSeconds (1);
			}
		}
	}
	
	public void attention(){
		if(att==0){
			att++;
		}
		call = 1;
	}	
}

