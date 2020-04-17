using FPGrowthLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var datas = new List<DataItem>();
            datas.Add(new DataItem { TID=1,  Items = new List<string> {"M1", "L2" } });
            datas.Add(new DataItem { TID = 2, Items = new List<string> {"L2", "M1", "L3" } });
            datas.Add(new DataItem { TID = 3, Items = new List<string> { "M3", "L2" } });
            datas.Add(new DataItem { TID = 4, Items = new List<string> { "L2" } });
            datas.Add(new DataItem { TID = 5, Items = new List<string> { "B4", "B6", "M3", "L2" } } );
            datas.Add(new DataItem { TID = 6, Items = new List<string> { "Li3"} });
            datas.Add(new DataItem { TID = 7, Items = new List<string> { "B6"} });
            datas.Add(new DataItem { TID = 8, Items = new List<string> { "L2", "M1"  } });
            datas.Add(new DataItem { TID = 9, Items = new List<string> { "B4"} });
            datas.Add(new DataItem { TID = 10, Items = new List<string> { "B2", "M3" } });
            datas.Add(new DataItem { TID = 11, Items = new List<string> { "M1"} });
            datas.Add(new DataItem { TID = 12, Items = new List<string> { "M1", "L2", "L3", "B3", "B2", "B4" } });
            datas.Add(new DataItem { TID = 13, Items = new List<string> { "M3"} });
            datas.Add(new DataItem { TID = 14, Items = new List<string> { "B2", "L2", "B6", "L3" } });
            datas.Add(new DataItem { TID = 15, Items = new List<string> { "L3", "B4"} });
            datas.Add(new DataItem { TID = 16, Items = new List<string> { "B4", "B3" } });
            datas.Add(new DataItem { TID = 17, Items = new List<string> { "M1", "M3", "B4" } });
            datas.Add(new DataItem { TID = 18, Items = new List<string> { "B4", "L2", "B6", "M3" } });
            datas.Add(new DataItem { TID = 19, Items = new List<string> { "M1", "L2", "M7" } });
            datas.Add(new DataItem { TID = 20, Items = new List<string> { "M1", "L2", "M7" } });
            datas.Add(new DataItem { TID = 21, Items = new List<string> { "B4", "B2", "B3" } });
            datas.Add(new DataItem { TID = 22, Items = new List<string> { "M1", "L2", "M7" } });


            
            Helper.PrintDataItem(datas);

            var result =Helper.FrekuensiItem(datas);//.OrderBy(X=>X.Name).ToList();
           
            Helper.PrintFrequensi(result);
           
            double MinSupport = 15;

            
            //remove item less then minsuport
            var frRemoveItem = new List<FekuensiItem>();
            foreach (var item in result.ToList())
            {
                var support = Convert.ToDouble(item.Count)/Convert.ToDouble(datas.Count) * 100;
                if(support < MinSupport)
                {
                    frRemoveItem.Add(item);
                    result.Remove(item);
                }else
                {
                    item.Suport = support;
                }
            }

           

            var resultX = result.OrderBy(x => x.Suport).ToList();
            Console.WriteLine("");
            Console.WriteLine("Frekuensi Sort And Eliminasi By Min Support");
            Helper.PrintFrequensi(result.OrderByDescending(x=>x.Suport).ToList());
            Console.WriteLine(" ");

            foreach (var itemRemove in frRemoveItem)
            {
                foreach(var data in datas)
                {
                    foreach (var item in data.Items.ToList())
                    {
                        if (item == itemRemove.Name)
                        {
                            data.Items.Remove(item);
                        }
                    }
                }
            }
            Console.WriteLine("Data Setelah Eliminasi By Min Support");
            Helper.PrintDataItem(datas);

            foreach (var data in datas)
            {
                data.SortData = new List<string>();

                foreach (var item in result.OrderByDescending(x=>x.Count))
                {
                    var res = data.Items.Where(x => x == item.Name).FirstOrDefault();
                    if (res != null)
                    {
                        data.SortData.Add(res);
                    }
                }
            }

            Console.WriteLine("Data Setelah Pengurutan Berdasrkan Prioritas");
            var dataitemsort = Helper.PrintDataItemSort(datas);

            Console.WriteLine("Data FTree");
            Helper.PrintDataTree(dataitemsort.Item1);
            var vertices = Enumerable.Range(0, 23).ToArray();
            var graph = new Graph<int>(vertices, dataitemsort.Item2);
            var algorithms = new AlgoritmaDFS();
            var path = new List<int>();
           

            Dictionary<string, List<PatternBase>> patternBase = new Dictionary<string, List<PatternBase>>() ;
            var start = 0;
            Console.WriteLine(string.Join(", ", algorithms.DFS(graph, start, v => path.Add(v))));
            Console.WriteLine("");

            foreach(var data in resultX)
            {
                var zzzz = dataitemsort.Item1.Where(x => x.Name == data.Name).ToList();
                
                List<PatternBase> list = new List<PatternBase>(); 
                foreach (var item in zzzz)
                {
                    var pathresult = new List<int>();
                    var pb = new PatternBase();
                    var r = path.IndexOf(item.Id);
                    var jj = dataitemsort.Item1.Where(x => x.Id == path[r]).FirstOrDefault();
                    pb.Count = jj.Count;
                    pathresult.Add(jj.Id);
                    while (jj.Index>0)
                    {
                        jj = dataitemsort.Item1.Where(x => x.Id == jj.ParenId).FirstOrDefault();
                        pathresult.Add(jj.Id);
                        //     Console.WriteLine(string.Join(", ", pathresult));
                      
                    }
                    pathresult.Reverse();
                    pb.Data=pathresult;
                    list.Add(pb);
                   // pathresult.Clear();

                }
                patternBase.Add(data.Name, list);
            }















            Console.WriteLine("Conditional Pattern Base");
            foreach (var item in patternBase)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in item.Value)
                {
                    if(item1.Data.Count>1)
                    {
                        var ii = 0;
                        foreach (var item3 in item1.Data)
                        {
                            var hh = dataitemsort.Item1.Where(x => x.Id == item3).FirstOrDefault();
                            ii++;
                            if (ii != item1.Data.Count)
                                Console.Write($"{hh.Name}-");
                            else
                                Console.Write($":{item1.Count}");
                        }
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("");
            }

            Console.WriteLine("CONDITTIONAL FP TREE");
            Dictionary<string, List<CONDITTIONALFPTREE>> listConditionalFPtree = new Dictionary<string, List<CONDITTIONALFPTREE>>();   
            foreach (var item in patternBase)
            {
                Console.WriteLine(item.Key);
                List<CONDITTIONALFPTREE> dic = new List<CONDITTIONALFPTREE>();
                foreach (var item1 in item.Value)
                {
                    if (item1.Data.Count > 0)
                    {
                        var ii = 0;
                        foreach (var item3 in item1.Data)
                        {
                            var hh = dataitemsort.Item1.Where(x => x.Id == item3).FirstOrDefault();
                            var aa = dic.Where(x => x.Key == hh.Name).FirstOrDefault();
                            if (aa == null)
                            {
                                dic.Add(new CONDITTIONALFPTREE(hh.Name, item1.Count));
                            }
                            else
                            {
                                aa.Value = aa.Value + item1.Count;
                            }

                        }
                    }
                }



                foreach (var dics in dic)
                {
                    Console.WriteLine($"{dics.Key} : {dics.Value}");
                }
                listConditionalFPtree.Add(item.Key, dic);

               



                Console.WriteLine("");
            }

            listConditionalFPtree.Reverse();
            var listItemSet = new List<ItemSet>();
            foreach (var itemm in listConditionalFPtree)
            {
                foreach (var itemm2 in itemm.Value)
                {
                    ItemSet ItemS = new ItemSet { Row = itemm.Key, Column = itemm2.Key, Value = itemm2.Value };
                    listItemSet.Add(ItemS);
                    ItemSet ItemSS = new ItemSet { Column = itemm.Key, Row= itemm2.Key, Value = itemm2.Value };
                    listItemSet.Add(ItemSS);
                }
            }

            Console.WriteLine("FREKUENS 2 ITEMSET");

            resultX.Reverse();
            Console.Write($" X  ");
            foreach (var h in resultX)
            {
                Console.Write($"| {h.Name} ");
            }
            Console.WriteLine("");
            foreach (var r in resultX)
            {
                Console.Write($"{r.Name}  ");
                foreach (var c in resultX)
                {
                    var res = listItemSet.Where(x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault();
                    if (res != null)
                    {
                        Console.Write($"| {res.Value}  ");
                    }
                    else
                    {
                        Console.Write($"| 0  ");
                    }
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("NILAI SUPPORT");
            Console.Write($" X  ");
            foreach (var h in resultX)
            {
                Console.Write($"|  {h.Name}  ");
            }
            Console.WriteLine("");
            foreach (var r in resultX)
            {
                Console.Write($"{r.Name}  ");
                foreach (var c in resultX)
                {
                    var res = listItemSet.Where(x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault();
                    if (res != null)
                    {
                        Console.Write( string.Format("| {0:0.00} ", Convert.ToDouble(res.Value)/22));
                    }
                    else
                    {
                        Console.Write($"|   0  ");
                    }
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("NILAI Confidance");
            Console.Write($" X  ");
            foreach (var h in resultX)
            {
                Console.Write($"|  {h.Name}  ");
            }
            Console.WriteLine("");
            foreach (var r in resultX)
            {
                Console.Write($"{r.Name}  ");
                foreach (var c in resultX)
                {
                    var res = listItemSet.Where(x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault();
                    if (res != null)
                    {
                        Console.Write(string.Format("| {0:0.00} ", Convert.ToDouble(res.Value) / r.Count));
                    }
                    else
                    {
                        Console.Write($"|   0  ");
                    }
                }
                Console.WriteLine("");

            }

            Console.WriteLine("");
            Console.WriteLine("hasil Aturan Asosiasi");
            var listItemSetResult = new List<ItemSet>();
            foreach (var r in resultX)
            {
                foreach (var c in resultX)
                {
                    var res = listItemSet.Where(x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault();
                    if (res != null)
                    {
                        var minSup = Convert.ToDouble(res.Value) / 22;
                        var conf = Convert.ToDouble(res.Value) / r.Count;

                        if (minSup <= 0.1 || conf <= 0.3 || conf==1)
                        {

                        }
                        else
                        {
                            res.MinSuport = minSup;
                            res.ConfidanceSupport = conf;
                            listItemSetResult.Add(res);
                        }
                    }
                }

            }


            foreach (var rr in listItemSetResult)
            {
                Console.WriteLine($"{rr.Row} - {rr.Column} | {rr.MinSuport} | {rr.ConfidanceSupport} |");
            }

            Console.ReadLine();
        }
    }
}
