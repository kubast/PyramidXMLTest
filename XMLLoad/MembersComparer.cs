using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using XMLLoad.Files;

namespace XMLLoad
{
   public class MembersComaprer : IComparer<Member>
    {
        public enum ComparerType
        {
          Id = 0,
          Level = 1
        }

        private ComparerType _comparisionType;
        public MembersComaprer(ComparerType compType)
        {
            _comparisionType = compType;
        }
        public int Compare( Member x,  Member y)
        {
            return _comparisionType == ComparerType.Id ? x.Id.CompareTo(y.Id) : x.Level.CompareTo(y.Level);
        }
    }
}
