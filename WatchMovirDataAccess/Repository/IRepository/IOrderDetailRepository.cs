using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchMovie.Models;

namespace WacthMovie.DataAccess.Repository.IRepository
{
    //this single handles the category models
    //note: models can be many and sabai model ko update op xuttai huna sakxa testo
    //case ma chai we perform manually

    public interface IOrderDetailRepository: IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);

        //save must be done in Repository ma but this is not the good practice 
    }
}
