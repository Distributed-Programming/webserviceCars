using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace webserviceCars
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Car addCar(Car composite);

        [OperationContract]
        Car getCar(int id);

        [OperationContract]
        List<Car> getCars();

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Car
    {

        private string _model;
        private string _brand;
        private Colors _color;
        private int _id;
        private float _engine;

        [DataMember]
        public float engine
        {
            get { return _engine; }
            set { _engine = value; }
        }

        [DataMember]
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public Colors color
        {
            get { return _color; }
            set { _color = value; }
        }

        [DataMember]
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        [DataMember]
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
    }

    [DataContract]
    public enum Colors
    {
        [EnumMember]
        Grey,
        [EnumMember]
        Red,
        [EnumMember]
        Blue
    }
}
