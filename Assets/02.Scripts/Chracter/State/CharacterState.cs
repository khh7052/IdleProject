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
    public void ChangeState<T>() where T : CharacterState => character.StateMachine?.ChangeState<T>();
    public void SetAnimation(int animHash) => AnimationHandler?.SetTrigger(animHash);
    public void SetAnimation(int animHash, bool value) => AnimationHandler?.SetBool(animHash, value);

    public void MoveTo(Vector3 destination) => NavmeshController?.SetDestination(destination);


}
