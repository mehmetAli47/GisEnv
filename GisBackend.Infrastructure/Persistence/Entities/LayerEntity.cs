using GisBackend.Domain.Entities.Enums;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GisBackend.Infrastructure.Persistence.Entities
{
    [Table("layer")]
    public class LayerEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("geometry_type")]
        public string GeometryType { get; set; } = null!;

        [Column("is_deleted")]
        public Status IsDeleted { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [Column("geom")]
        public Geometry? Geom { get; set; }
    }
}
