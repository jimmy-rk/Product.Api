using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using Product.Api.Configuration;
using System;
using System.Data.SqlClient;

namespace Product.Api.Cqrs.Product
{
    public class Update
    {
        public class Command : IRequest<Response>
        {
            public Guid Guid { get; set; }  
            public string Name { get; set; }    
            public string ProductGroupNk { get; set; }  
            public decimal Price { get; set; }
            public string Comments { get; set; }
        }

        public class Response
        {
            public int Id { get; set; }
            public Guid Guid { get; set; }
            public int UpdateCount { get; set; }
        }

        public class Handler : RequestHandler<Command, Response>
        {
            private readonly string _sqlConnectionString;
            public Handler(IOptionsMonitor<ConnectionStrings> connectionStringAccessor)
            {
                _sqlConnectionString = connectionStringAccessor.CurrentValue.DefaultConnection;

            }
            protected override Response Handle(Command message)
            {
                const string sql = @"
                                    UPDATE dbo.Product
                                    SET
                                        Name = @Name,
                                        ProductGroupNk = @ProductGroupNk,
                                        Price = @price,
                                        Comments = @Comments
                                    WHERE Guid = @Guid

                                    SELECT 
                                        Id,
                                        Guid,
                                        @@ROWCOUNT AS UPDATECOUNT
                                    FROM dbo.Product
                                    WHERE Guid = @Guid";

                using var connection = new SqlConnection(_sqlConnectionString);
                return connection.QueryFirstOrDefault<Response>(sql, message);
            }
        }
    }
}
