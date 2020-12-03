using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Bari.ConsoleApp.Domain.Models
{
    public class MQConnection
    {
        public MQConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
