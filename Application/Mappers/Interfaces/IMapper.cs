namespace TicketToRide.Application.Mappers.Interfaces
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new();

        List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sources)
            where TDestination : new();
    }

    public abstract class EntityMapper : IMapper
    {
        public virtual TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            return MapInternal<TSource, TDestination>(source);
        }

        public virtual List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> sources)
            where TDestination : new()
        {
            return sources.Select(s => Map<TSource, TDestination>(s)).ToList();
        }

        protected abstract TDestination MapInternal<TSource, TDestination>(TSource source)
            where TDestination : new();
    }
}