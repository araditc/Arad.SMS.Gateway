using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace GeneralLibrary.DependencyInjection
{
	public class WindsorIocContainer : IDependencyContainer
	{
		private static WindsorIocContainer iocContainer;
		private static readonly WindsorContainer container = new WindsorContainer();

		public static IDependencyContainer Instance
		{
			get
			{
				if (iocContainer == null)
				{
					iocContainer = new WindsorIocContainer();

					container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
					iocContainer.Register<IDependencyContainer>(iocContainer, LifeStyleType.Singleton);
				}

				return iocContainer.Resolve<IDependencyContainer>();
			}
		}

		public void Register<I, T>(LifeStyleType lifeStyle)
			where I : class
			where T : class, I
		{
			Register<I, T>(lifeStyle, string.Empty);
		}

		public void Register<I, T>(LifeStyleType lifeStyle, string key, params KeyValuePair<string, object>[] arguments)
	where I : class
	where T : class, I
		{
			var registration = Component.For<I>().ImplementedBy<T>();

			if (arguments != null)
			{
				foreach (var arg in arguments)
					registration.DynamicParameters((kernel, parameters) => parameters[arg.Key] = arg.Value);
			}

			registration = SetLifeStyle(lifeStyle, registration);

			if (!string.IsNullOrEmpty(key))
			{
				key = GetName<I>(key);
				registration.Named(key);
			}

			container.Register(registration);
		}
		public void Register<T>(T instance, LifeStyleType lifeStyle) where T : class
		{
			Register(instance, string.Empty, lifeStyle);
		}

		public void Register<T>(T instance, string key, LifeStyleType lifeStyle) where T : class
		{
			var registration = Component.For<T>().Instance(instance);

			registration = SetLifeStyle(lifeStyle, registration);

			if (!string.IsNullOrEmpty(key))
			{
				key = GetName<T>(key);
				registration.Named(key);
			}

			container.Register(registration);
		}

		public T Resolve<T>() where T : class
		{
			try
			{
				return container.Resolve<T>();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		private static string GetName<T>(string key) where T : class
		{
			return GetName(key, typeof(T));
		}

		private static string GetName(string key, Type type)
		{
			return type.FullName + "_" + key;
		}

		private static ComponentRegistration<T> SetLifeStyle<T>(LifeStyleType lifeStyle, ComponentRegistration<T> registration) where T : class
		{
			if (lifeStyle == LifeStyleType.Singleton)
				return registration.LifestyleSingleton();
			else if (lifeStyle == LifeStyleType.Transient)
				return registration.LifestyleTransient();
			else if (lifeStyle == LifeStyleType.PerWebRequest)
				return registration.LifestylePerWebRequest();
			else if (lifeStyle == LifeStyleType.PerThread)
				return registration.LifestylePerThread();
			else if (lifeStyle == LifeStyleType.Scoped)
				return registration.LifestyleScoped();

			return registration;
		}
	}
}
