using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IISPI.Client.Helpers
{
    public class Paginacion<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public static int PageSize { get; set; }

        private readonly IConfiguration Configuration;

        public Paginacion(IConfiguration configuration)
        {
            Configuration = configuration;
            PageSize = Configuration.GetValue("PageSize", 10);
        }

        public Paginacion(List<T> items, int count, int pageIndex)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<Paginacion<T>> CreateAsync(IQueryable<T> source, int pageIndex)
        {
            int pageSize = PageSize;
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * PageSize).Take(PageSize).ToListAsync();

            return new Paginacion<T>(items, count, pageIndex);
        }
    }
}