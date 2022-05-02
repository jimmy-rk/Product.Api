using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using Product.Api.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Product.Api.Cqrs.Product
{
    public class GetProductGroupTypes
    {
        public class Query : IRequest<IEnumerable<Response>> { }

        public class Response : IRequest
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }
        
        public class QueryHandler : RequestHandler<Query, IEnumerable<Response>>
        {
            private readonly string _sqlConnectionString;

            public QueryHandler(IOptionsMonitor<ConnectionStrings> connectionStringAccessor) => _sqlConnectionString = connectionStringAccessor.CurrentValue.DefaultConnection;

            protected override IEnumerable<Response> Handle(Query message)
            {
                const string sql = @"
                                    SELECT pg.[Nk] AS [Value],
                                           pg.[Text] 
                                    FROM dbo.ProductGroup pg;";

                using var connection = new SqlConnection(_sqlConnectionString);
                return connection.Query<Response>(sql, message);
            }

        }
    }
}
