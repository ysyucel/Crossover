using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;

public class TestMyStackController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    GameObject stackStartPoint, stackFinishPoint;
    GameManager gm;
    [SerializeField] int gradeCount = 0;
    [SerializeField] List<Vector3> jengaTowerCenters;
    [SerializeField]List<string> grades;
    [SerializeField] List<GameObject> cameraCenters;
    List<GameObject> pieces;
    // Start is called before the first frame update
    void Awake() {
        stackStartPoint = GameObject.FindGameObjectWithTag("StartPoint");
        stackFinishPoint = GameObject.FindGameObjectWithTag("FinishPoint");
        gm = FindAnyObjectByType<GameManager>();
        jengaTowerCenters = new List<Vector3>();
        grades = new List<string>();
        cameraCenters = new List<GameObject>();
        pieces = new List<GameObject>();
    }


    public void StartTestMyStack () {
        gm.TakeConnectionRequest();
    }
    public void DataDownloaded () {
        pieces.Clear();
        Debug.Log("1");
        DecideHowMuchGradeExist();
        gradeCount = DecideHowMuchGradeExist();
        DecideJengaTowerCenters();

        Debug.LogError(gradeCount);
        for(int i = 0; i < gradeCount; i++) {
            List<StudentData> seperatedGradesData = new List<StudentData>();
            foreach(StudentData item in gm.studentDatas) {
                if (item.grade.Equals(grades[i])) {
                    seperatedGradesData.Add(item);
                }
            }
            CreateJengaStacks(seperatedGradesData,i);
        }

        virtualCamera.Follow = cameraCenters[1].transform;
        virtualCamera.LookAt = cameraCenters[1].transform;
        gm.StackReady();
       // ActivatePhysics();
    }
    public void ActivatePhysics () {
        foreach(GameObject item in pieces) {
            if (item.GetComponent<JengaPiece>().glassObject.activeSelf) {
                item.SetActive ( false );
            } else {
                item.AddComponent<Rigidbody>();
            }
        }
    }
    public void NewCameraTarget (int index) {
        virtualCamera.Follow = cameraCenters[index].transform;
        virtualCamera.LookAt = cameraCenters[index].transform;
    }
    public int DecideHowMuchGradeExist () {

        Debug.Log("1");
        int count = 0;
        grades.Clear();
        foreach(StudentData item in gm.studentDatas) {
            string grade = item.grade;
            if (!grades.Contains(grade)) {
                grades.Add(grade);
            }
        }
        count = grades.Count;
        return count;
    }
    public void DecideJengaTowerCenters () {

        Debug.Log("1");
        jengaTowerCenters.Clear();
        cameraCenters.Clear();
        for(int i = 0; i < gradeCount; i++) {
            jengaTowerCenters.Add(new Vector3(stackStartPoint.transform.position.x + ((i+1)*(Vector3.Distance(stackStartPoint.transform.position, stackFinishPoint.transform.position) / (gradeCount + 1))), 1.5f, 0f));
            GameObject go = new GameObject();
            go.transform.position = jengaTowerCenters.Last();
            go.AddComponent<RotateWithMouse>();
            cameraCenters.Add(go);
        }
    }


    public void CreateJengaStacks (List<StudentData> studentData,int stackOrder) {
        
        Debug.Log("1");
        // Order the list by the 'Value' property in ascending order
        List<StudentData> orderedList = studentData.OrderBy(obj => obj.domain).ToList();
        List<StudentData> orderedList1 = orderedList.OrderBy(obj => obj.cluster).ToList();
        List<StudentData> orderedList2 = orderedList1.OrderBy(obj => obj.id).ToList();
        int pieceCountCurrent = 0;
        bool isRotate = false;
        Vector3 rotationAngles;
        foreach (StudentData item in orderedList2) {
            if (isRotate) { rotationAngles = new Vector3(0, 90, 0); } else { rotationAngles = new Vector3(0, 0, 0); }
            GameObject go= ObjectPoolingMaster.Instance.SpawnFromPool("JengaPiece", ReturnTransformPosition(pieceCountCurrent,jengaTowerCenters[stackOrder],isRotate), Quaternion.Euler(rotationAngles));
            go.GetComponent<JengaPiece>().SetData(item);
            pieces.Add(go);
            pieceCountCurrent++;
            if (pieceCountCurrent % 3 == 0) {
                isRotate = !isRotate;
            }
        }
    }
    public Vector3 ReturnTransformPosition (int pieceCountCurrent,Vector3 centerPoint,bool isRotate) {
        Vector3 currentPiecePosition = new Vector3();
        float x = 0f;
        float y = 0f;
        float z = 0f;
        if (isRotate) {
            x = ((pieceCountCurrent % 3f)*1.33f)-1.25f;
            y = (pieceCountCurrent / 3) * 1.1f;
            z = 0f;
        } else {
            x = 0f;
            y = (pieceCountCurrent / 3)*1.1f;
            z = ((pieceCountCurrent % 3f) * 1.33f) - 1.25f;
        }

        currentPiecePosition = centerPoint + new Vector3(x, y, z);


        return currentPiecePosition;
    }
   
}
