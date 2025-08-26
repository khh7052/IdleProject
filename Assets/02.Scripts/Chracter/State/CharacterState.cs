using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : IState
{
    protected readonly CharacterAI character;

    public NavmeshController NavmeshController => character.NavmeshController;
    public AnimationHandler AnimationHandler => character.AnimationHandler;

    public CharacterState(CharacterAI character)
        => this.character = character;

    public virtual void Enter() { }

    public virtual void Execute() { }

    public virtual void Exit() { }
}
