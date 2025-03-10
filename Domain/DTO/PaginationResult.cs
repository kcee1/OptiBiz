using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class PaginationResult<TEntity>
    {
        public List<TEntity>? Items { get; set; }
        public int TotalPages { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
    }
}
