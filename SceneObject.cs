namespace KG6
{
	abstract class SceneObject
	{
		public Surface Surface;

		public abstract ISect Intersect(Ray ray);

		public abstract Vector Normal(Vector pos);

		public SceneObject(Surface surface) => Surface = surface;
	}
}