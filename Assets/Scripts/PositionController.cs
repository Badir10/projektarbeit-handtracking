using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    // Dieses Skript wurde nicht von mir, Badir Al-Sayyed erstellt
    // Setzt eine statische Instanz zu dem Skript
    public static PositionController Instance;

    [SerializeField] private GameObject point;          //Prefab der Eck-Kugeln
    [SerializeField] private GameObject tablePlane;     //Tisch Prefab
    private List<GameObject> pointList = new List<GameObject>();
    private bool buildState = true;

    public bool BuildState
    {
        get => buildState;
        set => buildState = value;
    }

    private Vector3 downLeft;
    private Vector3 downRight;
    // public List<GameObject> buttonAnchors = new List<GameObject>();
    private bool tablePosBool = false;
    
    

    private GameObject tablePlaneInstance;

    void Awake(){
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Die Variable buildState ist ein Boolean und repräsentiert den Zustand des Baumodus. Ist der Baumodus im Prozess, so ist der buildState = true;
        // Ist der Baumodus beendet, so wird der buildState = false; gesetzt.
        if(buildState){
            // Methode zum setzen der Punkte per Controller
            SetPoints();
            // Methode zum erstellen des Tisches
            InstanceTable();
        } else{
            // In der Methode kann der Nutzer per Stick, die Tischhöhe beeinflussen
            PositionTableWithStick();
        }
        // Die Methode löscht die gesamten instanzierten Elemente (Reset)
        DeleteInstance();
    }

    void CreateRectangle(GameObject posA, GameObject posB, GameObject posC){
        // Dies sind die Punkte von den instanzierten "Points"
        Vector3 p0 = posA.transform.localPosition;
        Vector3 p1 = posB.transform.localPosition;
        Vector3 p2 = posC.transform.localPosition;


        // Der Vektor zwischen dem Punkt 0 und Punkt 1 wird berechnet
        Vector3 v0 = p0 - p1;

        // Der Vektor zwischen dem Punkt 2 und Punkt 1 wird berechnet
        Vector3 v1 = p2 - p1;

        // Der zentrierte Punkt, von dem Viereck wird berechnet
        Vector3 center = p0 + (p2 - p0) / 2;

        // Die Höhe vom Center wird um 0.03 verringert, da die Höhe von den Punkten auf dem Tisch leicht versetzt sind.
        // Das liegt daran, da die erstellten Punkte auf dem Tisch instanziert wurden, und nicht in dem Tisch.
        center.y = center.y - 0.03f;
        //Referenzvector der Punkte p0 & p1 der Tischplatte wie das Objekt in der Welt instanziert wird
        Vector3 u0 = new Vector3(1,0,0);

        //Es wird davon ausgegangen, dass v0 der Vector der Vorderseite des Tisches ist (also p0 und p1 die zwei vorderen Eckpunke am Tisch sind
        //Skalar zwischen v0 und u0
        float scalar = Vector3.Dot(v0, u0);    
        //Längenberechnung der unterschiedlichen Vektoren
        float lengthv0 = Vector3.Magnitude(v0);
        float lengthv1 = Vector3.Magnitude(v1);
        float lengthu0 = Vector3.Magnitude(u0);
        //Winkelberechnung zwischen v0 und u0
        float beta = Mathf.Acos(scalar/(lengthu0*lengthv0));
        //Winkel umrechnen von rad zu grad
        beta = beta * 180 / Mathf.PI;
        //Gegenwinkel ausrechnen
        float alpha = 180- beta;
        Debug.Log("Winkel a: " + alpha + "; Winkel b: " + beta + "; Vector V0: " + v0.x + ", " + v0.y + ", " + v0.z + "; Scalar: " + scalar);

        float minAngle;
        float maxAngle;
        //Je nach Rotation von v0 muss ein anderer Rotationswinkel gewählt werden (abhängig ob v0 +/- in x-Richtung und +/- in z-Richtung verläuft, somit 8 unterschiedliche States)
        if (scalar >= 0)
        {
            //Ist das Skalar >= 0 ist beta der kleinere der beiden Winkel
            minAngle = beta;
            maxAngle = alpha;
            //Unterscheidung ob v0 in z-Richtung positiv oder negativ verläuft
            if (v0.z >= 0)
            {
                //Unterscheidung ob v0 in x-Richtung positiv oder negativ verläuft
                if (v1.x >= 0)
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, maxAngle, 0));
                }
                else
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, -minAngle, 0));
                }
            }
            else
            {
                //Unterscheidung ob v0 in x-Richtung positiv oder negativ verläuft
                if (v1.x >= 0)
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, minAngle, 0));
                }
                else
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, -maxAngle, 0));
                }
            }
        }
        else
        {
            //Ist das Skalar < 0 ist alpha der kleinere der beiden Winkel
            minAngle = alpha;
            maxAngle = beta;
            //Unterscheidung ob v0 in z-Richtung positiv oder negativ verläuft
            if (v0.z >= 0)
            {
                //Unterscheidung ob v0 in x-Richtung positiv oder negativ verläuft
                if (v1.x >= 0)
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, minAngle, 0));
                }
                else
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, -maxAngle, 0));
                }
            }
            else
            {
                //Unterscheidung ob v0 in x-Richtung positiv oder negativ verläuft
                if (v1.x >= 0)
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, maxAngle, 0));
                }
                else
                {
                    //Instanzierung des Tisches mit korrekter Rotation
                    tablePlaneInstance = Instantiate(tablePlane, center, Quaternion.Euler(0, -minAngle, 0));
                }
            }
        }
        //Skaliere nur die Tischplatte den Längen der jeweiligen Vektoren entsprechend
        tablePlaneInstance.transform.GetChild(0).transform.localScale = new Vector3(lengthv0, 0.03f, lengthv1);

        GameObject scaledTable = tablePlaneInstance.transform.GetChild(0).gameObject;


        // Dies ist die Skalierung der Tischbeine in Richtung Y;
        float legScaleY = tablePlaneInstance.transform.localPosition.y;
        // Dies ist die Skalierung der Tischbeine in Richtung X und Z;
        float legScaleXZ = 0.05f;

        // Die Eckpunkte von dem instanzierten Tisch werden berechnet
        Vector3 topRight = new Vector3(scaledTable.transform.localScale.x - legScaleXZ, -(tablePlaneInstance.transform.position.y), scaledTable.transform.localScale.z - legScaleXZ) / 2;
        Vector3 topLeft = new Vector3(-(scaledTable.transform.localScale.x) + legScaleXZ, -(tablePlaneInstance.transform.position.y), scaledTable.transform.localScale.z - legScaleXZ) / 2;
        downRight = new Vector3(scaledTable.transform.localScale.x - legScaleXZ, -(tablePlaneInstance.transform.position.y), -(scaledTable.transform.localScale.z) + legScaleXZ) / 2;
        downLeft = new Vector3(-(scaledTable.transform.localScale.x) + legScaleXZ, -(tablePlaneInstance.transform.position.y), -(scaledTable.transform.localScale.z) + legScaleXZ) / 2;
        
        // Die berechneten Eckpunkte werden in einem Array zusammengefügt
        Vector3[] legPosition = new[] {topRight, topLeft, downRight, downLeft};
        // Die Tischtextur wird zwischengespeichert, damit die erstellten Tischbeine, die gleiche Textur erhält, wie die Tischplatte
        MeshRenderer scaledTableMesh = scaledTable.GetComponent<MeshRenderer>();

        // Die Eckpunkte werden durchlaufen und erstellen an jedem Punkt ein Tischbein mit der korrekten Höhe und der Tischtextur
        foreach(Vector3 legpos in legPosition){
            GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leg.transform.localEulerAngles = tablePlaneInstance.transform.localEulerAngles;
            leg.transform.position = tablePlaneInstance.transform.position;
            leg.transform.localScale = new Vector3(legScaleXZ, legScaleY, legScaleXZ);
            leg.transform.SetParent(tablePlaneInstance.transform);
            leg.transform.localPosition = legpos;
            MeshRenderer legMesh = leg.GetComponent<MeshRenderer>();
            legMesh.material = scaledTableMesh.material;
        }
    }

    public Vector3 getLeftPos(){
        return downLeft;
    }

    public Vector3 getRightPos(){
        return downRight;
    }

    public bool getTablePosBool(){
        return tablePosBool;
    }

    private void SetPoints(){
        // Wenn der "SecondaryIndexTrigger" Button gedrückt wurde,
        // dann, wird eine Instanz von dem Prefab "point" an der Position vom Controller erstellt.
        // Die Instanz wird in der Liste PointList hinzugefügt.
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            GameObject pointInstance = Instantiate(point, position, Quaternion.identity);
            pointList.Add(pointInstance);

            // GameObject buttonAnchor = Instantiate(new GameObject("ButtonAnchor"), position, Quaternion.identity);
            // buttonAnchors.Add(buttonAnchor);
        }

        // Das ist die gleiche Abfrage wie im oberen Teil, nur mit dem anderen Controller
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            GameObject pointInstance = Instantiate(point, position, Quaternion.identity);
            pointList.Add(pointInstance);
            
            // GameObject buttonAnchor = Instantiate(new GameObject("ButtonAnchor"), position, Quaternion.identity);
            // buttonAnchors.Add(buttonAnchor);
        }
    }

    private void DeleteInstance(){
        // Wenn der Button Start vom linken Controller gedrückt wurde, werden alle Instanzierten Prefabs (Points, tablePlaneInstance) zerstört
        // Dies dient dazu um die gesetzten Points zu reseten, falls der instanzierte Point nicht korrekt platziert wurde
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            foreach(GameObject points in pointList){
                GameObject.Destroy(points);
            }
            pointList = new List<GameObject>();
            GameObject.Destroy(tablePlaneInstance);
            buildState = true;
        }
    }

    private void InstanceTable(){
        // Wenn die Liste Pointlist mit drei Points gefüllt ist, wird der buildState auf false gesetzt,
        // damit die Bedinung nur ein Mal aufgerufen wird, und die anderen Bedinungen, wie das Platzieren von weiteren Points verhindert wird.
        if(pointList.Count == 3){
            // Create Table
            buildState = false;
            GameObject pointA = pointList[0];
            GameObject pointB = pointList[1];
            GameObject pointC = pointList[2];
            // Der Methode CreateRectangle werden die 3 platzierten Punkte übergeben um eine Instanz vom Prefab "tablePlane" an den gesetzten Punkten zu erstellen.
            CreateRectangle(pointA, pointB, pointC);
            // Nachdem die Methode ausgeführt wurde und ein Tisch auf der Höhe der platzierten Points ist, werden die gesetzten visuellen Punkte aus der Scene entfernt
            foreach(GameObject point in pointList){
                GameObject.Destroy(point);
            }
        } 
    }

    private void PositionTableWithStick(){
        // Der Nutzer die Möglichkeit in der Methode die Punkte mit dem Controller Stick, die Höhe (Y-coordinate) vom Tisch zu verstellen.
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        // UP 0,99 -- DOWN -0,99

        // Wenn der Controller Stick unberührt ist, soll tablePosBool auf false gesetzt werden
        // Das ist dafür da, um die Buttons, die auf dem Tisch sind (Die Kind-Objekte) mit auf der Höhe des Tisches zu skalieren. 
        if(primaryAxis.y == 0 && secondaryAxis.y == 0){
            tablePosBool = false;
        }

        // Wenn der Controller Stick nach oben gerichtet ist, soll die Y Position von dem Tisch pro Fram um 0.0002f erhöht werden.
        // Das ist dazu da, um die Höhe des Tisches, nach der Erstellung, korrekt zu justieren.
        if(primaryAxis.y > 0.9f || secondaryAxis.y > 0.9f){
            tablePlaneInstance.transform.position = new Vector3(tablePlaneInstance.transform.position.x, tablePlaneInstance.transform.position.y + 0.0002f, tablePlaneInstance.transform.position.z);
            tablePosBool = true;
        }

        // Das ist die gleiche Bedinungung nur, dass der Stick vom Controller nach unten zeigt und die Tisch Höhe um 0.0002f verringert
        if(primaryAxis.y < -0.9f || secondaryAxis.y < -0.9f){
            tablePlaneInstance.transform.position = new Vector3(tablePlaneInstance.transform.position.x, tablePlaneInstance.transform.position.y - 0.0002f, tablePlaneInstance.transform.position.z);
            tablePosBool = true;
        }
    }
}