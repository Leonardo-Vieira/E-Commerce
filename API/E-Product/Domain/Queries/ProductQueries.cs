/* using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using E_Product.Models;
using E_Product.Models.Dtos;
using Microsoft.Data.Sqlite;

namespace E_Product.Domain.Queries {
    public class ProductQueries : IProductQueries {
        private string _connectionString = string.Empty;
        public ProductQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<ProductToView>> GetAllAsync () {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT p.Id, p.Code, p.Name, p.Status, p.Description, p.Price, p.Stock,
                     b.Id as bId, b.Code as bCode, b.Name as bName, b.Description as bDescription, 
                     pt.Id as ptId, pt.Code as ptCode, pt.Name as ptName, pt.Description as ptDescription, 
                     prov.Id as provId, prov.Name as provName, prov.Description as provDescription, prov.Phone as provPhone, prov.PostalCode as provPostalCode, prov.Place as provPlace, prov.IdentificationNumber as provIdentificationNumber
                        FROM Products p

                        INNER JOIN Brands b on p.BrandId = b.Id
                        INNER JOIN Provider prov on p.ProviderId = prov.Id
                        INNER JOIN ProductTypes pt on p.ProductTypeId = pt.Id

                 "
                );
                return MapProductsItems(result);
            }
        }

        public async Task<ProductToView> GetByIdAsync (Guid id) 
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT p.Id, p.Code, p.Name, p.Status, p.Description, p.Price, p.Stock,
                     b.Id as bId, b.Code as bCode, b.Name as bName, b.Description as bDescription, 
                     pt.Id as ptId, pt.Code as ptCode, pt.Name as ptName, pt.Description as ptDescription, 
                     prov.Id as provId, prov.Name as provName, prov.Description as provDescription, prov.Phone as provPhone, prov.PostalCode as provPostalCode, prov.Place as provPlace, prov.IdentificationNumber as provIdentificationNumber
                        FROM Products p

                        INNER JOIN Brands b on p.BrandId = b.Id
                        INNER JOIN Provider prov on p.ProviderId = prov.Id
                        INNER JOIN ProductTypes pt on p.ProductTypeId = pt.Id

                        WHERE p.Id=@id"
                , new {id}
                );
                return MapProductItems(result);
            }

        }

        private ProductToView MapProductItems(dynamic result)
        {
            var product = new ProductToView
            {
                Id = new Guid(result[0].Id),
                Code = result[0].Code,
                Name = result[0].Name,
                Status = Convert.ToBoolean(result[0].Status),
                Description = Convert.ToDecimal(result[0].Description),
                Price = Convert.ToDecimal(result[0].Price),
                Stock = Convert.ToInt32(result[0].Stock),
                 Brand = new Brand {
                    Id = new Guid(result[0].bId),
                    Code = result[0].bCode,
                    Name = result[0].bName,
                    Description = result[0].bDescription,
                },
               ProductType = new ProductType {
                    Id = new Guid(result[0].ptId),
                    Code = result[0].ptCode,
                    Name = result[0].ptName,
                    Description = result[0].ptDescription,
                },
                Provider = new Provider {
                    Id = new Guid(result[0].provId),
                    Code = result[0].provCode,
                    Name = result[0].provName,
                    Description = result[0].provDescription,
                    Phone = result[0].provPhone,
                    PostalCode = result[0].provPostalCode,
                    Place = result[0].provPlace,
                    IdentificationNumber = result[0].provIdentificationNumber,
                }, 
            };

            return product;
        }
        private IEnumerable<ProductToView> MapProductsItems(dynamic result)
        {
            var products = new List<ProductToView>();

            for (int i = 0; i < result.Count; i++)
            {
                products.Add(new ProductToView 
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
                    ProductType = new ProductType {
                        Id = new Guid(result[i].ptId),
                        Code = result[i].ptCode,
                        Name = result[i].ptName,
                        Description = result[i].ptDescription,
                    },
                    Provider = new Provider {
                        Id = new Guid(result[i].provId),
                        Code = result[i].provCode,
                        Name = result[i].provName,
                        Description = result[i].provDescription,
                        Phone = result[i].provPhone,
                        PostalCode = result[i].provPostalCode,
                        Place = result[i].provPlace,
                        IdentificationNumber = result[i].provIdentificationNumber,
                    }, 
                });
            }

            return products;
        }
    }
} */