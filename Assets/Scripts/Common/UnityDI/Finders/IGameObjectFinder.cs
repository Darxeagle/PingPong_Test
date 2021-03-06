﻿using UnityEngine;

namespace Assets.Common.Scripts.UnityDI.Finders
{
	/// <summary>
	/// Интерфейс класса, ищущего игровые объекты по пути
	/// </summary>
	public interface IGameObjectFinder
	{
		GameObject Find(string path);
	}
}
