namespace GeneralLibrary.DependencyInjection
{
	public enum LifeStyleType
	{
		Singleton,
		Transient,
		PerWebRequest,
		PerThread,
		Scoped
	}
}
