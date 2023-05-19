using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JengaPiece : MonoBehaviour
{
    [SerializeField] StudentData studentData;
    [SerializeField] TMP_Text labelText;
    [SerializeField] TMP_Text gradeText;
    [SerializeField] TMP_Text clusterText;
    [SerializeField] TMP_Text standardIdText;
    public GameObject glassObject;
    [SerializeField] GameObject woodObject;
    [SerializeField] GameObject rockObject;
    // Start is called before the first frame update
    void Start()
    {
        //TextMesh textObject = labelText.GetComponent<TextMesh>();
       // textObject.text = "test";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(StudentData studentDataTemp) {
        studentData = studentDataTemp;
        if (studentData.mastery == 0) {
            //TextMesh textObject = labelText.GetComponent<TextMesh>();
            labelText.text = "";
            glassObject.SetActive(true);
        } else if (studentData.mastery == 1) {
            //TextMesh textObject = labelText.GetComponent<TextMesh>();
            labelText.text = "LEARNED";
            woodObject.SetActive(true);
        } else if (studentData.mastery == 2) {
            // TextMesh textObject = labelText.GetComponent<TextMesh>();
            labelText.text = "MASTERED";
            rockObject.SetActive(true);
        }
        gradeText.text = studentData.domain;
        clusterText.text = studentData.cluster;
        standardIdText.text = studentData.standardid;
    }
    public void OpenAdditionalInfo () {
        gradeText.gameObject.SetActive(true);
        clusterText.gameObject.SetActive(true);
        standardIdText.gameObject.SetActive(true);
        StartCoroutine(CloseText());
    }
    private IEnumerator CloseText () {
        yield return new WaitForSeconds(5);
        gradeText.gameObject.SetActive(false);
        clusterText.gameObject.SetActive(false);
        standardIdText.gameObject.SetActive(false);
    }
}
