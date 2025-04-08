/* using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using E_Product.Models;
using Microsoft.Data.Sqlite;

namespace E_Product.Domain.Queries
{
    public class ProviderQueries : IProviderQueries
    {

        private string _connectionString = string.Empty;
        public ProviderQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
           using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT prov.Id, prov.Code, prov.Name, prov.Description, prov.Phone, prov.PostalCode, prov.Place, prov.IdentificationNumber

                    FROM Provider prov"
                );
                return MapProvidersItems(result);
            }
        }

        public async Task<Provider> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                     @"SELECT prov.Id, prov.Code, prov.Name, prov.Description, prov.Phone, prov.PostalCode, prov.Place, prov.IdentificationNumber

                    FROM Provider prov
                    WHERE prov.Id=@id"
                , new {id}
                );
                return MapProviderItems(result);
            }
        }

         private Provider MapProviderItems(dynamic result)
        {
            var provider = new Provider
            {
                Id = new Guid(result[0].Id),
                Code = result[0].Code,
                Name = result[0].Name,
                Description = result[0].Description,
                Phone = result[0].Phone,
                PostalCode = result[0].PostalCode,
                Place = result[0].Place,
                IdentificationNumber = result[0].IdentificationNumber,
            };

            return provider;
        }
        private IEnumerable<Provider> MapProvidersItems(dynamic result)
        {
            var providers = new List<Provider>();

            for (int i = 0; i < result.Count; i++)
            {
                providers.Add(new Provider 
                {
                    Id = new Guid(result[i].Id),
                    Code = result[i].Code,
                    Name = result[i].Name,
                    Description = result[i].Description,
                    Phone = result[i].Phone,
                    PostalCode = result[i].PostalCode,
                    Place = result[i].Place,
                    IdentificationNumber = result[i].IdentificationNumber,
                });
            }
            return providers;
        }
    }
} */