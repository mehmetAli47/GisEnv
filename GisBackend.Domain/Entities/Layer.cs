using GisBackend.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisBackend.Domain.Entities
{
    public class Layer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string GeometryType { get; private set; } = null!;
        public Status IsDeleted { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset? UpdatedAt { get; private set; }

        // EF'den bağımsız
        public string? GeometryWkt { get; private set; }

        private Layer() { } // EF için değil, domain koruması

        public Layer(
            Guid id,
            string name,
            string geometryType,
            string? geometryWkt
        )
        {
            Id = id;
            Name = name;
            GeometryType = geometryType;
            GeometryWkt = geometryWkt;
            IsDeleted = Status.Active;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Update(string name, string geometryType)
        {
            Name = name;
            GeometryType = geometryType;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public void SoftDelete()
        {
            IsDeleted = Status.Passive;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
