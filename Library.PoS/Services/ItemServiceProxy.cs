using CLI.PoS.Model;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Library.PoS.Services
{
    public class ItemServiceProxy
    {
        private List<Item> items;

        public List<Item> Items
        {
            get
            {
                return items;
            }

            set
            {
                if (items != value)
                {
                    items = value;
                }
            }
        }
        private static ItemServiceProxy? instance;
        private static object instanceLock = new object();

        public Item? GetById(int id)
        {
            if(id == 0)
            {
                return null;
            }

            return Items.FirstOrDefault(i => i.Id == id);
        }
        public static ItemServiceProxy Current
        {
            get
            {
                lock (instanceLock) {
                    if (instance == null)
                    {
                        instance = new ItemServiceProxy();
                    }
                }
                return instance; 
            }
        }
        private ItemServiceProxy() {
            //items = new List<Item> { 
            //    new Item{ Id = 1, Name = "Something 1", Description="This is a test description", Price = 1 }
            //    , new Item{ Id = 2, Name = "Something 2", Price = 2 }
            //    , new Item{ Id = 3, Name = "Something 3", Price = 3 } 
            //};

            var stringFromAPI = new WebRequestHandler().Get("/MenuItem").Result;
            items = JsonConvert.DeserializeObject<List<Item>>(stringFromAPI) ?? new List<Item>();
        }

        public Item? AddOrUpdate(Item? item)
        {
            if (item == null)
            {
                return null;
            }

            var stringFromAPI = new WebRequestHandler().Post("/MenuItem", item).Result;
            var itemFromAPI = JsonConvert.DeserializeObject<Item>(stringFromAPI);

            var existingItem = items.FirstOrDefault(i => i.Id == (itemFromAPI?.Id ?? 0));

            if (existingItem != null && itemFromAPI != null)
            {
                //update the item
                var index = items.IndexOf(existingItem);
                items.RemoveAt(index);
                items.Insert(index, itemFromAPI);
            } else if(itemFromAPI != null) {
                //add the item
                items.Add(itemFromAPI);
            }

                return itemFromAPI ?? item;
        }

        public Item? Delete(Item? item)
        {
            if (item == null)
            {
                return null;
            }

            var stringResponse = new WebRequestHandler().Delete($"/MenuItem/{item.Id}").Result;
            items.Remove(item);

            return item;
        }


    }
}
