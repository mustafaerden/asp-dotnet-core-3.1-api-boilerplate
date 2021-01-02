using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListByCategory(int categoryId);

        // void olan metotlari IResult donuyoruz;
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);

        // Transaction Testi icin;
        //IResult TransactionOperation(Product product);
    }
}
