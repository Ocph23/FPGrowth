using System;
using System.Collections.Generic;

namespace FPGrowthLib
{
    public class DataItem
    {
        public int TID { get; set; }
        public List<string> Items { get; set; }
        public List<string> SortData { get; set; }
    }


    public class FekuensiItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Suport { get; set; }
    }


    public class ItemSet
    {
        public string Row { get; set; }
        public string Column { get; set; }
        public int Value { get; set; }
        public double MinSuport { get;  set; }
        public double ConfidanceSupport { get;  set; }
    }
    public class StateItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int ParentIndex { get; set; }
        public string ParentName { get; set; }
        public int Id { get;  set; }
        public int ParenId { get;  set; }
    }




    public class PatternBase
    {
        public int Count { get; set; }
        public List<int> Data { get; set; }

    }



    public class CONDITTIONALFPTREE
    {

        public CONDITTIONALFPTREE(string name, int count)
        {
            this.Key = name;
            this.Value = count;
        }

        public string Key { get; set; }
        public int Value { get; set; }
    }

}
