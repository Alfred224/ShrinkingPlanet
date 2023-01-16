using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject gameOverUI;

	void Awake ()
	{
		instance = this;
	}

	public void EndGame ()
	{
		gameOverUI.SetActive(true);
		gameOverUI.transform.Find("FinalScore").GetComponent<Text>().text = "d = <i><b>" + Planet.Score.ToString("0.#") + "</b>m</i>";
	}

	public void Restart ()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
