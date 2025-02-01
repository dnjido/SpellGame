using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus
{
    private readonly MonoBehaviour _monoBahaviour;
    private GameObject[] units;

    public GameStatus(MonoBehaviour monoBahaviour) 
    {
        _monoBahaviour = monoBahaviour;
    }

    public void GetAllUnits()
    {
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            unit.GetComponent<IHealthAdapter>().IHealthEvents.DeathEvent += ResetGame;
        }
    }

    public void ResetGame() => _monoBahaviour.StartCoroutine(LoadScene());

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
