using System;
using System.Collections;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Tutorial_12_Order.DTO;
using Tutorial_12_Order.Models;

namespace Tutorial_12_Order.Services
{
    public class OrderDbService : IOrderDbService
    {
        static Random rnd = new Random();
        private readonly OrderDbContext _context;

        public OrderDbService(OrderDbContext context)
        {
            _context = context;
        }

        // name or not
        public IEnumerable getOrders(string name)
        {
            if (name == null)
            {
                var res = from o in _context.Order
                    select new OrderResponse()
                    {
                        IdOrder = o.IdOrder,
                        DateAccepted = o.DateAccepted,
                        DateFinishied = o.DateFinished,
                        List = (from e in _context.Confectionery_order
                                join c in _context.Confectionery on e.IdConfectionary equals c.IdConfectionery
                                where e.IdOrder == o.IdOrder
                                select new Confectionery
                                {
                                    IdConfectionery = c.IdConfectionery,
                                    Name = c.Name,
                                    Type = c.Type,
                                    PricePerItem = c.PricePerItem
                                }
                            ).ToList()

                    };
                return res;

            }
            else
            {
                if (_context.Customer.Any(e => e.FirstName == name))
                {
                    var customer = _context.Customer.Single(e => e.FirstName == name);
                    var id = customer.IdClient;
                    var orderResponse = from o in _context.Order
                        where o.IdClient == customer.IdClient
                        select new OrderResponse()
                        {
                            IdOrder = o.IdOrder,
                            DateAccepted = o.DateAccepted,
                            DateFinishied = o.DateFinished,
                            List = (from c in _context.Confectionery_order
                                join conf in _context.Confectionery on c.IdConfectionary equals conf.IdConfectionery
                                where c.IdOrder == o.IdOrder
                                select new Confectionery()
                                {
                                    IdConfectionery = conf.IdConfectionery,
                                    Name = conf.Name,
                                    Type = conf.Type,
                                    PricePerItem = conf.PricePerItem
                                }).ToList()
                        };
                    return orderResponse;

                }
                else
                {
                    throw new Exception("Customer not found!");
                }
            }
        }

        public string PlaceOrder(int customerId, OrderRequest request)
        {
            if (_context.Customer.Any(e => e.IdClient == customerId))
            {
                if (_context.Confectionery.Any(e => request.Confectionery.Select(c => c.Name).Contains(e.Name)))
                {
                    _context.Database.BeginTransaction();
                    var IdOrder = _context.Order.Max(e => e.IdOrder) + 1;
                    var l = _context.Employee.Select(e => e.IdEmployee).ToList();
                    var order = new Order
                    {
                        IdOrder = IdOrder,
                        DateAccepted = request.DateAccepted,
                        DateFinished = DateTime.Now.AddMonths(1),
                        Notes = request.Notes,
                        IdClient = customerId,
                        IdEmployee = l[rnd.Next(l.Count)]
                    };
                    _context.Order.Add(order);
                    _context.SaveChanges();
                    
                    foreach (var c in request.Confectionery)
                    {
                        var confectioneryId = _context.Confectionery.First(e => e.Name == c.Name).IdConfectionery;
                        var confectionery_order = new Confectionery_Order()
                        {
                            IdConfectionary = confectioneryId,
                            IdOrder = order.IdOrder,
                            quantity = c.Quantity,
                            Notes = c.Notes
                        };
                        _context.Confectionery_order.Add(confectionery_order);
                        _context.SaveChanges();
                    }
                    _context.Database.CommitTransaction();

                    return "Added order and confectionery successfuly";
                } 
            }

            return "Didn't add";
        }
    }
}