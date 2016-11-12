using UnityEngine;
using System.Collections;
/// <summary>
/// Player用
/// </summary>
public class Controller {

	virtual public void Enter(JobBase jb)
	{

	}
	virtual public void Excute(JobBase jb)
	{

	}
	virtual public void Exit(JobBase jb)
	{

	}
}
/// <summary>
/// Root用
/// </summary>
public class RootController{

	virtual public void Enter(PlayerRoot pr = null)
	{

	}
	virtual public void Excute(PlayerRoot pr = null)
	{

	}
	virtual public void Exit(PlayerRoot pr = null)
	{

	}
}
/// <summary>
/// エネミー用
/// </summary>
public class e_Controller
{

    virtual public void Enter(EnemyBase eb = null)
    {

    }
    virtual public void Excute(EnemyBase eb = null)
    {

    }
    virtual public void Exit(EnemyBase eb = null)
    {

    }
}