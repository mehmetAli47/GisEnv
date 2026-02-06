namespace GisBackend.Core.Domain.Entities
{
    public class Layer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string GeometryType { get; private set; }
        public string? GeometryWkt { get; private set; }

        private Layer() { }

        public Layer(Guid id, string name, string geometryType, string? geometryWkt)
        {
            Id = id;
            Name = name;
            GeometryType = geometryType;
            GeometryWkt = geometryWkt;
        }
    }
}
