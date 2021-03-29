using System.Collections.Generic;
using GeneralLibrary.BaseCore;

namespace GeneralLibrary.DependencyInjection
{
	public interface IDependencyContainer
	{
		void Register<I, T>(LifeStyleType lifeStyle)
			where I : class
			where T : class, I;

		void Register<I, T>(LifeStyleType lifeStyle, string key, params KeyValuePair<string, object>[] arguments)
		where I : class
		where T : class, I;

		void Register<T>(T instance, LifeStyleType lifeStyle) where T : class;

		void Register<T>(T instance, string key, LifeStyleType lifeStyle) where T : class;

		T Resolve<T>() where T : class;
	}
}
