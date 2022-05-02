using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using Product.Api.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Product.Api.Cqrs.Product
{
    public class GetByGuid
    {
        public class Query : IRequest<Response>
        {
            public Guid Guid { get; set; }
        }

        public class Response : IRequest
        {
            public Guid Guid { get; set; }
            public string Name { get; set; }
            public string ProductGroupNk { get; set; }
            public decimal Price { get; set; }
            public string Comments { get; set; }
        }

        public class QueryHandler : RequestHandler<Query, Response>
        {
            private readonly string _sqlConnectionString;

            public QueryHandler(IOptionsMonitor<ConnectionStrings> connectionStringAccessor) => _sqlConnectionString = connectionStringAccessor.CurrentValue.DefaultConnection;


            protected override Response Handle(Query message)
            {
                const string sql = @"
                                    SELECT 
                                        p.Guid,
                                        p.Name,
                                        p.ProductGroupNk,
                                        p.Price,
                                        p.Comments
                                    FROM [dbo].[Product] p
                                    WHERE p.Guid = @Guid";

                using var connection = new SqlConnection(_sqlConnectionString);
                return connection.QuerySingleOrDefault<Response>(sql, message);

            }
        }
    }
}
