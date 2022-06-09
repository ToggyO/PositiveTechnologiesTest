using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using FileSearchEngine.Constants;
using FileSearchEngine.Engine;
using FileSearchEngine.Models;
using FileSearchEngine.Parser;
using FileSearchEngine.Printer;

namespace FileSearchEngine.Storage
{
    public class MemoryStorage
    {
        private readonly ConcurrentBag<User> _users = new ();

        private readonly ConcurrentBag<Soft> _products = new ();

        private List<User> _sortedUsers = new ();

        private List<Soft> _sortedProducts = new ();

        public async Task Fill(IList<SearchResult> results)
        {
            var tasks = results.Select(async r =>
            {
                if (r.Extension == AppConstants.FileExtensions.User)
                {
                    var user = await FileParser.ParseJson(r);
                    if (user is not null)
                        _users.Add(user);
                }
                
                if (r.Extension == AppConstants.FileExtensions.Product)
                {
                    var product = await FileParser.ParseXml(r);
                    if (product is not null)
                        _products.Add(product);
                }
            });
            
            await Task.WhenAll(tasks);
            
            await Task.WhenAll(
                Task.Run(() => _sortedUsers.AddRange(_users.OrderBy(k => k.Name))),
                Task.Run(() => _sortedProducts.AddRange(_products.OrderBy(k => k.Name))));
        }

        public void PrintResults()
        {
            if (_sortedUsers.Count > 0)
            {
                Utils.Log("Найденные пользователи:");
                ResultsPrinter.Print(_sortedUsers);
            }
            
            if (_sortedProducts.Count > 0)
            {
                Utils.Log("Найденные продукты:");
                ResultsPrinter.Print(_sortedProducts);
            }

            if (_users.Count == 0 && _products.Count == 0)
                Utils.Log("Данные не найдены. Попробуйте еще раз!");
        }

        public async Task Clear()
        {
            await Task.WhenAll(
                Task.Run(() => _users.Clear()),
                Task.Run(() => _products.Clear()),
                Task.Run(() => _sortedUsers.Clear()),
                Task.Run(() => _sortedProducts.Clear()));
        }
    }
}