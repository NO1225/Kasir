using System;
using Kasir.Application.Common.Interfaces;

namespace Kasir.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
