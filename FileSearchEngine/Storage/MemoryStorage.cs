using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        // private readonly ConcurrentBag<string> _users = new ();
        //
        // private readonly ConcurrentBag<string> _products = new ();

        private ConcurrentBag<User> _users = new ();

        private ConcurrentBag<Soft> _products = new ();
        
        private readonly FileParser _parser;

        public MemoryStorage(FileParser parser)
        {
            _parser = parser;
        }
        
        public async Task Fill(List<SearchResult> results)
        {
            var tasks = results.Select(async r =>
            {
                if (r.Extension == AppConstants.FileExtensions.User)
                {
                    var user = await _parser.ParseJson(r);
                    if (user is not null)
                        _users.Add(user);
                        // _users.Add(user.ToString());
                }
                
                if (r.Extension == AppConstants.FileExtensions.Product)
                {
                    var product = await _parser.ParseXml(r);
                    if (product is not null)
                        _products.Add(product);
                        // _products.Add(product.ToString());
                }
            });

            await Task.WhenAll(tasks);
            // TODO: implement sorting
            // _users = new ConcurrentBag<User>(_users.OrderBy(k => k.Name));
            // _products = new ConcurrentBag<Soft>(_products.OrderBy(k => k.Name));
            // await Task.WhenAll(
            //     new Task(() => _users = new ConcurrentBag<User>(_users.OrderBy(k => k.Name))),
            //     new Task(() => _products = new ConcurrentBag<Soft>(_products.OrderBy(k => k.Name))));
        }

        public void PrintResults()
        {
            if (_users.Count > 0)
            {
                Utils.Print("Найденные пользователи:");
                ResultsPrinter.Print(_users);
            }
            
            if (_products.Count > 0)
            {
                Utils.Print("Найденные продукты:");
                ResultsPrinter.Print(_products);
            }

            if (_users.Count == 0 && _products.Count == 0)
                Utils.Print("Данные не найдены. Попробуйте еще раз!");
        }

        public async Task Clear()
        {
            await Task.WhenAll(
                Task.Run(() => _users.Clear()),
                Task.Run(() => _products.Clear()));
        }
    }
}