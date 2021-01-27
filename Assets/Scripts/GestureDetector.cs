using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Gesture
{
    //Speichert die erstellten Gesten mit Namen, den Fingerdaten und den zugehörigen Events ab
    public string name;
    public List<Vector3> fingerData;
    public UnityEvent onRecognized;
    public UnityEvent notRecognized;
} 

public class GestureDetector : MonoBehaviour
{
    //Diese Klasse speichert gesten die nach drücken der Leertaste gemacht werden ab und ermöglicht es diese als
    //neue Geste für interaktionen mit Handtracking zu verwenden
    //
    
    //Grenzwert für die erkennung einer Geste
    public float threshold = 0.1f;
    //OVR Skeleton ist von Oculus bereitgestellt und speichert die Knochendaten der Hand in abrufbaren Variablen (Vektoren)
    public OVRSkeleton skeleton;
    //Eine Liste der erkannten Gesten
    public List<Gesture> gestures;
    public bool debugMode = true;

    //die Knochen aus der OVRSkeleton Klasse von Oculus
    private List<OVRBone> fingerBones;

    //Speichert die vorherige Geste ab, damit diese voneinander unterschieden werden können
    private Gesture previousGesture;

    public OVRHand hand;

    void Start()
    {
        //Speichert die Rechte Hand, welche sich in der Szene befindet in dieser Variable ab
        //(Dies bedeutet, dass die Gesten nur von der rechten Hand erfasst werden)
        hand = GameObject.Find("OVRCustomHandPrefab_R").GetComponent<OVRHand>();
        skeleton = hand.GetComponent<OVRCustomSkeleton>();
        fingerBones = new List<OVRBone>(skeleton.Bones);
        previousGesture = new Gesture();
    }

    void Update()
    {
        if (debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            //Nur, wenn debug Mode an ist werden die Gesten bei Knopfdruck gespeichert, damit nicht durchgehend
            //neue Gesten gespeichert werden, falls man diese einfach nur nutzen möchte
            
            fingerBones = new List<OVRBone>(skeleton.Bones); // Löschen wenn es nicht funktioniert
            Save();
        }

        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        
        if (!hasRecognized)
        {
            if (!previousGesture.Equals(new Gesture()))
            {
                Debug.Log("No Gesture recognized!");
                previousGesture.notRecognized.Invoke();
            }
            
        }
        

        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("New gesture found! " + currentGesture.name);
            if (!previousGesture.Equals(new Gesture()))
            {
                previousGesture.notRecognized.Invoke();
            }
            currentGesture.onRecognized.Invoke();
        }
        previousGesture = currentGesture;
    }

    //Methode, welche die neuen Gesten speichert
    void Save()
    {
        //Neue Geste wird erstellt
        Gesture g = new Gesture();
        //Ihr wird ein Name gegeben
        g.name = "New Gesture";
        //Die Vektoren werden vorerstellt
        List<Vector3> data = new List<Vector3>();
        
        //Jede Knochenposition wird in der soeben erstellten Vektorliste gespeichert
        foreach (var bone in fingerBones)
        {
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerData = data;
        
        //Alles wird in der globalen gesture-Variable (mit dem Struct Gesture als Vorbild) gespeichert
        gestures.Add(g);
    }

    //Methode die eine Geste mit der erstellten Liste abgleicht und dann ein Event auslöst
    Gesture Recognize()
    {
        //Lokale Gestenvariable wird erstellt
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        //die Abstände der zu erkennenden Geste wird mit den Gesten in der gestures-Liste abgeglichen
        //Eine Geste wird dann erkannt, wenn die abstände der Knochenpositionen mit der gestenliste abgeglichen wird
        //und dort mit einem vorher erstellten Eintrag übereinstimmt
        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerData[i]);
                if (distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }
                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                currentGesture = gesture;
            }
        }
        return currentGesture;
    }
}
