using UnityEngine;
using System.Collections;

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