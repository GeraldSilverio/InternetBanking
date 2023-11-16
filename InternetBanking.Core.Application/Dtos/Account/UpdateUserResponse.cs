using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Dtos.Account
{
    public class UpdateUserResponse
    {
        public string? IdUser { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
