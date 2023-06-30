using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    // CAPA DE (NEGOCIO)
    public class businessLogicLayer
    {

        private DataAccessLayer _dataAccessLayer;
        public businessLogicLayer()
        { 
            _dataAccessLayer = new DataAccessLayer();   
        }
        public ContacModel guardarContac(ContacModel contac)
        {
            if (contac.id == 0)
            {
                _dataAccessLayer.insertContac(contac);

            }
            else
                _dataAccessLayer.updateContacto(contac);
            return contac;
           
        }


        public List<ContacModel> GetContactos(string buscartxt = null) 
        { 
           return _dataAccessLayer.GetContactos(buscartxt);
        }

        public void EliminarContac(int id)
        {
            _dataAccessLayer.EliminarContac(id);
        }
    }
}
