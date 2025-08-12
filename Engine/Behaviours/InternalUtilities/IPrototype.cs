namespace IaraEngine;

public interface IPrototype
{
	IPrototype ShallowClone();

	IPrototype DeepClone();
}
