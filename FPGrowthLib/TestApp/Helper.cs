using FPGrowthLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
   public class Helper
    {
       public static void PrintDataItem(List<DataItem> datas)
        {
            Console.WriteLine("Data Setelah Di Transformasi");
            foreach (var item in datas)
            {
                Console.WriteLine($"{item.TID} - {GetStringItems(item.Items)}");
            }


            Console.WriteLine("");
        }

        public static object GetStringItems(List<string> items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append(item).Append(", ");
            }
            return sb.ToString();
        }



        public static void PrintFrequensi(List<FekuensiItem> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Name} |  {item.Count} | {item.Suport}");
            }

        }

        public static List<FekuensiItem> FrekuensiItem(List<DataItem> datas)
        {

            List<FekuensiItem> items = new List<FekuensiItem>();
            foreach (var item in datas)
            {
                foreach (var data in item.Items)
                {
                    var result = items.Where(x => x.Name == data).FirstOrDefault();
                    if (result == null)
                    {
                        items.Add(new FekuensiItem { Name = data, Count = 1 });
                    }
                    else
                        result.Count++;
                }
            }

            return items;
        }



      public  static void PrintDataTree(List<StateItem> datas)
        {
            foreach (var item in datas)
            {
                Console.WriteLine($"{item.ParentName}-{item.Name} - {item.Count}");
            }


            Console.WriteLine("");
        }
       public static Tuple<List<StateItem>, List<Tuple<int, int>>> PrintDataItemSort(List<DataItem> datas)
        {
            List<StateItem> StateItems = new List<StateItem>();
            var edges = new List<Tuple<int, int>>();
            int id = 1;
            foreach (var item in datas)
            {
                Console.WriteLine($"{item.TID} - {Helper.GetStringItems(item.SortData)}");
                var index = 0;
                StateItem parent = null;
                foreach (var d in item.SortData)
                {
                    StateItem x = null;
                    if (parent != null)
                    {
                        x = StateItems.Where(x => x.Index == index && x.Name == d && x.ParenId == parent.Id).FirstOrDefault();
                    }
                    else
                    {
                        x = StateItems.Where(x => x.Index == index && x.Name == d).FirstOrDefault();
                    }

                    if (x != null)
                    {
                        x.Count++;
                        parent = x;
                    }
                    else
                    {
                        StateItem y = null;
                        if (parent != null)
                        {
                            y = new StateItem { Index = index, Name = d, Count = 1, Id = id, ParenId = parent.Id };
                            edges.Add(Tuple.Create(parent.Id, y.Id));
                            y.ParentIndex = parent.Index;
                            y.ParentName = parent.Name;
                        }
                        else
                        {
                            y = new StateItem { Index = index, Name = d, Count = 1, Id = id };
                            edges.Add(Tuple.Create(0, y.Id));
                        }

                        parent = y;
                        StateItems.Add(y);
                        id++;
                    }
                    index++;
                }
            }
            Console.WriteLine("");
            return Tuple.Create(StateItems, edges);

        }


    }
}
