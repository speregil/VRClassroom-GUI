using UnityEngine;
using System.Collections;

public class AudioRecorder : MonoBehaviour {

    private AudioClip TempClip;

    public void Record()
    {
        TempClip = Microphone.Start(null, false, 1000, 44100);
    }

    public void Save()
    {
        int lastSample = Microphone.GetPosition(null);
        Microphone.End(null);

        float[] data = new float[lastSample];
        TempClip.GetData(data, 0);

        AudioClip myAudioClip = AudioClip.Create("",lastSample,TempClip.channels,TempClip.frequency, false);
        //Debug.Log(myAudioClip);
        myAudioClip.SetData(data, 0);
        SavWav.Save("myfile", myAudioClip);
    }
}
