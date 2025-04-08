/* using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using E_Product.Models;
using Microsoft.Data.Sqlite;

namespace E_Product.Domain.Queries
{
    public class ProductTypeQueries : IProductTypeQueries
    {
        private string _connectionString = string.Empty;
        public ProductTypeQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT pt.Id, pt.Code, pt.Name, pt.Description

                    FROM ProductTypes pt"
                );
                return MapProductTypesItems(result);
            }
        }

        public async Task<ProductType> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT pt.Id, pt.Code, pt.Name, pt.Description

                    FROM ProductTypes pt

                    WHERE pt.Id=@id"
                , new {id}
                );
                return MapProductTypeItems(result);
            }
        }

        private ProductType MapProductTypeItems(dynamic result)
        {
            var productType = new ProductType
            {
                Id = new Guid(result[0].Id),
                Code = result[0].Code,
                Name = result[0].Name,
                Description = result[0].Description,
            };

            return productType;
        }
        private IEnumerable<ProductType> MapProductTypesItems(dynamic result)
        {
            var productTypes = new List<ProductType>();

            for (int i = 0; i < result.Count; i++)
            {
                productTypes.Add(new ProductType 
                {
                    Id = new Guid(result[i].Id),
                    Code = result[i].Code,
                    Name = result[i].Name,
                    Description = result[i].Description,
                });
            }
            return productTypes;
        }
    }
} */