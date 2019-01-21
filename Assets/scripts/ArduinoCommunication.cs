using UnityEngine;
using System.IO.Ports;
using System;


public class ArduinoCommunication : MonoBehaviour 
{

    SerialPort stream;

    public FMODUnity.StudioEventEmitter eventEmit1;
    public FMODUnity.StudioEventEmitter eventEmit2;
    public FMODUnity.StudioEventEmitter eventEmit3;

    public bool t1On = false;
    public bool t2On = false;
    public bool t3On = false;

    private void Start () 
	{
        stream = new SerialPort("COM6", 9600);
        stream.ReadTimeout = 50;
        try
        {
            stream.Open();
        }
        catch
        {
            Debug.Log("Not Open");
        }
    }

    public string ReadFromArduino(int timeout = 0)
    {
        stream.ReadTimeout = timeout;
        try
        {
            return stream.ReadLine();
        }
        catch 
        {
            return "Werktnie";
        }
    }

    private void Update () 
	{
        string reading = ReadFromArduino(25);

        if (reading == "t1ONN" || Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!eventEmit1.IsPlaying())
            {
                eventEmit1.Play();
            }
            t1On = true;
        }
        if (reading == "t2ONN" || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!eventEmit2.IsPlaying())
            {
                eventEmit2.Play();
            }
            t2On = true;
        }
        if (reading == "t3ONN" || Input.GetKeyDown(KeyCode.Alpha3)) t3On = true;



        if (reading == "t1OFF" || Input.GetKeyUp(KeyCode.Alpha1))
        {
            eventEmit1.Stop();
            t1On = false;
        }
        if (reading == "t2OFF" || Input.GetKeyUp(KeyCode.Alpha2))
        {
            eventEmit2.Stop();
            t2On = false;
        }
        if (reading == "t3OFF" || Input.GetKeyUp(KeyCode.Alpha3)) t3On = false;
    }
}
