using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RunUIController : MonoBehaviour {

    public Text scoreText;
    public int score;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        score++;
        scoreText.text = score.ToString();
	}
}
