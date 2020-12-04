using System;

namespace Toll_Calculator_API.DbModels
{
    public partial class Log
    {
        public long Id { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
    }
}
