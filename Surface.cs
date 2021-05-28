using System;

namespace KG6
{
    struct Surface
	{
        public Func<Vector, Color> Diffuse;
        public Func<Vector, Color> Specular;
        public Func<Vector, double> Reflect;
        public double Roughness;

        public Surface(
            Func<Vector, Color> diffuse,
            Func<Vector, Color> specular,
            Func<Vector, double> reflect,
            double roughness)
        {
            Diffuse = diffuse;
            Specular = specular;
            Reflect = reflect;
            Roughness = roughness;
        }
    }
}