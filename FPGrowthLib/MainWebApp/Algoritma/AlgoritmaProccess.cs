using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPGrowthLib;
using MainWebApp.Models.Data;

namespace MainWebApp.Algoritma {
    public class AlgoritmaProccess {
        public AlgoritmaProccess (double minSupport, double confidance, List<DataItem> dataSource) {
            double MinSupport = minSupport;
            double Confidance = confidance;
            var datas = dataSource;
            Source = datas;
            var result = FrekuensiItem (datas);
            var frRemoveItem = new List<FekuensiItem> ();
            foreach (var item in result.ToList ()) {
                var support = Convert.ToDouble (item.Count) / Convert.ToDouble (datas.Count) * 100;
                if (support < MinSupport) {
                    frRemoveItem.Add (item);
                    result.Remove (item);
                } else {
                    item.Suport = support;
                }
            }

            Frekuensi = result;
            var resultX = result.OrderByDescending (x => x.Suport).ThenBy (x => x.Name).ToList ();

            foreach (var itemRemove in frRemoveItem) {
                foreach (var data in datas) {
                    foreach (var item in data.Items.ToList ()) {
                        if (item == itemRemove.Name) {
                            data.Items.Remove (item);
                        }
                    }
                }
            }

            foreach (var data in datas) {
                data.SortData = new List<string> ();

                foreach (var item in result.OrderByDescending (x => x.Count).ThenBy (x => x.Name)) {
                    var res = data.Items.Where (x => x == item.Name).FirstOrDefault ();
                    if (res != null) {
                        data.SortData.Add (res);
                    }
                }
            }

            //Console.WriteLine("Data Setelah Pengurutan Berdasrkan Prioritas");
            var dataitemsort = DataItemSort (datas);

            ItemSortPriority = dataitemsort;

            //Console.WriteLine("Data FTree");
            //PrintDataTree(dataitemsort.Item1);
            var vertices = Enumerable.Range (0, datas.Count + 1).ToArray ();
            var graph = new Graph<int> (vertices, dataitemsort.Item2);
            var algorithms = new AlgoritmaDFS ();
            var path = new List<int> ();

            Dictionary<string, List<PatternBase>> patternBase = new Dictionary<string, List<PatternBase>> ();
            var start = 0;
            Console.WriteLine (string.Join (", ", algorithms.DFS (graph, start, v => path.Add (v))));

            foreach (var data in resultX.OrderBy (x => x.Suport).ToList ()) {
                var zzzz = dataitemsort.Item1.Where (x => x.Name == data.Name).ToList ();

                List<PatternBase> list = new List<PatternBase> ();
                foreach (var item in zzzz) {
                    var pathresult = new List<int> ();
                    var pb = new PatternBase ();
                    //      var r = path.IndexOf (item.Id);
                    //    var ssss = path[r];
                    var jj = dataitemsort.Item1.Where (x => x.Id == item.Id).FirstOrDefault ();
                    pb.Count = jj.Count;
                    pathresult.Add (jj.Id);
                    while (jj.Index > 0) {
                        jj = dataitemsort.Item1.Where (x => x.Id == jj.ParenId).FirstOrDefault ();
                        pathresult.Add (jj.Id);
                        //    //Console.WriteLin(string.Join(", ", pathresult));

                    }
                    pathresult.Reverse ();
                    pb.Data = pathresult;
                    list.Add (pb);
                    // pathresult.Clear();

                }
                patternBase.Add (data.Name, list);
            }

            // //Console.WriteLin("Conditional Pattern Base");
            foreach (var item in patternBase) {
                //Console.WriteLin(item.Key);
                foreach (var item1 in item.Value) {
                    if (item1.Data.Count > 1) {
                        var ii = 0;
                        foreach (var item3 in item1.Data) {
                            var hh = dataitemsort.Item1.Where (x => x.Id == item3).FirstOrDefault ();
                            ii++;
                            if (ii != item1.Data.Count)
                                Console.Write ($"{hh.Name}-");
                            else
                                Console.Write ($":{item1.Count}");
                        }
                        //Console.WriteLin("");
                    }
                }

                //Console.WriteLin("");
            }

            //Console.WriteLin("CONDITTIONAL FP TREE");
            Dictionary<string, List<CONDITTIONALFPTREE>> listConditionalFPtree = new Dictionary<string, List<CONDITTIONALFPTREE>> ();
            foreach (var item in patternBase) {
                //Console.WriteLin(item.Key);
                List<CONDITTIONALFPTREE> dic = new List<CONDITTIONALFPTREE> ();
                foreach (var item1 in item.Value) {
                    if (item1.Data.Count > 0) {
                        foreach (var item3 in item1.Data) {
                            var hh = dataitemsort.Item1.Where (x => x.Id == item3).FirstOrDefault ();
                            var aa = dic.Where (x => x.Key == hh.Name).FirstOrDefault ();
                            if (aa == null) {
                                dic.Add (new CONDITTIONALFPTREE (hh.Name, item1.Count));
                            } else {
                                aa.Value = aa.Value + item1.Count;
                            }

                        }
                    }
                }

                /* foreach (var dics in dic)
                 {
                    //Console.WriteLin($"{dics.Key} : {dics.Value}");
                 }*/
                listConditionalFPtree.Add (item.Key, dic);

                // //Console.WriteLin("");
            }

            listConditionalFPtree.Reverse ();
            var listItemSet = new List<ItemSet> ();
            foreach (var itemm in listConditionalFPtree) {
                foreach (var itemm2 in itemm.Value) {
                    ItemSet ItemS = new ItemSet { Row = itemm.Key, Column = itemm2.Key, Value = itemm2.Value };
                    listItemSet.Add (ItemS);
                    ItemSet ItemSS = new ItemSet { Column = itemm.Key, Row = itemm2.Key, Value = itemm2.Value };
                    listItemSet.Add (ItemSS);
                }
            }

            Vertices = vertices;
            Graphs = graph;
            ListConditionFPTree = listConditionalFPtree;

            ////Console.WriteLin("FREKUENS 2 ITEMSET");

            resultX.Reverse ();
            var frekuensiToItemSet = new List<List<double>> ();
            foreach (var r in resultX) {
                var resItems = new List<double> ();
                foreach (var c in resultX) {
                    var res = listItemSet.Where (x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault ();
                    if (res != null) {
                        resItems.Add (res.Value);
                    } else {
                        resItems.Add (0);
                    }
                }
                frekuensiToItemSet.Add (resItems);
            }

            FrekuensiItemSet = frekuensiToItemSet;

            ResultX = resultX.OrderByDescending (x => x.Suport).ThenBy (x => x.Name).ToList ();

            //Console.WriteLin("");
            //Console.WriteLin("NILAI Confidance");
            Console.Write ($" X  ");
            foreach (var h in resultX) {
                Console.Write ($"|  {h.Name}  ");
            }
            //Console.WriteLin("");

            ListItemSet = listItemSet;

            ////Console.WriteLin("");
            //Console.WriteLine("hasil Aturan Asosiasi");
            var listItemSetResult = new List<ItemSet> ();
            foreach (var r in resultX) {
                foreach (var c in resultX) {
                    var res = listItemSet.Where (x => x.Row == r.Name && x.Column == c.Name).FirstOrDefault ();
                    if (res != null) {
                        var minSup = Convert.ToDouble (res.Value) / datas.Count;
                        var conf = Convert.ToDouble (res.Value) / r.Count;
                        var rrr = (MinSupport / 100);
                        if (minSup <= 0.1 || conf <= Confidance || conf == 1) {

                        } else {
                            res.MinSuport = minSup;
                            res.ConfidanceSupport = conf;
                            listItemSetResult.Add (res);
                        }
                    }
                }

            }

            ListItemSetResult = listItemSetResult.OrderByDescending (x => x.Value).ToList ();
        }

        public List<DataItem> Source { get; }
        public List<FekuensiItem> Frekuensi { get; private set; }
        public List<ItemSet> ListItemSetResult { get; }
        public List<FekuensiItem> ResultX { get; }
        public List<List<double>> FrekuensiItemSet { get; private set; }
        public List<List<double>> ConfidanceDataSet { get; }
        public List<ItemSet> ListItemSet { get; }
        public Dictionary<string, List<CONDITTIONALFPTREE>> ListConditionFPTree { get; }
        public Tuple<List<StateItem>, List<Tuple<int, int>>> ItemSortPriority { get; }
        public int[] Vertices { get; }
        public Graph<int> Graphs { get; }

        public static List<FekuensiItem> FrekuensiItem (List<DataItem> datas) {

            List<FekuensiItem> items = new List<FekuensiItem> ();
            foreach (var item in datas) {
                foreach (var data in item.Items) {
                    var result = items.Where (x => x.Name == data).FirstOrDefault ();
                    if (result == null) {
                        items.Add (new FekuensiItem { Name = data, Count = 1 });
                    } else
                        result.Count++;
                }
            }

            return items;
        }

        public static object GetStringItems (List<string> items) {
            StringBuilder sb = new StringBuilder ();
            foreach (var item in items) {
                sb.Append (item).Append (", ");
            }
            return sb.ToString ();
        }

        public static Tuple<List<StateItem>, List<Tuple<int, int>>> DataItemSort (List<DataItem> datas) {
            List<StateItem> StateItems = new List<StateItem> ();
            var edges = new List<Tuple<int, int>> ();
            int id = 1;
            foreach (var item in datas) {
                var index = 0;
                StateItem parent = null;
                foreach (var d in item.SortData) {
                    StateItem x = null;
                    if (parent != null) {
                        x = StateItems.Where (x => x.Index == index && x.Name == d && x.ParenId == parent.Id).FirstOrDefault ();
                    } else {
                        x = StateItems.Where (x => x.Index == index && x.Name == d).FirstOrDefault ();
                    }

                    if (x != null) {
                        x.Count++;
                        parent = x;
                    } else {
                        StateItem y = null;
                        if (parent != null) {
                            y = new StateItem { Index = index, Name = d, Count = 1, Id = id, ParenId = parent.Id };
                            edges.Add (Tuple.Create (parent.Id, y.Id));
                            y.ParentIndex = parent.Index;
                            y.ParentName = parent.Name;
                        } else {
                            y = new StateItem { Index = index, Name = d, Count = 1, Id = id };
                            edges.Add (Tuple.Create (0, y.Id));
                        }

                        parent = y;
                        StateItems.Add (y);
                        id++;
                    }
                    index++;
                }
            }
            return Tuple.Create (StateItems, edges);

        }

    }

}