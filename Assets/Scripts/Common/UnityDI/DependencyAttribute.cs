using System;

namespace Assets.Common.Scripts.UnityDI
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class Dependency : Attribute
	{
		public Dependency() {}

		public Dependency(string name)
		{
			Name = name;
		}

		public string Name { get; private set; }
	}
}
