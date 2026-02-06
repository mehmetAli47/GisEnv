using Mapster;
using NetTopologySuite.IO;
using GisBackend.Infrastructure.Persistence.Entities;
using GisBackend.Domain.Entities;
using NetTopologySuite.Geometries;

namespace GisBackend.Infrastructure.Persistence.Mappers
{
    public static class MapsterConfig
    {
        private static readonly WKTReader _wktReader = new();
        private static readonly WKTWriter _wktWriter = new();

        public static void Configure()
        {
            // Global Settings
            var config = TypeAdapterConfig.GlobalSettings;
            config.Default.EnableNonPublicMembers(true);

            // NTS Geometry nesnelerini Mapster'ın kurcalamasını engelliyoruz, 
            // bunları olduğu gibi (reference) bırak diyoruz.
            config.ForType<Geometry, Geometry>().MapWith(src => src);
            
            // LayerEntity -> Layer
            config.ForType<LayerEntity, Layer>()
                .ConstructUsing(src => (Layer)Activator.CreateInstance(typeof(Layer), true)!)
                .Map(dest => dest.GeometryWkt, src => src.Geom != null ? _wktWriter.Write(src.Geom) : null);

            // Layer -> LayerEntity
            config.ForType<Layer, LayerEntity>()
                .Map(dest => dest.Geom, src => MapWktToGeometry(src.GeometryWkt));
        }

        private static Geometry? MapWktToGeometry(string? wkt)
        {
            if (string.IsNullOrEmpty(wkt)) return null;
            var geom = _wktReader.Read(wkt);
            geom.SRID = 4326;
            return geom;
        }
    }
}
