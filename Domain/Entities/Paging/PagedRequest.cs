using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Paging
{
    public class PagedRequest
    {
        /// <summary>
        /// page: start from 0
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// size of a page. if pagesize = -1, return all records
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// chỉ lấy những record đang active
        /// </summary>
        public bool? IsActive { get; set; } = true;
        /// <summary>
        /// sql order by clause
        /// </summary>
        public string? Sort { get; set; } = "created_at DESC";
        /// <summary>
        /// use stored procedure to get data
        /// </summary>
        public bool? IsUseProc { get; set; } = false;
    }
}
