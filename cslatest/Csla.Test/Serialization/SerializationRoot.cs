using System;

namespace Csla.Test.Serialization
{
    [Serializable()]
    public class SerializationRoot : BusinessBase<SerializationRoot>
    {
        string _data = "";

        protected override object GetIdValue( )
        {
            return _data;
        }

        public string Data
        {
            get
            {
                return _data;
            }

            set
            {
                if(_data != value)
                {
                    _data = value;
                    PropertyHasChanged( );
                }
            }
        }

        //public SerializationRoot()
        //{
        //    nonSerializableEventHandler objHandler = new nonSerializableEventHandler();
        //    objHandler.Reg(this);
        //}

        protected override void OnDeserialized(System.Runtime.Serialization.StreamingContext context)
        {
 	        base.OnDeserialized(context);
            Csla.ApplicationContext.GlobalContext.Add("Deserialized", true);
            Console.WriteLine("OnDeserialized");
        }
    }
}