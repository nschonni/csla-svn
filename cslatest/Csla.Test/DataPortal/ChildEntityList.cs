using System;
using System.Data;

namespace Csla.Test.DataBinding
{
    [Serializable()]
    public class ChildEntityList : BusinessListBase<ChildEntityList, ChildEntity>
    {
        private ChildEntityList()
        {
            //require factory method
            this.MarkAsChild();
        }

        #region "factory methods"

        public static ChildEntityList NewChildEntityList()
        {
            return new ChildEntityList();
        }

        #endregion

        #region "Criteria"

        [Serializable()]
        private class Criteria
        {
            //no criteria for this list
        }

        #endregion

        internal void update(IDbTransaction tr)
        {
            foreach (ChildEntity child in this)
            {
                //child.Update(tr);
            }
        }

        public static ChildEntityList GetList()
        {
            return Csla.DataPortal.Fetch<ChildEntityList>(new Criteria());
        }

        protected override void DataPortal_Fetch(object criteria)
        {
            for (int i = 0; i < 10; i++)
                Add(new ChildEntity(i, "first" + i, "last" + i));
        }
    }
}
