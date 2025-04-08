/* using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using e_order.Domain.Models;
using e_order.Domain.ViewModels;
using E_Order.Domain.Models.Dto;
using Microsoft.Data.Sqlite;

namespace E_Order.Domain.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private string _connectionString = string.Empty;
        public OrderQueries (string constr) {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));;
        }
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync()
        {
            using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<dynamic>(
                        @"SELECT o.Id, o.DateOrder, o.State, 
                        c.Id as cId, c.Username as cUsername, c.Email as cEmail, c.Name as cName, c.IdentificationNumber as cIdentificationNumber, c.PostalCode as cPostalCode, c.Place as cPlace, c.Address as cAddress, c.IsCollaborator as cIsCollaborator

                        FROM Orders o

                        INNER JOIN Clients c on o.ClientId = c.Id
                        
                     "
                    
                    );
                    return await MapOrdersItems(result);
                }
        }

        public async Task<IEnumerable<OrderToClientDto>> GetByClientId(Guid clientId)
        {
              using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<dynamic>(
                        @"SELECT o.Id, o.DateOrder, o.State

                        FROM Orders o

                        WHERE o.ClientId=@clientId"
                    ,new {clientId}                    
                    );
                    return await MapOrdersToClientItems(result);
                }
        }

        public async Task<OrderViewModel> GetByIdAsync(Guid id)
        {
            using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<dynamic>(
                        @"SELECT o.Id, o.DateOrder, o.State, 
                        c.Id as cId, c.Username as cUsername, c.Email as cEmail, c.Name as cName, c.IdentificationNumber as cIdentificationNumber, c.PostalCode as cPostalCode, c.Place as cPlace, c.Address as cAddress, c.IsCollaborator as cIsCollaborator

                        FROM Orders o

                        INNER JOIN Clients c on o.ClientId = c.Id
                        
                        WHERE o.Id=@id"
                    ,new {id}
                    );
                    return await MapOrderItems(result);
                }
        }
        private async Task<List<OrderItemDto>> GetOrderItems (Guid orderId) 
        {
             using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();
                    var result = await connection.QueryAsync<dynamic>(
                        @"SELECT oi.Id as oiId, oi.Quantity as oiQuantity,
                        p.Id as pId, p.Code as pCode, p.Name as pName, p.Status as pStatus, p.Description as pDescription, p.Price as pPrice, p.Stock as pStock,
                        b.Id as bId, b.Code as bCode, b.Name as bName, b.Description as bDescription

                        FROM OrderItems oi

                        INNER JOIN Products p on oi.ProductId = p.Id
                        INNER JOIN Brands b on p.BrandId = b.Id
                        
                        WHERE oi.OrderId=@orderId"
                    ,new {orderId}
                    );

                    var orderList= new List<OrderItemDto>();   

                     foreach (var item in result)
                        {
                            orderList.Add(new OrderItemDto {
                            Product = new ProductViewModel {
                                    Id = new Guid(item.pId),
                                    Code = item.pCode,
                                    Name = item.pName,
                                    Status = Convert.ToBoolean(item.pStatus),
                                    Description = item.pDescription,
                                    Price = Convert.ToDecimal(item.pPrice),
                                    Stock = Convert.ToInt32(item.pStock),
                                    Brand = new Brand {
                                        Id = new Guid(item.bId),
                                        Code = item.bCode,
                                        Name = item.bName,
                                        Description = item.bDescription,
                                    }
                                },
                                Quantity = Convert.ToInt32(item.oiQuantity)   
                            });
                        } 
                        return  orderList;
                }
         

        } 
         private async Task<OrderViewModel> MapOrderItems(dynamic result)
        {
            var order = new OrderViewModel
            {
                Id = new Guid(result[0].Id),
                Client = new Client {
                    Id = new Guid(result[0].cId),
                    Username = result[0].cUsername,
                    Email = result[0].cEmail,
                    Name = result[0].cName,
                    IdentificationNumber = result[0].cIdentificationNumber,
                    PostalCode = result[0].cPostalCode,
                    Place = result[0].cPlace,
                    Address = result[0].cAddress,
                    IsCollaborator = Convert.ToBoolean(result[0].cIsCollaborator),
                },
                OrderItems = await GetOrderItems(new Guid(result[0].Id)),
                DateOrder = Convert.ToDateTime(result[0].DateOrder),
                State = Convert.ToBoolean(result[0].State),
            };

            return order;
        }
        private async Task<IEnumerable<OrderViewModel>> MapOrdersItems(dynamic result)
        {
            var orders = new List<OrderViewModel>();

            for (int i = 0; i < result.Count; i++)
            {
                orders.Add(new OrderViewModel
                {
                    Id = new Guid(result[i].Id),
                    Client = new Client {
                        Id = new Guid(result[i].cId),
                        Username = result[i].cUsername,
                        Email = result[i].cEmail,
                        Name = result[i].cName,
                        IdentificationNumber = result[i].cIdentificationNumber,
                        PostalCode = result[i].cPostalCode,
                        Place = result[i].cPlace,
                        Address = result[i].cAddress,
                        IsCollaborator = Convert.ToBoolean(result[i].cIsCollaborator),
                    },
                    DateOrder = Convert.ToDateTime(result[i].DateOrder),
                    State = Convert.ToBoolean(result[i].State),
                });

                foreach (var item in orders)
                {
                    item.OrderItems = await GetOrderItems(item.Id);
                }

            }

            return orders;
         }
        private async Task<IEnumerable<OrderToClientDto>> MapOrdersToClientItems(dynamic result)
        {
            var orders = new List<OrderToClientDto>();

            for (int i = 0; i < result.Count; i++)
            {
                orders.Add(new OrderToClientDto
                {
                    Id = new Guid(result[i].Id),
                    DateOrder = Convert.ToDateTime(result[i].DateOrder),
                    State = Convert.ToBoolean(result[i].State),
                });

                foreach (var item in orders)
                {
                    item.OrderItems = await GetOrderItems(item.Id);
                }

            }

            return orders;
         }
    }
}

 */