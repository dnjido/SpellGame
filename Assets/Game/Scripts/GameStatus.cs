using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatus
{
    private readonly MonoBehaviour _monoBahaviour;
    private GameObject[] _units;

    public GameStatus(MonoBehaviour monoBahaviour) 
    {
        _monoBahaviour = monoBahaviour;
    }

    public void GetAllUnits()
    {
        _units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in _units)
        {
            unit.GetComponent<IHealthAdapter>().IHealthEvents.DeathEvent += CheckReset;
        }
    }

    public bool CheckTeamUnits()
    {
        _units = GameObject.FindGameObjectsWithTag("Unit");

        GameObject[] enemies = _units.Where(u => GetTeam(u) == Relantioship.Enemy).ToArray();
        GameObject[] players = _units.Where(u => GetTeam(u) == Relantioship.Player).ToArray();

        return players.Length <= 0 || enemies.Length <= 0;
    }

    public Relantioship GetTeam(GameObject unit)
    {
        return unit.GetComponent<ITeam>().GetRelantioship();
    }

    public void CheckReset() => _monoBahaviour.StartCoroutine(LoadScene(true));

    public void ResetGame() => _monoBahaviour.StartCoroutine(LoadScene(false));

    private IEnumerator LoadScene(bool check)
    {
        yield return new WaitForSeconds(.5f);
        if (!check || CheckTeamUnits()) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
