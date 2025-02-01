using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetSpellAction
{
    public void SetSpell(SpellStatus spell, Relantioship team);
    public IPlayerAction IPlayerAction { get; set; }
}

public class ContainerPlayerAction : MonoBehaviour, ISetSpellAction
{
    private PlayerAction _action;
    public IPlayerAction IPlayerAction { get; set; }
    // Start is called before the first frame update
    void Awake()
    {
        _action = new PlayerAction();
        IPlayerAction = _action;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) _action.ApplySpell();
        if (Input.GetMouseButtonDown(1)) _action.ClearSpell();
    }

    public void SetSpell(SpellStatus spell, Relantioship team)
    {
        _action.SetSpell(spell, team);
    }
}
