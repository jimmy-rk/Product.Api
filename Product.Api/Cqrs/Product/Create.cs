using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using Product.Api.Configuration;
using System;
using System.Data.SqlClient;

namespace Product.Api.Cqrs.Product
{
    public class Create
    {
        public class Command : IRequest<Response>
        {
            public string Name { get; set; }
            public string ProductGroupNk { get; set; }  
            public decimal Price { get; set; }
            public string Comments { get; set; }

        }

        public class Response : IRequest
        {
            public int Id { get; set; }
            public Guid Guid { get; set; }
        }

        public class Handler : RequestHandler<Command, Response>
        {
            private readonly string _sqlConnectionString;

            public Handler(IOptionsMonitor<ConnectionStrings> connectionStringAccessor) => _sqlConnectionString = connectionStringAccessor.CurrentValue.DefaultConnection;

            
            protected override Response Handle(Command message)
            {
                const string sql = @"INSERT INTO dbo.Product
                                    (
                                        Name,
                                        ProductGroupNk,
                                        Price,
                                        Comments
                                    )
                                    SELECT
                                        @Name,
                                        @ProductGroupNk,
                                        @Price,
                                        @Comments

                                    SELECT @@IDENTITY AS Id, Guid FROM dbo.Product WHERE Id = @@IDENTITY";
                using var connection = new SqlConnection(_sqlConnectionString);
                return connection.QuerySingleOrDefault<Response>(sql, message);
            }
        }

    }
}
