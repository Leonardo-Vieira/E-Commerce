/* using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using E_Product.Models;
using Microsoft.Data.Sqlite;

namespace E_Product.Domain.Queries
{
    public class BrandQueries : IBrandQueries
    {
        private string _connectionString = string.Empty;
        public BrandQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
           using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT b.Id, b.Code, b.Name, b.Description

                    FROM Brands b"
                );
                return MapBrandsItems(result);
            }
        }

        public async Task<Brand> GetByIdAsync(Guid id)
        {
           using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT b.Id, b.Code, b.Name, b.Description

                    FROM Brands b

                    WHERE b.Id=@id"
                , new {id}
                );
                return MapBrandItems(result);
            }
        }

        private Brand MapBrandItems(dynamic result)
        {
            var brand = new Brand
            {
                Id = new Guid(result[0].Id),
                Code = result[0].Code,
                Name = result[0].Name,
                Description = result[0].Description,
            };

            return brand;
        }
        private IEnumerable<Brand> MapBrandsItems(dynamic result)
        {
            var brands = new List<Brand>();

            for (int i = 0; i < result.Count; i++)
            {
                brands.Add(new Brand 
                {
                    Id = new Guid(result[i].Id),
                    Code = result[i].Code,
                    Name = result[i].Name,
                    Description = result[i].Description,
                });
            }
            return brands;
        }
    
    }
} */