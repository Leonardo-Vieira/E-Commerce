/* using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using e_order.Domain.Models;
using Microsoft.Data.Sqlite;

namespace E_Product.Domain.Queries {
    public class ProductQueries : IProductQueries {
        private string _connectionString = string.Empty;
        public ProductQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<ProductViewModel>> GetAllAsync () {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT p.Id, p.Code, p.Name, p.Status, p.Description, p.Price, p.Stock,
                     b.Id as bId, b.Code as bCode, b.Name as bName, b.Description as bDescription

                        FROM Products p

                        INNER JOIN Brands b on p.BrandId = b.Id

                 "
                );
                return MapProductsItems(result);
            }
        }

        public async Task<ProductViewModel> GetByIdAsync (Guid id) 
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT p.Id, p.Code, p.Name, p.Status, p.Description, p.Price, p.Stock,
                     b.Id as bId, b.Code as bCode, b.Name as bName, b.Description as bDescription
                     
                        FROM Products p

                        INNER JOIN Brands b on p.BrandId = b.Id

                        WHERE p.Id=@id"
                , new {id}
                );
                return MapProductItems(result);
            }

        }

        private ProductViewModel MapProductItems(dynamic result)
        {
            var product = new ProductViewModel
            {
                Id = new Guid(result[0].Id),
                Code = result[0].Code,
                Name = result[0].Name,
                Status = Convert.ToBoolean(result[0].Status),
                Description = result[0].Description,
                Price = Convert.ToDecimal(result[0].Price),
                Stock = Convert.ToInt32(result[0].Stock),
                 Brand = new Brand {
                    Id = new Guid(result[0].bId),
                    Code = result[0].bCode,
                    Name = result[0].bName,
                    Description = result[0].bDescription,
                },
            };

            return product;
        }
        private IEnumerable<ProductViewModel> MapProductsItems(dynamic result)
        {
            var products = new List<ProductViewModel>();

            for (int i = 0; i < result.Count; i++)
            {
                products.Add(new ProductViewModel 
                {
                    Id = new Guid(result[i].Id),
                    Code = result[i].Code,
                    Name = result[i].Name,
                    Status = Convert.ToBoolean(result[i].Status),
                    Description = result[i].Description,
                    Price = Convert.ToDecimal(result[i].Price),
                    Stock = Convert.ToInt32(result[i].Stock),
                    Brand = new Brand {
                        Id = new Guid(result[i].bId),
                        Code = result[i].bCode,
                        Name = result[i].bName,
                        Description = result[i].bDescription,
                    },
                });
            }

            return products;
        }
    }
} */