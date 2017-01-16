using System.Collections.Generic;

namespace PreStart.Models
{
    public class SaleForceResponse<T>
    {
        public ICollection<T> records;
    }
}
