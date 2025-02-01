using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeam
{
    public Relantioship GetRelantioship();
}

public class Team : MonoBehaviour, ITeam
{
    [SerializeField] private Relantioship _relantioship;
    [SerializeField] private ColorScriptableObject _materials;

    private TeamModel _teamModel;

    public Relantioship GetRelantioship() => _teamModel._relantioship;

    private void Awake()
    {
        SetColor();
        _teamModel = new TeamModel(_relantioship);
    }

    private void SetColor()
    {
        GetComponent<MeshRenderer>().material = _materials._materials[(int)_relantioship];
    }
}

public class TeamModel
{
    public readonly Relantioship _relantioship;

    public TeamModel(Relantioship relantioship)
    {
        _relantioship = relantioship;
    }
}

public enum Relantioship
{
    Player = 0,
    Enemy = 1,
}
