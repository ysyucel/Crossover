using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    UIManager uiManager;
    TestMyStackController testMyStackController;
    APIConnectionManager apiConnectionManager;
    const string urlTest = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    public List<StudentData> studentDatas; 
    // Start is called before the first frame update
    void Start() {
        apiConnectionManager = GameObject.FindObjectOfType<APIConnectionManager>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        testMyStackController = GameObject.FindObjectOfType<TestMyStackController>();
        studentDatas = new List<StudentData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClickTestMyStack () {
        uiManager.CloseMainMenu();
        uiManager.OpenTestMyStackMenu();
        testMyStackController.StartTestMyStack();
    }
    public void TakeConnectionRequest () {
        apiConnectionManager.MakeAPIRequest(urlTest, HandleApiCallback);
    }

    public void HandleApiCallback (StudentData[] dataList) {
        studentDatas.Clear();
        foreach (StudentData data in dataList) {
            studentDatas.Add(data);
        }
        testMyStackController.DataDownloaded();
    }
    public void ChangeCameraTarget (int index) {
        testMyStackController.NewCameraTarget(index);
    }
    public void StackReady () {
        uiManager.OpenTestMyStackMenu();
    }
    public void ActivatePhysics () {
        testMyStackController.ActivatePhysics();
    }


}
