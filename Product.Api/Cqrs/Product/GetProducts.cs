using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using Product.Api.Configuration;
using Product.Api.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Product.Api.Cqrs.Product
{
    public class GetProducts
    {
        public class Query : IRequest<IEnumerable<Response>> { }

        public class Response : IRequest
        {
            public Guid Guid { get; set; }
            public string Name { get; set; }
            public string ProductGroupNk { get; set; }
            public decimal Price { get; set; }
            public string Comments { get; set; }

        }
        
        public class QueryHandler : RequestHandler<Query, IEnumerable<Response>>
        {
            private readonly string _sqlConnectionString;

            public QueryHandler(IOptionsMonitor<ConnectionStrings> connectionStringAccessor) => _sqlConnectionString = connectionStringAccessor.CurrentValue.DefaultConnection;

            protected override IEnumerable<Response> Handle(Query message)
            {
                const string sql = @"
                                    SELECT p.[Guid],
                                           p.[Name],
                                           p.[ProductGroupNk],
                                           p.[Price],
                                           p.[comments] 
                                    FROM dbo.Product p;";

                using var connection = new SqlConnection(_sqlConnectionString);
                return connection.Query<Response>(sql, message);
            }

        }
    }
}
