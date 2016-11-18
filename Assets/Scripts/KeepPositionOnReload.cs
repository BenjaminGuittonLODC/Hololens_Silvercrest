using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeepPositionOnReload : MonoBehaviour {

    public Transform model;

	// Use this for initialization
	void Start () {
        LoadPosition();
    }

    void Update()
    {
        if (Input.GetButtonDown("Reload"))
            SavePosition();
    }

    public void LoadPosition()
    {
        //Set the model pos to the last relative position to the cam if we reloaded the app
        if (PlayerPrefs.GetInt("IsReloaded") == 1)
        {
            model.transform.position = new Vector3(PlayerPrefs.GetFloat("ModelPosX"), PlayerPrefs.GetFloat("ModelPosY"), PlayerPrefs.GetFloat("ModelPosZ"));
            float scale = PlayerPrefs.GetFloat("Size");
            model.transform.localScale = Vector3.one * scale;
        }
        PlayerPrefs.SetInt("IsReloaded", 0);
    }
	
	public void SavePosition()
    {
        //Save the relative position of the model from the cam
        Vector3 pos = model.position - Camera.main.transform.position;
        pos = Utility.ChangeReference(pos, Camera.main.transform.forward);

        PlayerPrefs.SetInt("IsReloaded", 1);
        PlayerPrefs.SetFloat("ModelPosX", pos.x);
        PlayerPrefs.SetFloat("ModelPosY", pos.y);
        PlayerPrefs.SetFloat("ModelPosZ", pos.z);
        PlayerPrefs.SetFloat("Size", model.transform.localScale.x);

        //Reload the scene
        SceneManager.LoadScene(0);
    }
}
