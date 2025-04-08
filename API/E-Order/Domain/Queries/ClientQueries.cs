/* using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using e_order.Domain.Models;
using Microsoft.Data.Sqlite;

namespace E_Order.Domain.Queries
{
    public class ClientQueries : IClientQueries
    {
        private string _connectionString = string.Empty;
        public ClientQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
           using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT c.Id, c.Username, c.Email, c.Name, c.IdentificationNumber, c.PostalCode, c.Place, c.Address, c.IsCollaborator

                    FROM Clients c
                 "
                );
                return MapBrandsItems(result);
            }
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT c.Id, c.Username, c.Email, c.Name, c.IdentificationNumber, c.PostalCode, c.Place, c.Address, c.IsCollaborator

                    FROM Clients c"
                , new {id}
                );
                return MapBrandItems(result);
            }
        }

        private Client MapBrandItems(dynamic result)
        {
            var client = new Client
            {
                Id = new Guid(result[0].Id),
                Username = result[0].Username,
                Email = result[0].Email,
                Name = result[0].Name,
                IdentificationNumber = result[0].IdentificationNumber,
                PostalCode = result[0].PostalCode,
                Place = result[0].Place,
                Address = result[0].Address,
                IsCollaborator = Convert.ToBoolean(result[0].IsCollaborator),
            };

            return client;
        }
        private IEnumerable<Client> MapBrandsItems(dynamic result)
        {
            var clients = new List<Client>();

            for (int i = 0; i < result.Count; i++)
            {
                clients.Add(new Client 
                {
                    Id = new Guid(result[i].Id),
                    Username = result[i].Username,
                    Email = result[i].Email,
                    Name = result[i].Name,
                    IdentificationNumber = result[i].IdentificationNumber,
                    PostalCode = result[i].PostalCode,
                    Place = result[i].Place,
                    Address = result[i].Address,
                    IsCollaborator = Convert.ToBoolean(result[i].IsCollaborator),
                });
            }
            return clients;
        }
    }
} */